Option Explicit On
Option Strict On
Option Infer On

Public NotInheritable Class OrderAuditSubscriber

    Private ReadOnly _messages As New List(Of String)()

    Public Function GetMessages() _
        As IReadOnlyList(Of String)

        Return _messages _
            .ToList() _
            .AsReadOnly()
    End Function

    Public Sub HandleOrderProcessed(
        sender As Object,
        e As OrderProcessedEventArgs)

        _messages.Add(
            $"{e.ProcessedAtUtc:O} | " &
            $"PROCESSED | " &
            $"{e.OrderNumber} | " &
            $"{e.CustomerName} | " &
            $"{e.TotalAmount:F2}")
    End Sub

    Public Sub HandleOrderRejected(
        sender As Object,
        e As OrderRejectedEventArgs)

        _messages.Add(
            $"{e.RejectedAtUtc:O} | " &
            $"REJECTED | " &
            $"{e.OrderNumber} | " &
            $"{e.Reason}")
    End Sub

End Class

