Option Explicit On
Option Strict On
Option Infer On

Public NotInheritable Class OrderReportRecord

    Public Sub New(
        orderNumber As String,
        customerName As String,
        status As OrderReportStatus,
        totalAmount As Decimal,
        isPriority As Boolean,
        message As String,
        occurredAtUtc As DateTimeOffset)

        Me.OrderNumber =
            If(orderNumber, String.Empty).Trim()

        Me.CustomerName =
            If(customerName, String.Empty).Trim()

        Me.Status = status
        Me.TotalAmount = totalAmount
        Me.IsPriority = isPriority

        Me.Message =
            If(message, String.Empty).Trim()

        Me.OccurredAtUtc =
            occurredAtUtc
    End Sub

    Public ReadOnly Property OrderNumber As String

    Public ReadOnly Property CustomerName As String

    Public ReadOnly Property Status As OrderReportStatus

    Public ReadOnly Property TotalAmount As Decimal

    Public ReadOnly Property IsPriority As Boolean

    Public ReadOnly Property Message As String

    Public ReadOnly Property OccurredAtUtc As DateTimeOffset

End Class

