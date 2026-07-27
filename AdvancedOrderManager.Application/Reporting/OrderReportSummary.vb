Option Explicit On
Option Strict On
Option Infer On

Imports System.Collections.Generic
Imports System.Linq

Public NotInheritable Class OrderReportSummary

    Private Sub New(
        totalRecords As Integer,
        processedCount As Integer,
        rejectedCount As Integer,
        totalRevenue As Decimal,
        averageProcessedValue As Decimal)

        Me.TotalRecords = totalRecords
        Me.ProcessedCount = processedCount
        Me.RejectedCount = rejectedCount
        Me.TotalRevenue = totalRevenue

        Me.AverageProcessedValue =
            averageProcessedValue
    End Sub

    Public ReadOnly Property TotalRecords As Integer

    Public ReadOnly Property ProcessedCount As Integer

    Public ReadOnly Property RejectedCount As Integer

    Public ReadOnly Property TotalRevenue As Decimal

    Public ReadOnly Property AverageProcessedValue As Decimal

    Public Shared Function Create(
        records As IEnumerable(Of OrderReportRecord)) _
        As OrderReportSummary

        If records Is Nothing Then

            Throw New ArgumentNullException(
                NameOf(records))
        End If

        Dim recordList =
            records.ToList()

        Dim processedRecords =
            recordList _
                .Where(
                    Function(record)

                        Return record.Status =
                               OrderReportStatus.Processed
                    End Function) _
                .ToList()

        Dim totalRevenue =
            processedRecords.Sum(
                Function(record)

                    Return record.TotalAmount
                End Function)

        Dim averageValue =
            If(
                processedRecords.Count = 0,
                0D,
                totalRevenue /
                processedRecords.Count)

        Return New OrderReportSummary(
            recordList.Count,
            processedRecords.Count,
            recordList.Count -
                processedRecords.Count,
            totalRevenue,
            averageValue)
    End Function

End Class

