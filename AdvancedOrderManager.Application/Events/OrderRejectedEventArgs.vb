Option Explicit On
Option Strict On
Option Infer On

Public NotInheritable Class OrderRejectedEventArgs
    Inherits EventArgs

    Public Sub New(
        orderNumber As String,
        reason As String,
        rejectedAtUtc As DateTimeOffset)

        Me.OrderNumber =
            If(orderNumber, String.Empty)

        Me.Reason =
            If(reason, String.Empty)

        Me.RejectedAtUtc =
            rejectedAtUtc
    End Sub

    Public ReadOnly Property OrderNumber As String

    Public ReadOnly Property Reason As String

    Public ReadOnly Property RejectedAtUtc As DateTimeOffset

End Class
