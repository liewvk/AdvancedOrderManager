Option Explicit On
Option Strict On
Option Infer On

Imports System
Imports System.Diagnostics
Imports System.Threading
Imports System.Threading.Tasks
Imports AdvancedOrderManager.Application
Imports Microsoft.Extensions.Logging

Public NotInheritable Class SimulatedOrderProcessingService
    Implements IAsyncOrderProcessingService

    Private Const DelayPerOrderMilliseconds As Integer = 250

    Private ReadOnly _logger As ILogger(Of SimulatedOrderProcessingService)

    Public Sub New(
        logger As ILogger(Of SimulatedOrderProcessingService))

        If logger Is Nothing Then
            Throw New ArgumentNullException(NameOf(logger))
        End If

        _logger = logger
    End Sub

    Public Async Function ProcessAsync(
        orderCount As Integer,
        progress As IProgress(Of OrderProcessingProgress),
        cancellationToken As CancellationToken) As Task(Of OrderProcessingSummary) _
        Implements IAsyncOrderProcessingService.ProcessAsync

        If orderCount <= 0 Then
            Throw New ArgumentOutOfRangeException(
                NameOf(orderCount),
                "The order count must be greater than zero.")
        End If

        If progress Is Nothing Then
            Throw New ArgumentNullException(NameOf(progress))
        End If

        Dim processingTimer As Stopwatch =
            Stopwatch.StartNew()

        Dim processedOrders As Integer = 0

        _logger.LogInformation(
            "Asynchronous processing started for {OrderCount} orders.",
            orderCount)

        Try
            For orderNumber As Integer = 1 To orderCount

                cancellationToken.ThrowIfCancellationRequested()

                Await Task.Delay(
                    DelayPerOrderMilliseconds,
                    cancellationToken)

                processedOrders = orderNumber

                Dim message As String =
                    $"Processed order {orderNumber} of {orderCount}."

                Dim progressUpdate As New OrderProcessingProgress(
                    processedOrders,
                    orderCount,
                    message)

                progress.Report(progressUpdate)
            Next

        Catch ex As OperationCanceledException

            _logger.LogWarning(
                "Asynchronous processing was cancelled after " &
                "{ProcessedOrders} of {RequestedOrders} orders.",
                processedOrders,
                orderCount)

            Throw

        Catch ex As Exception

            _logger.LogError(
                ex,
                "An error occurred during asynchronous order processing.")

            Throw

        Finally
            processingTimer.Stop()
        End Try

        _logger.LogInformation(
            "Asynchronous processing completed. " &
            "{ProcessedOrders} orders were processed in " &
            "{ElapsedMilliseconds} milliseconds.",
            processedOrders,
            processingTimer.ElapsedMilliseconds)

        Return New OrderProcessingSummary(
            orderCount,
            processedOrders,
            processingTimer.Elapsed)
    End Function

End Class