Option Explicit On
Option Strict On
Option Infer On

Namespace Domain

    Public NotInheritable Class OrderLine

        Public Sub New(productName As String,
                       quantity As Integer,
                       unitPrice As Decimal)

            If String.IsNullOrWhiteSpace(productName) Then
                Throw New ArgumentException(
                    "A product name is required.",
                    NameOf(productName))
            End If

            If quantity <= 0 Then
                Throw New ArgumentOutOfRangeException(
                    NameOf(quantity),
                    "The quantity must be greater than zero.")
            End If

            If unitPrice < 0D Then
                Throw New ArgumentOutOfRangeException(
                    NameOf(unitPrice),
                    "The unit price cannot be negative.")
            End If

            Me.ProductName = productName.Trim()
            Me.Quantity = quantity
            Me.UnitPrice = unitPrice
        End Sub

        Public ReadOnly Property ProductName As String

        Public ReadOnly Property Quantity As Integer

        Public ReadOnly Property UnitPrice As Decimal

        Public ReadOnly Property LineTotal As Decimal
            Get
                Return Quantity * UnitPrice
            End Get
        End Property

    End Class

End Namespace

