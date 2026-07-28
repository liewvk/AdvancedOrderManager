Option Explicit On
Option Strict On
Option Infer On

Imports System.Collections.Generic
Imports System.Threading
Imports System.Threading.Tasks
Imports AdvancedOrderManager.Application

Public NotInheritable Class CsvReportExportCommand
    Implements IReportExportCommand

    Private ReadOnly _exporter As IOrderReportExporter

    Public Sub New(
        exporter As IOrderReportExporter)

        If exporter Is Nothing Then

            Throw New ArgumentNullException(
                NameOf(exporter))
        End If

        _exporter = exporter
    End Sub

    Public ReadOnly Property FileFilter As String _
        Implements IReportExportCommand.FileFilter

        Get
            Return "CSV files|*.csv"
        End Get
    End Property

    Public ReadOnly Property DefaultExtension As String _
        Implements IReportExportCommand.DefaultExtension

        Get
            Return "csv"
        End Get
    End Property

    Public Function ExecuteAsync(
        records As IReadOnlyList(
            Of OrderReportRecord),
        filePath As String,
        cancellationToken As CancellationToken) _
        As Task _
        Implements IReportExportCommand.ExecuteAsync

        Return _exporter.ExportCsvAsync(
            records,
            filePath,
            cancellationToken)
    End Function

End Class

