Option Explicit On
Option Strict On
Option Infer On

Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass>
<TestCategory("Unit")>
Public Class OrderProcessorTests

    <TestMethod>
    Public Sub Process_ValidOrder_RaisesProcessedEvent()

        Dim validator As Func(Of OrderSubmission, String) =
        Function(submission As OrderSubmission) As String

            Return String.Empty
        End Function

        Dim calculator As Func(Of OrderSubmission, Decimal) =
        Function(submission As OrderSubmission) As Decimal

            Return submission.Subtotal
        End Function

        Dim processor =
        New OrderProcessor(
            validator,
            calculator)

        Dim receivedEvent As OrderProcessedEventArgs =
        Nothing

        AddHandler processor.OrderProcessed,
        Sub(sender As Object,
            eventArgs As OrderProcessedEventArgs)

            receivedEvent = eventArgs
        End Sub

        Dim testOrder =
        New OrderSubmission(
            "ORD-TEST-1",
            "Test Customer",
            2,
            50D,
            False)

        Dim result =
        processor.Process(testOrder)

        Assert.IsTrue(result)
        Assert.IsNotNull(receivedEvent)

        Assert.AreEqual(
        "ORD-TEST-1",
        receivedEvent.OrderNumber)

        Assert.AreEqual(
        100D,
        receivedEvent.TotalAmount)
    End Sub
    <TestMethod>
    Public Sub Process_InvalidOrder_RaisesRejectedEvent()

        Dim validator As Func(Of OrderSubmission, String) =
        Function(submission As OrderSubmission) As String

            Return "Order rejected by test rule."
        End Function

        Dim calculator As Func(Of OrderSubmission, Decimal) =
        Function(submission As OrderSubmission) As Decimal

            Return submission.Subtotal
        End Function

        Dim processor =
        New OrderProcessor(
            validator,
            calculator)

        Dim receivedEvent As OrderRejectedEventArgs =
        Nothing

        AddHandler processor.OrderRejected,
        Sub(sender As Object,
            eventArgs As OrderRejectedEventArgs)

            receivedEvent = eventArgs
        End Sub

        Dim testOrder =
        New OrderSubmission(
            "ORD-TEST-2",
            "Test Customer",
            1,
            20D,
            False)

        Dim result =
        processor.Process(
            testOrder)

        Assert.IsFalse(result)

        Assert.IsNotNull(
        receivedEvent)

        Assert.AreEqual(
        "Order rejected by test rule.",
        receivedEvent.Reason)
    End Sub

End Class

