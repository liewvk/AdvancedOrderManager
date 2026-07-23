Option Explicit On
Option Strict On
Option Infer On

Imports AdvancedOrderManager.Application
Imports AdvancedOrderManager.Infrastructure
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass>
Public Class RegisterCustomerServiceTests

    Private Shared Function CreateRequest(
        email As String) As RegisterCustomerRequest

        Return New RegisterCustomerRequest(
            "Alice",
            "Tan",
            email,
            "20 Jalan Ampang",
            "Kuala Lumpur",
            "50450",
            "Malaysia")
    End Function

    <TestMethod>
    Public Sub Execute_ValidRequest_RegistersCustomer()

        Dim repository As New InMemoryCustomerRepository()

        Dim service As New RegisterCustomerService(repository)

        Dim result =
            service.Execute(
                CreateRequest(
                    "alice@example.com"))

        Assert.IsTrue(result.IsSuccess)
        Assert.IsNotNull(result.Value)
        Assert.HasCount(1, repository.GetAll())
    End Sub

    <TestMethod>
    Public Sub Execute_DuplicateEmail_ReturnsFailure()

        Dim repository As New InMemoryCustomerRepository()

        Dim service As New RegisterCustomerService(repository)

        Dim firstResult =
            service.Execute(
                CreateRequest(
                    "Alice@Example.com"))

        Dim secondResult =
            service.Execute(
                CreateRequest(
                    "alice@example.com"))

        Assert.IsTrue(firstResult.IsSuccess)
        Assert.IsFalse(secondResult.IsSuccess)

        StringAssert.Contains(
            secondResult.ErrorMessage,
            "already registered")

        Assert.HasCount(1, repository.GetAll())
    End Sub

End Class

