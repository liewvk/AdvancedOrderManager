Option Explicit On
Option Strict On
Option Infer On

Public NotInheritable Class OrderSubmission

    Public Sub New(
        orderNumber As String,
        customerName As String,
        quantity As Integer,
        unitPrice As Decimal,
        isPriority As Boolean)

        Me.OrderNumber =
            If(orderNumber, String.Empty).Trim()

        Me.CustomerName =
            If(customerName, String.Empty).Trim()

        Me.Quantity =
            quantity

        Me.UnitPrice =
            unitPrice

        Me.IsPriority =
            isPriority
    End Sub

    Public ReadOnly Property OrderNumber As String

    Public ReadOnly Property CustomerName As String

    Public ReadOnly Property Quantity As Integer

    Public ReadOnly Property UnitPrice As Decimal

    Public ReadOnly Property IsPriority As Boolean

    Public ReadOnly Property Subtotal As Decimal
        Get
            Return Quantity * UnitPrice
        End Get
    End Property

End Class

