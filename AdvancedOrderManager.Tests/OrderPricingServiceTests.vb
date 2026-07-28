Option Explicit On
Option Strict On
Option Infer On

Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports AdvancedOrderManager.Application

<TestClass>
<TestCategory("Unit")>
Public Class OrderPricingServiceTests

    <TestMethod>
    Public Sub CalculateTotal_StandardOrder_ReturnsSubtotal()

        Dim service =
            New OrderPricingService()

        Dim submission =
            New OrderSubmission(
                "ORD-PRICE-1",
                "Test Customer",
                2,
                50D,
                False)

        Dim total =
            service.CalculateTotal(
                submission,
                applyTax:=False)

        Assert.AreEqual(
            100D,
            total)
    End Sub

    <TestMethod>
    Public Sub CalculateTotal_PriorityOrder_AddsSurcharge()

        Dim service =
            New OrderPricingService()

        Dim submission =
            New OrderSubmission(
                "ORD-PRICE-2",
                "Test Customer",
                2,
                50D,
                True)

        Dim total =
            service.CalculateTotal(
                submission,
                applyTax:=False)

        Assert.AreEqual(
            110D,
            total)
    End Sub

    <TestMethod>
    Public Sub CalculateTotal_BulkOrder_AppliesDiscount()

        Dim service =
            New OrderPricingService()

        Dim submission =
            New OrderSubmission(
                "ORD-PRICE-3",
                "Test Customer",
                10,
                10D,
                False)

        Dim total =
            service.CalculateTotal(
                submission,
                applyTax:=False)

        Assert.AreEqual(
            95D,
            total)
    End Sub

    <TestMethod>
    Public Sub CalculateTotal_TaxEnabled_AddsTax()

        Dim service =
            New OrderPricingService()

        Dim submission =
            New OrderSubmission(
                "ORD-PRICE-4",
                "Test Customer",
                2,
                50D,
                False)

        Dim total =
            service.CalculateTotal(
                submission,
                applyTax:=True)

        Assert.AreEqual(
            106D,
            total)
    End Sub

End Class

