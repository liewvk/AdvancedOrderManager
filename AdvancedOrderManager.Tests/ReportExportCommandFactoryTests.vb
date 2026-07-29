Option Explicit On
Option Strict On
Option Infer On

Imports AdvancedOrderManager.Application
Imports AdvancedOrderManager.Infrastructure
Imports Microsoft.Extensions.Logging.Abstractions
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass>
<TestCategory("Unit")>
Public Class ReportExportCommandFactoryTests

    Private Shared Function CreateExporter() _
        As OrderReportExporter

        Return New OrderReportExporter(
            NullLogger(
                Of OrderReportExporter).Instance)
    End Function

    <TestMethod>
    Public Sub Create_CsvFormat_ReturnsCsvCommand()

        Dim exporter =
            CreateExporter()

        Dim command =
            ReportExportCommandFactory.Create(
                ReportExportFormat.Csv,
                exporter)

        Assert.IsNotNull(
            command)

        Assert.AreEqual(
            "csv",
            command.DefaultExtension)

        Assert.AreEqual(
            "CSV files|*.csv",
            command.FileFilter)
    End Sub

    <TestMethod>
    Public Sub Create_JsonFormat_ReturnsJsonCommand()

        Dim exporter =
            CreateExporter()

        Dim command =
            ReportExportCommandFactory.Create(
                ReportExportFormat.Json,
                exporter)

        Assert.IsNotNull(
            command)

        Assert.AreEqual(
            "json",
            command.DefaultExtension)

        Assert.AreEqual(
            "JSON files|*.json",
            command.FileFilter)
    End Sub

    <TestMethod>
    Public Sub Create_HtmlFormat_ReturnsHtmlCommand()

        Dim exporter =
            CreateExporter()

        Dim command =
            ReportExportCommandFactory.Create(
                ReportExportFormat.Html,
                exporter)

        Assert.IsNotNull(
            command)

        Assert.AreEqual(
            "html",
            command.DefaultExtension)

        Assert.AreEqual(
            "HTML files|*.html",
            command.FileFilter)
    End Sub

End Class