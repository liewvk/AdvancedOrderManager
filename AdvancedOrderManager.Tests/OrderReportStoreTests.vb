Option Explicit On
Option Strict On
Option Infer On

Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports AdvancedOrderManager.Application

<TestClass>
<TestCategory("Unit")>
Public Class OrderReportStoreTests

    <TestMethod>
    Public Sub HandleOrderProcessed_AddsReportRecord()

        Dim store =
            New OrderReportStore()

        Dim eventArguments =
            New OrderProcessedEventArgs(
                "ORD-REPORT-1",
                "Alice Tan",
                250D,
                True,
                New DateTimeOffset(
                    2026,
                    7,
                    26,
                    10,
                    0,
                    0,
                    TimeSpan.Zero))

        store.HandleOrderProcessed(
            Me,
            eventArguments)

        Dim records =
            store.GetSnapshot()

        Assert.HasCount(1,
            records)

        Assert.AreEqual(
            OrderReportStatus.Processed,
            records(0).Status)

        Assert.AreEqual(
            250D,
            records(0).TotalAmount)

        Assert.IsTrue(
            records(0).IsPriority)
    End Sub

End Class

