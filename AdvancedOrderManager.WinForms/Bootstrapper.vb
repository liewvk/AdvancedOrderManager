Option Explicit On
Option Strict On
Option Infer On

Imports AdvancedOrderManager.Application
Imports AdvancedOrderManager.Application.Contracts
Imports AdvancedOrderManager.Infrastructure

Friend NotInheritable Class Bootstrapper

    Private Sub New()
    End Sub

    Public Shared Function CreateMainForm() As MainForm

        Dim repository As IOrderRepository =
            New InMemoryOrderRepository()

        Dim createOrderService As New CreateOrderService(repository)

        Return New MainForm(createOrderService, repository)

    End Function

End Class