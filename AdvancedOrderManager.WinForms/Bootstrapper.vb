Option Explicit On
Option Strict On
Option Infer On

Imports AdvancedOrderManager.Application
Imports AdvancedOrderManager.Application.Contracts
Imports AdvancedOrderManager.Infrastructure

Friend NotInheritable Class Bootstrapper

    Private Shared ReadOnly _orderRepository As IOrderRepository =
        New InMemoryOrderRepository()

    Private Shared ReadOnly _customerRepository As ICustomerRepository =
        New InMemoryCustomerRepository()

    Private Sub New()
    End Sub

    Public Shared Function CreateMainForm() As MainForm

        Dim createOrderService As New CreateOrderService(
            _orderRepository)

        Return New MainForm(
            createOrderService,
            _orderRepository)

    End Function

    Public Shared Function CreateCustomerForm() As CustomerForm

        Dim registerCustomerService As New RegisterCustomerService(
            _customerRepository)

        Return New CustomerForm(
            registerCustomerService,
            _customerRepository)

    End Function
End Class