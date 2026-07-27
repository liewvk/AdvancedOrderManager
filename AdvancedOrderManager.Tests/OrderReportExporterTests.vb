Option Explicit On
Option Strict On
Option Infer On

Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports AdvancedOrderManager.Application
Imports AdvancedOrderManager.Infrastructure
Imports System.IO
Imports System.Threading

<TestClass>
<TestCategory("Integration")>
Public Class OrderReportExporterTests

    <TestMethod>
    Public Async Function ExportCsvAsync_CommaInCustomerName_QuotesField() _
        As Task

        Dim temporaryPath =
            Path.Combine(
                Path.GetTempPath(),
                $"order-report-{Guid.NewGuid():N}.csv")

        Try
            Dim records As IReadOnlyList(Of OrderReportRecord) =
                    New List(Of OrderReportRecord) From {
                        New OrderReportRecord(
                            "ORD-CSV-1",
                            "Tan, Alice",
                            OrderReportStatus.Processed,
                            100D,
                            False,
                            "Completed",
                            DateTimeOffset.UtcNow)
                    }.AsReadOnly()

            Dim exporter =
                New OrderReportExporter()

            Await exporter.ExportCsvAsync(
                records,
                temporaryPath,
                CancellationToken.None)

            Dim contents =
                Await File.ReadAllTextAsync(
                    temporaryPath)

            StringAssert.Contains(
                contents,
                """Tan, Alice""")

        Finally
            If File.Exists(
                temporaryPath) Then

                File.Delete(
                    temporaryPath)
            End If
        End Try
    End Function

End Class

