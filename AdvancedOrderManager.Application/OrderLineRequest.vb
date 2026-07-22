Option Explicit On
Option Strict On
Option Infer On

Namespace Application

    Public NotInheritable Class OrderLineRequest

        Public Sub New(productName As String,
                       quantity As Integer,
                       unitPrice As Decimal)

            Me.ProductName = productName
            Me.Quantity = quantity
            Me.UnitPrice = unitPrice
        End Sub

        Public ReadOnly Property ProductName As String

        Public ReadOnly Property Quantity As Integer

        Public ReadOnly Property UnitPrice As Decimal

    End Class

End Namespace

