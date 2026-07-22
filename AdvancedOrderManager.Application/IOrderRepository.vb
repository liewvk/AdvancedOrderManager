Option Explicit On
Option Strict On
Option Infer On

Imports AdvancedOrderManager.Domain

Namespace Application.Contracts

    Public Interface IOrderRepository

        Sub Add(order As Order)
        Function GetAll() As IReadOnlyCollection(Of Order)

    End Interface

End Namespace

