Option Explicit On
Option Strict On
Option Infer On

Imports AdvancedOrderManager.Application
Imports AdvancedOrderManager.Domain
Imports AdvancedOrderManager.Infrastructure
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass>
Public Class RestockQueueServiceTests

    Private Shared Function CreateLowStockProduct(
        code As String) As Product

        Return New Product(
            ProductId.NewId(),
            ProductCode.Create(code),
            "Low Stock Product",
            "Hardware",
            10D,
            2,
            5)
    End Function

    <TestMethod>
    Public Sub Enqueue_AddsLowStockProduct()

        Dim repository As New InMemoryProductRepository()

        Dim product =
            CreateLowStockProduct(
                "PRD-00001")

        repository.Add(product)

        Dim service As New RestockQueueService(
                repository)

        Dim result =
            service.Enqueue(
                product.ProductId)

        Assert.IsTrue(result.IsSuccess)
        Assert.AreEqual(
            1,
            service.PendingCount)
    End Sub

    <TestMethod>
    Public Sub Enqueue_DuplicateProduct_ReturnsFailure()

        Dim repository As New InMemoryProductRepository()

        Dim product =
            CreateLowStockProduct(
                "PRD-00001")

        repository.Add(product)

        Dim service As New RestockQueueService(
                repository)

        service.Enqueue(product.ProductId)

        Dim duplicateResult =
            service.Enqueue(
                product.ProductId)

        Assert.IsFalse(
            duplicateResult.IsSuccess)

        Assert.AreEqual(
            1,
            service.PendingCount)
    End Sub

    <TestMethod>
    Public Sub ProcessNext_UsesFifoOrder()

        Dim repository As New InMemoryProductRepository()

        Dim first =
            CreateLowStockProduct(
                "PRD-00001")

        Dim second =
            CreateLowStockProduct(
                "PRD-00002")

        repository.Add(first)
        repository.Add(second)

        Dim service As New RestockQueueService(
                repository)

        service.Enqueue(first.ProductId)
        service.Enqueue(second.ProductId)

        Dim result =
            service.TryProcessNext()

        Assert.AreEqual(
            first.ProductId,
            result.Value.ProductId)
    End Sub

End Class

