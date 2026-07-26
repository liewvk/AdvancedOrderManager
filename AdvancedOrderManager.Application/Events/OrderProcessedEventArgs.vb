Option Explicit On
Option Strict On
Option Infer On

Public NotInheritable Class OrderProcessedEventArgs
    Inherits EventArgs

    Public Sub New(
        orderNumber As String,
        customerName As String,
        totalAmount As Decimal,
        isPriority As Boolean,
        processedAtUtc As DateTimeOffset)

        Me.OrderNumber =
            orderNumber

        Me.CustomerName =
            customerName

        Me.TotalAmount =
            totalAmount

        Me.IsPriority =
            isPriority

        Me.ProcessedAtUtc =
            processedAtUtc
    End Sub

    Public ReadOnly Property OrderNumber As String

    Public ReadOnly Property CustomerName As String

    Public ReadOnly Property TotalAmount As Decimal

    Public ReadOnly Property IsPriority As Boolean

    Public ReadOnly Property ProcessedAtUtc As DateTimeOffset

End Class

