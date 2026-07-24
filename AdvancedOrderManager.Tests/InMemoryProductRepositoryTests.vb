Option Explicit On
Option Strict On
Option Infer On

Imports AdvancedOrderManager.Domain
Imports AdvancedOrderManager.Infrastructure
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass>
Public Class InMemoryProductRepositoryTests

    Private Shared Function CreateProduct(
        code As String,
        category As String) As Product

        Return New Product(
            ProductId.NewId(),
            ProductCode.Create(code),
            "Test Product",
            category,
            10D,
            5,
            2)
    End Function

    <TestMethod>
    Public Sub Add_IndexesProductByCode()
        Dim repository As New InMemoryProductRepository()

        Dim product =
            CreateProduct(
                "PRD-00001",
                "Hardware")

        repository.Add(product)

        Dim found =
            repository.GetByCode(
                ProductCode.Create(
                    "PRD-00001"))

        Assert.IsNotNull(found)
        Assert.AreEqual(
            product.ProductId,
            found.ProductId)
    End Sub

    <TestMethod>
    Public Sub Add_DuplicateCode_ThrowsException()

        Dim repository As New InMemoryProductRepository()

        repository.Add(
            CreateProduct(
                "PRD-00001",
                "Hardware"))

        Assert.Throws(Of InvalidOperationException)(
            Sub()
                repository.Add(
                    CreateProduct(
                        "PRD-00001",
                        "Software"))
            End Sub)
    End Sub

    <TestMethod>
    Public Sub Categories_AreUniqueAndSorted()

        Dim repository As New InMemoryProductRepository()

        repository.Add(
            CreateProduct(
                "PRD-00001",
                "Software"))

        repository.Add(
            CreateProduct(
                "PRD-00002",
                "Hardware"))

        repository.Add(
            CreateProduct(
                "PRD-00003",
                "software"))

        Dim categories =
            repository.GetCategories()

        Assert.HasCount(2, categories)
        Assert.AreEqual(
            "Hardware",
            categories(0))
        Assert.AreEqual(
            "Software",
            categories(1))
    End Sub

End Class

