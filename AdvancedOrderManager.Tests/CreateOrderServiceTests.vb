Option Explicit On
Option Strict On
Option Infer On

Imports AdvancedOrderManager.Application
Imports AdvancedOrderManager.Infrastructure
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass>
Public Class CreateOrderServiceTests

    <TestMethod>
    Public Sub Execute_ValidRequest_SavesOrder()

        Dim repository As New InMemoryOrderRepository()

        Dim service As New CreateOrderService(repository)

        Dim lines As New List(Of OrderLineRequest) From {
            New OrderLineRequest(
                "Monitor",
                2,
                850D),
            New OrderLineRequest(
                "Display Cable",
                3,
                35D)
        }

        Dim request As New CreateOrderRequest(
            "Ben Lee",
            lines)

        Dim createdOrder = service.Execute(request)

        Dim savedOrders = repository.GetAll()

        Assert.HasCount(1, savedOrders)
        Assert.AreEqual(
            createdOrder.OrderId,
            savedOrders(0).OrderId)

        Assert.AreEqual(1805D, createdOrder.Total)
    End Sub

    <TestMethod>
    Public Sub Execute_NoLines_ThrowsException()

        Dim repository As New InMemoryOrderRepository()

        Dim service As New CreateOrderService(repository)

        Dim request As New CreateOrderRequest(
            "Ben Lee",
            New List(Of OrderLineRequest)())

        Assert.ThrowsExactly(Of InvalidOperationException)(
            Sub()
                service.Execute(request)
            End Sub)
    End Sub

End Class

