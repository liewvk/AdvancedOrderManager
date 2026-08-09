Option Explicit On
Option Strict On
Option Infer On

Public NotInheritable Class OrderHistorySearchCriteria

    Public Sub New(
        customerName As String,
        status As String,
        priorityOnly As Boolean)

        Me.CustomerName =
            If(
                customerName,
                String.Empty).Trim()

        Me.Status =
            If(
                status,
                String.Empty).Trim()

        Me.PriorityOnly =
            priorityOnly
    End Sub

    Public ReadOnly Property CustomerName As String

    Public ReadOnly Property Status As String

    Public ReadOnly Property PriorityOnly As Boolean

End Class

