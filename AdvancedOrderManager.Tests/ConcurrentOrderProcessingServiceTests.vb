Option Explicit On
Option Strict On
Option Infer On

Imports System.Collections.Generic
Imports System.Threading
Imports System.Threading.Tasks
Imports AdvancedOrderManager.Application
Imports AdvancedOrderManager.Infrastructure
Imports Microsoft.Extensions.Logging.Abstractions
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass>
<TestCategory("Unit")>
Public Class ConcurrentOrderProcessingServiceTests

    <TestMethod>
    Public Async Function ProcessBatchAsync_SixOrders_CompletesAll() _
        As Task

        Dim updates As New List(
            Of ConcurrentBatchProgress)()

        Dim progress =
            New ImmediateProgress(
                Of ConcurrentBatchProgress)(
                    Sub(update)
                        updates.Add(update)
                    End Sub)

        Dim service =
            New ConcurrentOrderProcessingService(
                NullLogger(
                    Of ConcurrentOrderProcessingService).Instance)

        Dim summary As ConcurrentBatchSummary =
            Await service.ProcessBatchAsync(
                6,
                2,
                progress,
                CancellationToken.None)

        Assert.AreEqual(
            6,
            summary.RequestedOrders)

        Assert.AreEqual(
            6,
            summary.CompletedOrders)

        Assert.AreEqual(
            0,
            summary.FailedOrders)

        Assert.HasCount(
    6,
    summary.Results)

        Assert.IsInRange(
    1,
    2,
    summary.PeakConcurrency)

        Assert.HasCount(
    6,
    updates)


        Assert.AreEqual(
            100,
            updates(updates.Count - 1).Percentage)
    End Function

    <TestMethod>
    Public Async Function ProcessBatchAsync_InvalidConcurrency_ThrowsException() _
        As Task

        Dim progress =
            New ImmediateProgress(
                Of ConcurrentBatchProgress)(
                    Sub(update)
                    End Sub)

        Dim service =
            New ConcurrentOrderProcessingService(
                NullLogger(
                    Of ConcurrentOrderProcessingService).Instance)

        Dim exceptionWasThrown As Boolean = False

        Try
            Await service.ProcessBatchAsync(
                5,
                0,
                progress,
                CancellationToken.None)

        Catch ex As ArgumentOutOfRangeException
            exceptionWasThrown = True
        End Try

        Assert.IsTrue(
            exceptionWasThrown,
            "The service should reject a maximum " &
            "concurrency value of zero.")
    End Function

    <TestMethod>
    Public Async Function ProcessBatchAsync_CancelledToken_ThrowsException() _
        As Task

        Dim progress =
            New ImmediateProgress(
                Of ConcurrentBatchProgress)(
                    Sub(update)
                    End Sub)

        Dim service =
            New ConcurrentOrderProcessingService(
                NullLogger(
                    Of ConcurrentOrderProcessingService).Instance)

        Using cancellationSource As New CancellationTokenSource()

            cancellationSource.Cancel()

            Dim cancellationWasThrown As Boolean = False

            Try
                Await service.ProcessBatchAsync(
                    10,
                    3,
                    progress,
                    cancellationSource.Token)

            Catch ex As OperationCanceledException
                cancellationWasThrown = True
            End Try

            Assert.IsTrue(
                cancellationWasThrown,
                "The service should throw " &
                "OperationCanceledException when the token " &
                "has already been cancelled.")
        End Using
    End Function

    Private NotInheritable Class ImmediateProgress(Of T)
        Implements IProgress(Of T)

        Private ReadOnly _handler As Action(Of T)

        Public Sub New(
            handler As Action(Of T))

            If handler Is Nothing Then
                Throw New ArgumentNullException(
                    NameOf(handler))
            End If

            _handler = handler
        End Sub

        Public Sub Report(
            value As T) _
            Implements IProgress(Of T).Report

            _handler(value)
        End Sub

    End Class

End Class

