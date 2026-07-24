Option Explicit On
Option Strict On
Option Infer On

Imports AdvancedOrderManager.Application
Imports AdvancedOrderManager.Domain
Imports AdvancedOrderManager.Infrastructure
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass>
Public Class InventoryAdjustmentServiceTests

    Private Shared Function CreateProduct() As Product

        Return New Product(
            ProductId.NewId(),
            ProductCode.Create(
                "PRD-00001"),
            "Keyboard",
            "Hardware",
            89.9D,
            10,
            3)
    End Function

    <TestMethod>
    Public Sub Adjust_ChangesStockQuantity()

        Dim repository As New InMemoryProductRepository()

        Dim product = CreateProduct()
        repository.Add(product)

        Dim service As New InventoryAdjustmentService(
                repository)

        Dim result =
            service.Adjust(
                product.ProductId,
                5,
                "New delivery")

        Assert.IsTrue(result.IsSuccess)
        Assert.AreEqual(
            15,
            product.QuantityInStock)
    End Sub

    <TestMethod>
    Public Sub Adjust_NegativeResult_ReturnsFailure()

        Dim repository As New InMemoryProductRepository()

        Dim product = CreateProduct()
        repository.Add(product)

        Dim service As New InventoryAdjustmentService(
                repository)

        Dim result =
            service.Adjust(
                product.ProductId,
                -20,
                "Damaged stock")

        Assert.IsFalse(result.IsSuccess)
        Assert.AreEqual(
            10,
            product.QuantityInStock)
    End Sub

    <TestMethod>
    Public Sub UndoLast_ReversesAdjustment()

        Dim repository As New InMemoryProductRepository()

        Dim product = CreateProduct()
        repository.Add(product)

        Dim service As New InventoryAdjustmentService(
                repository)

        service.Adjust(
            product.ProductId,
            5,
            "New delivery")

        Dim undoResult =
            service.UndoLast()

        Assert.IsTrue(undoResult.IsSuccess)
        Assert.AreEqual(
            10,
            product.QuantityInStock)
    End Sub

End Class

