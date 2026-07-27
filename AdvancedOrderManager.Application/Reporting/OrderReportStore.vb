Option Explicit On
Option Strict On
Option Infer On

Imports System.Collections.Generic
Imports System.Linq

Public NotInheritable Class OrderReportStore

    Private ReadOnly _syncRoot As New Object()

    Private ReadOnly _records As New List(Of OrderReportRecord)()

    Public Sub HandleOrderProcessed(
        sender As Object,
        e As OrderProcessedEventArgs)

        If e Is Nothing Then

            Throw New ArgumentNullException(
                NameOf(e))
        End If

        Dim record =
            New OrderReportRecord(
                e.OrderNumber,
                e.CustomerName,
                OrderReportStatus.Processed,
                e.TotalAmount,
                e.IsPriority,
                "Order processed successfully.",
                e.ProcessedAtUtc)

        SyncLock _syncRoot
            _records.Add(record)
        End SyncLock
    End Sub

    Public Sub HandleOrderRejected(
        sender As Object,
        e As OrderRejectedEventArgs)

        If e Is Nothing Then

            Throw New ArgumentNullException(
                NameOf(e))
        End If

        Dim record =
            New OrderReportRecord(
                e.OrderNumber,
                String.Empty,
                OrderReportStatus.Rejected,
                0D,
                False,
                e.Reason,
                e.RejectedAtUtc)

        SyncLock _syncRoot
            _records.Add(record)
        End SyncLock
    End Sub

    Public Function GetSnapshot() _
        As IReadOnlyList(Of OrderReportRecord)

        SyncLock _syncRoot

            Return _records _
                .OrderByDescending(
                    Function(record)

                        Return record.OccurredAtUtc
                    End Function) _
                .ToList() _
                .AsReadOnly()
        End SyncLock
    End Function

    Public Sub Clear()

        SyncLock _syncRoot
            _records.Clear()
        End SyncLock
    End Sub

End Class

