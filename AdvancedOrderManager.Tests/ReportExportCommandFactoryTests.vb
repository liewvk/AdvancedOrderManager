Option Explicit On
Option Strict On
Option Infer On

Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports AdvancedOrderManager.Application
Imports AdvancedOrderManager.Infrastructure

<TestClass>
<TestCategory("Unit")>
Public Class ReportExportCommandFactoryTests

    <TestMethod>
    Public Sub Create_CsvFormat_ReturnsCsvCommand()

        Dim exporter =
            New OrderReportExporter()

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
            New OrderReportExporter()

        Dim command =
            ReportExportCommandFactory.Create(
                ReportExportFormat.Json,
                exporter)

        Assert.IsNotNull(
            command)

        Assert.AreEqual(
            "json",
            command.DefaultExtension)
    End Sub

    <TestMethod>
    Public Sub Create_HtmlFormat_ReturnsHtmlCommand()

        Dim exporter =
            New OrderReportExporter()

        Dim command =
            ReportExportCommandFactory.Create(
                ReportExportFormat.Html,
                exporter)

        Assert.IsNotNull(
            command)

        Assert.AreEqual(
            "html",
            command.DefaultExtension)
    End Sub

End Class

