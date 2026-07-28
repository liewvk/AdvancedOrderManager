Option Explicit On
Option Strict On
Option Infer On

Imports AdvancedOrderManager.Application

Public NotInheritable Class ReportExportCommandFactory

    Private Sub New()
    End Sub

    Public Shared Function Create(
        format As ReportExportFormat,
        exporter As IOrderReportExporter) _
        As IReportExportCommand

        If exporter Is Nothing Then

            Throw New ArgumentNullException(
                NameOf(exporter))
        End If

        Select Case format

            Case ReportExportFormat.Csv

                Return New CsvReportExportCommand(
                    exporter)

            Case ReportExportFormat.Json

                Return New JsonReportExportCommand(
                    exporter)

            Case ReportExportFormat.Html

                Return New HtmlReportExportCommand(
                    exporter)

            Case Else

                Throw New ArgumentOutOfRangeException(
                    NameOf(format),
                    format,
                    "The export format is not supported.")
        End Select
    End Function

End Class
