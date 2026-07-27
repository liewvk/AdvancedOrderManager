Option Explicit On
Option Strict On
Option Infer On

Imports System.Collections.Generic
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Threading
Imports System.Linq
Imports AdvancedOrderManager.Application
Imports AdvancedOrderManager.Infrastructure


Public Class OrderReportForm

    Private ReadOnly _reportStore As OrderReportStore

    Private ReadOnly _exporter As IOrderReportExporter

    Private ReadOnly _printDocument As New PrintDocument()

    Private _currentRecords As IReadOnlyList(Of OrderReportRecord) =
            New List(Of OrderReportRecord)() _
                .AsReadOnly()

    Private _printRecords As IReadOnlyList(Of OrderReportRecord) =
            New List(Of OrderReportRecord)() _
                .AsReadOnly()

    Private _printRecordIndex As Integer

    Public Sub New()

        Me.New(
            New OrderReportStore(),
            New OrderReportExporter())
    End Sub

    Public Sub New(
        reportStore As OrderReportStore,
        exporter As IOrderReportExporter)

        InitializeComponent()

        If reportStore Is Nothing Then

            Throw New ArgumentNullException(
                NameOf(reportStore))
        End If

        If exporter Is Nothing Then

            Throw New ArgumentNullException(
                NameOf(exporter))
        End If

        _reportStore = reportStore
        _exporter = exporter

        AddHandler _printDocument.BeginPrint,
            AddressOf PrintDocument_BeginPrint

        AddHandler _printDocument.PrintPage,
            AddressOf PrintDocument_PrintPage
    End Sub
    Private Sub OrderReportForm_Load(
        sender As Object,
        e As EventArgs) _
        Handles MyBase.Load

        cboReportStatus.Items.AddRange(
            New Object() {
                "All",
                "Processed",
                "Rejected"
            })

        cboReportStatus.SelectedIndex = 0

        RefreshReport()
    End Sub

    Private Sub RefreshReport()

        Dim records =
            _reportStore.GetSnapshot()

        Dim selectedStatus =
            Convert.ToString(
                cboReportStatus.SelectedItem)

        Dim searchText =
            txtReportSearch.Text.Trim()

        Dim query =
            records.AsEnumerable()

        If String.Equals(
            selectedStatus,
            "Processed",
            StringComparison.OrdinalIgnoreCase) Then

            query =
                query.Where(
                    Function(record)

                        Return record.Status =
                               OrderReportStatus.Processed
                    End Function)

        ElseIf String.Equals(
            selectedStatus,
            "Rejected",
            StringComparison.OrdinalIgnoreCase) Then

            query =
                query.Where(
                    Function(record)

                        Return record.Status =
                               OrderReportStatus.Rejected
                    End Function)
        End If

        If searchText.Length > 0 Then

            query =
                query.Where(
                    Function(record)

                        Return ContainsText(
                                   record.OrderNumber,
                                   searchText) OrElse
                               ContainsText(
                                   record.CustomerName,
                                   searchText) OrElse
                               ContainsText(
                                   record.Message,
                                   searchText)
                    End Function)
        End If

        _currentRecords =
            query _
                .OrderByDescending(
                    Function(record)

                        Return record.OccurredAtUtc
                    End Function) _
                .ToList() _
                .AsReadOnly()

        dgvOrderReport.DataSource =
            _currentRecords.ToList()

        ConfigureGridColumns()
        UpdateSummary()
    End Sub

    Private Shared Function ContainsText(
        source As String,
        searchText As String) As Boolean

        Return If(
            source,
            String.Empty) _
            .IndexOf(
                searchText,
                StringComparison.OrdinalIgnoreCase) >= 0
    End Function
    Private Sub ConfigureGridColumns()

        If dgvOrderReport.Columns.Count = 0 Then
            Return
        End If

        dgvOrderReport.Columns(
            NameOf(
                OrderReportRecord.OrderNumber)) _
            .HeaderText =
                "Order Number"

        dgvOrderReport.Columns(
            NameOf(
                OrderReportRecord.CustomerName)) _
            .HeaderText =
                "Customer"

        dgvOrderReport.Columns(
            NameOf(
                OrderReportRecord.TotalAmount)) _
            .HeaderText =
                "Total"

        dgvOrderReport.Columns(
            NameOf(
                OrderReportRecord.IsPriority)) _
            .HeaderText =
                "Priority"

        dgvOrderReport.Columns(
            NameOf(
                OrderReportRecord.OccurredAtUtc)) _
            .HeaderText =
                "Occurred At"

        dgvOrderReport.Columns(
            NameOf(
                OrderReportRecord.TotalAmount)) _
            .DefaultCellStyle.Format =
                "N2"

        dgvOrderReport.Columns(
            NameOf(
                OrderReportRecord.OccurredAtUtc)) _
            .DefaultCellStyle.Format =
                "dd MMM yyyy HH:mm:ss"
    End Sub
    Private Sub UpdateSummary()

        Dim summary =
            OrderReportSummary.Create(
                _currentRecords)

        lblReportRecords.Text =
            $"Records: {summary.TotalRecords:N0}"

        lblReportProcessed.Text =
            $"Processed: {summary.ProcessedCount:N0}"

        lblReportRejected.Text =
            $"Rejected: {summary.RejectedCount:N0}"

        lblReportRevenue.Text =
            $"Revenue: RM{summary.TotalRevenue:N2}"

        lblReportAverage.Text =
            $"Average: RM{summary.AverageProcessedValue:N2}"

        lblReportStatus.Text =
            If(
                summary.TotalRecords = 0,
                "No report records match the filter.",
                "Report ready.")
    End Sub

    Private Sub btnRefreshReport_Click(
        sender As Object,
        e As EventArgs) _
        Handles btnRefreshReport.Click

        RefreshReport()
    End Sub

    Private Sub cboReportStatus_SelectedIndexChanged(
        sender As Object,
        e As EventArgs) _
        Handles cboReportStatus.SelectedIndexChanged

        If IsHandleCreated Then
            RefreshReport()
        End If
    End Sub

    Private Sub txtReportSearch_KeyDown(
        sender As Object,
        e As KeyEventArgs) _
        Handles txtReportSearch.KeyDown

        If e.KeyCode = Keys.Enter Then

            RefreshReport()

            e.SuppressKeyPress = True
        End If
    End Sub
    Private Async Sub btnExportCsv_Click(
            sender As Object,
            e As EventArgs) _
            Handles btnExportCsv.Click

        Await ExportAsync(
            "CSV files|*.csv",
            "csv",
            Function(path)

                Return _exporter.ExportCsvAsync(
                    _currentRecords,
                    path,
                    CancellationToken.None)
            End Function)
    End Sub
    Private Async Sub btnExportJson_Click(
        sender As Object,
        e As EventArgs) _
        Handles btnExportJson.Click

        Await ExportAsync(
            "JSON files|*.json",
            "json",
            Function(path)

                Return _exporter.ExportJsonAsync(
                    _currentRecords,
                    path,
                    CancellationToken.None)
            End Function)
    End Sub
    Private Async Sub btnExportHtml_Click(
        sender As Object,
        e As EventArgs) _
        Handles btnExportHtml.Click

        Await ExportAsync(
            "HTML files|*.html",
            "html",
            Function(path)

                Return _exporter.ExportHtmlAsync(
                    _currentRecords,
                    path,
                    CancellationToken.None)
            End Function)
    End Sub
    Private Async Function ExportAsync(
        filter As String,
        defaultExtension As String,
        exportOperation As Func(Of String, Task)) _
        As Task

        If _currentRecords.Count = 0 Then

            MessageBox.Show(
                Me,
                "There are no report records to export.",
                "Report Export",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information)

            Return
        End If

        Using dialog As New SaveFileDialog()

            dialog.Filter = filter

            dialog.DefaultExt =
                defaultExtension

            dialog.AddExtension = True

            dialog.OverwritePrompt = True

            dialog.FileName =
                $"order-report-" &
                $"{DateTime.Now:yyyyMMdd-HHmmss}." &
                $"{defaultExtension}"

            If dialog.ShowDialog(Me) <>
               DialogResult.OK Then

                Return
            End If

            Try
                ToggleExportControls(
                    enabled:=False)

                lblReportStatus.Text =
                    "Exporting report..."

                Await exportOperation(
                    dialog.FileName)

                lblReportStatus.Text =
                    $"Report exported to {dialog.FileName}"

            Catch ex As Exception

                lblReportStatus.Text =
                    "The report could not be exported."

                MessageBox.Show(
                    Me,
                    ex.Message,
                    "Report Export",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error)

            Finally
                ToggleExportControls(
                    enabled:=True)
            End Try
        End Using
    End Function

    Private Sub ToggleExportControls(
        enabled As Boolean)

        btnExportCsv.Enabled = enabled
        btnExportJson.Enabled = enabled
        btnExportHtml.Enabled = enabled
        btnPrintPreview.Enabled = enabled
    End Sub
    Private Sub btnPrintPreview_Click(
        sender As Object,
        e As EventArgs) _
        Handles btnPrintPreview.Click

        If _currentRecords.Count = 0 Then

            MessageBox.Show(
                Me,
                "There are no records to print.",
                "Print Preview",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information)

            Return
        End If

        _printRecords =
            _currentRecords

        Using preview As New PrintPreviewDialog()

            preview.Document =
                _printDocument

            preview.Width = 1100
            preview.Height = 750

            preview.ShowDialog(Me)
        End Using
    End Sub

    Private Sub PrintDocument_BeginPrint(
        sender As Object,
        e As PrintEventArgs)

        _printRecordIndex = 0
    End Sub
    Private Sub PrintDocument_PrintPage(
        sender As Object,
        e As PrintPageEventArgs)

        Dim graphics =
            e.Graphics

        If graphics Is Nothing Then
            Return
        End If

        Dim left =
            CSng(
                e.MarginBounds.Left)

        Dim top =
            CSng(
                e.MarginBounds.Top)

        Dim bottom =
            CSng(
                e.MarginBounds.Bottom)

        Dim currentY =
            top

        Const RowHeight As Single =
            24.0F

        Using titleFont As New Font(
                "Segoe UI",
                16.0F,
                FontStyle.Bold)

            Using headingFont As New Font(
                    "Segoe UI",
                    9.0F,
                    FontStyle.Bold)

                Using bodyFont As New Font(
                        "Segoe UI",
                        9.0F,
                        FontStyle.Regular)

                    graphics.DrawString(
                        "Order Processing Report",
                        titleFont,
                        Brushes.Black,
                        left,
                        currentY)

                    currentY += 36.0F

                    graphics.DrawString(
                        $"Generated: " &
                        $"{DateTime.Now:dd MMM yyyy HH:mm:ss}",
                        bodyFont,
                        Brushes.Black,
                        left,
                        currentY)

                    currentY += 30.0F

                    DrawPrintHeadings(
                        graphics,
                        headingFont,
                        left,
                        currentY)

                    currentY += RowHeight

                    While _printRecordIndex <
                          _printRecords.Count AndAlso
                          currentY + RowHeight <= bottom

                        DrawPrintRecord(
                            graphics,
                            bodyFont,
                            _printRecords(
                                _printRecordIndex),
                            left,
                            currentY)

                        _printRecordIndex += 1
                        currentY += RowHeight
                    End While
                End Using
            End Using
        End Using

        e.HasMorePages =
            _printRecordIndex <
            _printRecords.Count
    End Sub
    Private Shared Sub DrawPrintHeadings(
        graphics As Graphics,
        font As Font,
        left As Single,
        top As Single)

        graphics.DrawString(
            "Date",
            font,
            Brushes.Black,
            left,
            top)

        graphics.DrawString(
            "Order",
            font,
            Brushes.Black,
            left + 125.0F,
            top)

        graphics.DrawString(
            "Customer",
            font,
            Brushes.Black,
            left + 235.0F,
            top)

        graphics.DrawString(
            "Status",
            font,
            Brushes.Black,
            left + 410.0F,
            top)

        graphics.DrawString(
            "Total",
            font,
            Brushes.Black,
            left + 500.0F,
            top)
    End Sub

    Private Shared Sub DrawPrintRecord(
        graphics As Graphics,
        font As Font,
        record As OrderReportRecord,
        left As Single,
        top As Single)

        graphics.DrawString(
            record.OccurredAtUtc _
                .ToLocalTime() _
                .ToString(
                    "dd MMM HH:mm"),
            font,
            Brushes.Black,
            left,
            top)

        graphics.DrawString(
            LimitText(
                record.OrderNumber,
                16),
            font,
            Brushes.Black,
            left + 125.0F,
            top)

        graphics.DrawString(
            LimitText(
                record.CustomerName,
                24),
            font,
            Brushes.Black,
            left + 235.0F,
            top)

        graphics.DrawString(
            record.Status.ToString(),
            font,
            Brushes.Black,
            left + 410.0F,
            top)

        Dim totalText =
            If(
                record.Status =
                OrderReportStatus.Processed,
                $"RM{record.TotalAmount:N2}",
                String.Empty)

        graphics.DrawString(
            totalText,
            font,
            Brushes.Black,
            left + 500.0F,
            top)
    End Sub

    Private Shared Function LimitText(
        value As String,
        maximumLength As Integer) As String

        Dim safeValue =
            If(value, String.Empty)

        If safeValue.Length <= maximumLength Then
            Return safeValue
        End If

        Return safeValue.Substring(
            0,
            maximumLength - 3) &
            "..."
    End Function
    Private Sub btnClearReport_Click(
        sender As Object,
        e As EventArgs) _
        Handles btnClearReport.Click

        Dim answer =
            MessageBox.Show(
                Me,
                "Clear all report records from the " &
                "current application session?",
                "Clear Report",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning)

        If answer <>
           DialogResult.Yes Then

            Return
        End If

        _reportStore.Clear()

        RefreshReport()

        lblReportStatus.Text =
            "Report records cleared."
    End Sub
    Private Sub OrderReportForm_FormClosed(
        sender As Object,
        e As FormClosedEventArgs) _
        Handles MyBase.FormClosed

        RemoveHandler _printDocument.BeginPrint,
            AddressOf PrintDocument_BeginPrint

        RemoveHandler _printDocument.PrintPage,
            AddressOf PrintDocument_PrintPage

        _printDocument.Dispose()
    End Sub
End Class

