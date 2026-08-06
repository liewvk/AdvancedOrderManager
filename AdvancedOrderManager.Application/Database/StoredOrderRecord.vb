Option Explicit On
Option Strict On
Option Infer On

Public NotInheritable Class StoredOrderRecord

    Public Sub New(
        orderId As String,
        customerName As String,
        quantity As Integer,
        unitPrice As Decimal,
        isPriority As Boolean,
        totalAmount As Decimal,
        status As String,
        processedAt As DateTimeOffset)

        If String.IsNullOrWhiteSpace(orderId) Then
            Throw New ArgumentException(
                "An order ID is required.",
                NameOf(orderId))
        End If

        If String.IsNullOrWhiteSpace(customerName) Then
            Throw New ArgumentException(
                "A customer name is required.",
                NameOf(customerName))
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

        If totalAmount < 0D Then
            Throw New ArgumentOutOfRangeException(
                NameOf(totalAmount),
                "The total amount cannot be negative.")
        End If

        If String.IsNullOrWhiteSpace(status) Then
            Throw New ArgumentException(
                "A status is required.",
                NameOf(status))
        End If

        Me.OrderId = orderId.Trim()
        Me.CustomerName = customerName.Trim()
        Me.Quantity = quantity
        Me.UnitPrice = unitPrice
        Me.IsPriority = isPriority
        Me.TotalAmount = totalAmount
        Me.Status = status.Trim()
        Me.ProcessedAt = processedAt
    End Sub

    Public ReadOnly Property OrderId As String

    Public ReadOnly Property CustomerName As String

    Public ReadOnly Property Quantity As Integer

    Public ReadOnly Property UnitPrice As Decimal

    Public ReadOnly Property IsPriority As Boolean

    Public ReadOnly Property TotalAmount As Decimal

    Public ReadOnly Property Status As String

    Public ReadOnly Property ProcessedAt As DateTimeOffset

End Class

