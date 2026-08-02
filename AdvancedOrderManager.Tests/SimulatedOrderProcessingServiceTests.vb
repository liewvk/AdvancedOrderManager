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
Public Class SimulatedOrderProcessingServiceTests

    <TestMethod>
    Public Async Function ProcessAsync_TwoOrders_ReturnsSummary() _
        As Task

        Dim updates =
            New List(
                Of OrderProcessingProgress)()

        Dim progress =
            New ImmediateProgress(
                Of OrderProcessingProgress)(
                    Sub(update)
                        updates.Add(update)
                    End Sub)

        Dim service =
            New SimulatedOrderProcessingService(
                NullLogger(
                    Of SimulatedOrderProcessingService).Instance)

        Dim result =
            Await service.ProcessAsync(
                2,
                progress,
                CancellationToken.None)

        Assert.AreEqual(
            2,
            result.RequestedOrders)

        Assert.AreEqual(
            2,
            result.ProcessedOrders)

        Assert.HasCount(2, updates)

        Assert.AreEqual(
            100,
            updates(1).Percentage)
    End Function

    <TestMethod>
    Public Async Function ProcessAsync_CancelledToken_ThrowsException() _
    As Task

        Dim progress =
        New ImmediateProgress(Of OrderProcessingProgress)(
            Sub(update)
                'No progress action is required for this test.
            End Sub)

        Dim service =
        New SimulatedOrderProcessingService(
            NullLogger(
                Of SimulatedOrderProcessingService).Instance)

        Using cancellationSource As New CancellationTokenSource()

            cancellationSource.Cancel()

            Dim cancellationWasThrown As Boolean = False

            Try
                Await service.ProcessAsync(
                5,
                progress,
                cancellationSource.Token)

            Catch ex As OperationCanceledException
                cancellationWasThrown = True
            End Try

            Assert.IsTrue(
            cancellationWasThrown,
            "ProcessAsync should throw OperationCanceledException " &
            "when the cancellation token has already been cancelled.")

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
