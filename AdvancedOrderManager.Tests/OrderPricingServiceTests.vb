Option Explicit On
Option Strict On
Option Infer On

Imports AdvancedOrderManager.Application
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass>
<TestCategory("Unit")>
Public Class OrderPricingServiceTests

    Private Shared Function CreateOptions() As OrderManagerOptions

        Return New OrderManagerOptions() With {
            .DemonstrationTaxRate = 0.06D,
            .MinimumBulkQuantity = 10,
            .BulkDiscountRate = 0.05D,
            .PrioritySurchargeRate = 0.05D
        }
    End Function

    <TestMethod>
    Public Sub CalculateTotal_StandardOrder_ReturnsSubtotal()

        Dim service =
            New OrderPricingService(
                CreateOptions())

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
            New OrderPricingService(
                CreateOptions())

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
            105D,
            total)
    End Sub

    <TestMethod>
    Public Sub CalculateTotal_BulkOrder_AppliesDiscount()

        Dim service =
            New OrderPricingService(
                CreateOptions())

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
            New OrderPricingService(
                CreateOptions())

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