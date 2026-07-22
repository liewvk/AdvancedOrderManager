Option Explicit On
Option Strict On
Option Infer On

Namespace Application

    Public NotInheritable Class CreateOrderRequest

        Public Sub New(
            customerName As String,
            lines As IReadOnlyCollection(Of OrderLineRequest))

            Me.CustomerName = customerName
            Me.Lines = lines
        End Sub

        Public ReadOnly Property CustomerName As String

        Public ReadOnly Property Lines As IReadOnlyCollection(Of OrderLineRequest)

    End Class

End Namespace

