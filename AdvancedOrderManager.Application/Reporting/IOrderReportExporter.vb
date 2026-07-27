Option Explicit On
Option Strict On
Option Infer On

Imports System.Collections.Generic
Imports System.Threading
Imports System.Threading.Tasks

Public Interface IOrderReportExporter

    Function ExportCsvAsync(
        records As IReadOnlyList(Of OrderReportRecord),
        filePath As String,
        cancellationToken As CancellationToken) _
        As Task

    Function ExportJsonAsync(
        records As IReadOnlyList(Of OrderReportRecord),
        filePath As String,
        cancellationToken As CancellationToken) _
        As Task

    Function ExportHtmlAsync(
        records As IReadOnlyList(Of OrderReportRecord),
        filePath As String,
        cancellationToken As CancellationToken) _
        As Task

End Interface

