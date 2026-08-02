Option Explicit On
Option Strict On
Option Infer On

Imports System.Collections.Concurrent
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Linq
Imports System.Threading
Imports System.Threading.Tasks
Imports AdvancedOrderManager.Application
Imports Microsoft.Extensions.Logging

Public NotInheritable Class ConcurrentOrderProcessingService
    Implements IConcurrentOrderProcessingService

    Private ReadOnly _logger As ILogger(
        Of ConcurrentOrderProcessingService)

    Public Sub New(
        logger As ILogger(
            Of ConcurrentOrderProcessingService))

        If logger Is Nothing Then
            Throw New ArgumentNullException(
                NameOf(logger))
        End If

        _logger = logger
    End Sub

    Public Async Function ProcessBatchAsync(
        orderCount As Integer,
        maximumConcurrency As Integer,
        progress As IProgress(Of ConcurrentBatchProgress),
        cancellationToken As CancellationToken) _
        As Task(Of ConcurrentBatchSummary) _
        Implements IConcurrentOrderProcessingService.ProcessBatchAsync

        ValidateArguments(
            orderCount,
            maximumConcurrency,
            progress)

        Dim processingTimer As Stopwatch =
            Stopwatch.StartNew()

        Dim results As New ConcurrentBag(
            Of ConcurrentOrderResult)()

        Dim processingState As New ProcessingState()

        _logger.LogInformation(
            "Concurrent processing started for " &
            "{OrderCount} orders with maximum concurrency " &
            "{MaximumConcurrency}.",
            orderCount,
            maximumConcurrency)

        Using concurrencyGate As New SemaphoreSlim(
            maximumConcurrency,
            maximumConcurrency)

            Dim tasks As New List(Of Task)()

            For orderNumber As Integer = 1 To orderCount

                Dim currentOrderNumber As Integer =
                    orderNumber

                Dim processingTask As Task =
                    ProcessOneOrderAsync(
                        currentOrderNumber,
                        orderCount,
                        concurrencyGate,
                        results,
                        processingState,
                        progress,
                        cancellationToken)

                tasks.Add(processingTask)
            Next

            Try
                Await Task.WhenAll(tasks)

            Catch ex As OperationCanceledException

                _logger.LogWarning(
                    "Concurrent processing was cancelled " &
                    "after {CompletedOrders} orders.",
                    Volatile.Read(
                        processingState.CompletedOrders))

                Throw

            Catch ex As Exception

                _logger.LogError(
                    ex,
                    "The concurrent batch failed.")

                Throw

            Finally
                processingTimer.Stop()
            End Try
        End Using

        Dim orderedResults As IReadOnlyList(
            Of ConcurrentOrderResult) =
            results _
                .OrderBy(
                    Function(result)
                        Return result.OrderNumber
                    End Function) _
                .ToList() _
                .AsReadOnly()

        Dim failedOrders As Integer =
    System.Linq.Enumerable.Count(
        orderedResults,
        Function(result As ConcurrentOrderResult)
            Return Not result.WasSuccessful
        End Function)

        Dim completedOrders As Integer =
            orderedResults.Count

        Dim peakConcurrency As Integer =
            Volatile.Read(
                processingState.PeakOperations)

        _logger.LogInformation(
            "Concurrent processing completed. " &
            "{CompletedOrders} orders were completed in " &
            "{ElapsedMilliseconds} milliseconds. " &
            "Peak concurrency was {PeakConcurrency}.",
            completedOrders,
            processingTimer.ElapsedMilliseconds,
            peakConcurrency)

        Return New ConcurrentBatchSummary(
            orderCount,
            completedOrders,
            failedOrders,
            peakConcurrency,
            processingTimer.Elapsed,
            orderedResults)
    End Function

    Private Async Function ProcessOneOrderAsync(
        orderNumber As Integer,
        totalOrders As Integer,
        concurrencyGate As SemaphoreSlim,
        results As ConcurrentBag(Of ConcurrentOrderResult),
        processingState As ProcessingState,
        progress As IProgress(Of ConcurrentBatchProgress),
        cancellationToken As CancellationToken) As Task

        Dim gateEntered As Boolean = False
        Dim orderTimer As Stopwatch = Nothing

        Try
            Await concurrencyGate.WaitAsync(
                cancellationToken)

            gateEntered = True

            Dim activeOperations As Integer =
                Interlocked.Increment(
                    processingState.ActiveOperations)

            UpdatePeakConcurrency(
                processingState,
                activeOperations)

            orderTimer = Stopwatch.StartNew()

            _logger.LogInformation(
                "Processing order {OrderNumber}. " &
                "Active operations: {ActiveOperations}.",
                orderNumber,
                activeOperations)

            Dim simulatedDelay As Integer =
                250 + ((orderNumber Mod 4) * 100)

            Await Task.Delay(
                simulatedDelay,
                cancellationToken)

            orderTimer.Stop()

            results.Add(
                New ConcurrentOrderResult(
                    orderNumber,
                    True,
                    orderTimer.Elapsed,
                    $"Order {orderNumber} completed."))

            Dim completedOrders As Integer =
                Interlocked.Increment(
                    processingState.CompletedOrders)

            Dim currentActive As Integer =
                Volatile.Read(
                    processingState.ActiveOperations)

            progress.Report(
                New ConcurrentBatchProgress(
                    completedOrders,
                    totalOrders,
                    currentActive,
                    $"Completed order {orderNumber} " &
                    $"of {totalOrders}."))

        Catch ex As OperationCanceledException
            Throw

        Catch ex As Exception

            If orderTimer IsNot Nothing Then
                orderTimer.Stop()
            End If

            Dim elapsed As TimeSpan =
                If(
                    orderTimer Is Nothing,
                    TimeSpan.Zero,
                    orderTimer.Elapsed)

            results.Add(
                New ConcurrentOrderResult(
                    orderNumber,
                    False,
                    elapsed,
                    ex.Message))

            Dim completedOrders As Integer =
                Interlocked.Increment(
                    processingState.CompletedOrders)

            Dim currentActive As Integer =
                Volatile.Read(
                    processingState.ActiveOperations)

            progress.Report(
                New ConcurrentBatchProgress(
                    completedOrders,
                    totalOrders,
                    currentActive,
                    $"Order {orderNumber} failed."))

            _logger.LogError(
                ex,
                "Order {OrderNumber} failed.",
                orderNumber)

        Finally
            If gateEntered Then

                Interlocked.Decrement(
                    processingState.ActiveOperations)

                concurrencyGate.Release()
            End If
        End Try
    End Function

    Private Shared Sub UpdatePeakConcurrency(
        processingState As ProcessingState,
        currentActiveOperations As Integer)

        Dim previousPeak As Integer

        Do
            previousPeak =
                Volatile.Read(
                    processingState.PeakOperations)

            If currentActiveOperations <= previousPeak Then
                Return
            End If

        Loop While Interlocked.CompareExchange(
            processingState.PeakOperations,
            currentActiveOperations,
            previousPeak) <> previousPeak
    End Sub

    Private Shared Sub ValidateArguments(
        orderCount As Integer,
        maximumConcurrency As Integer,
        progress As IProgress(Of ConcurrentBatchProgress))

        If orderCount <= 0 Then
            Throw New ArgumentOutOfRangeException(
                NameOf(orderCount),
                "The order count must be greater than zero.")
        End If

        If maximumConcurrency <= 0 Then
            Throw New ArgumentOutOfRangeException(
                NameOf(maximumConcurrency),
                "Maximum concurrency must be greater than zero.")
        End If

        If maximumConcurrency > orderCount Then
            Throw New ArgumentOutOfRangeException(
                NameOf(maximumConcurrency),
                "Maximum concurrency cannot exceed " &
                "the number of orders.")
        End If

        If progress Is Nothing Then
            Throw New ArgumentNullException(
                NameOf(progress))
        End If
    End Sub

    Private NotInheritable Class ProcessingState

        Public CompletedOrders As Integer

        Public ActiveOperations As Integer

        Public PeakOperations As Integer

    End Class

End Class

