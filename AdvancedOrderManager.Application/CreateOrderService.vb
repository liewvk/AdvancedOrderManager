Option Explicit On
Option Strict On
Option Infer On

Imports AdvancedOrderManager.Application.Contracts
Imports AdvancedOrderManager.Domain

Namespace Application

    Public NotInheritable Class CreateOrderService

        Private ReadOnly _repository As IOrderRepository

        Public Sub New(repository As IOrderRepository)

            If repository Is Nothing Then
                Throw New ArgumentNullException(
                    NameOf(repository))
            End If

            _repository = repository
        End Sub

        Public Function Execute(
            request As CreateOrderRequest) As Order

            If request Is Nothing Then
                Throw New ArgumentNullException(
                    NameOf(request))
            End If

            If request.Lines Is Nothing OrElse
               request.Lines.Count = 0 Then

                Throw New InvalidOperationException(
                    "An order must contain at least one line.")
            End If

            Dim order As New Order(request.CustomerName)

            For Each requestedLine In request.Lines

                Dim line As New OrderLine(
                    requestedLine.ProductName,
                    requestedLine.Quantity,
                    requestedLine.UnitPrice)

                order.AddLine(line)
            Next

            _repository.Add(order)

            Return order
        End Function

    End Class

End Namespace


