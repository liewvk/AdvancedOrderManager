Option Explicit On
Option Strict On
Option Infer On

Imports System.Collections.Generic
Imports System.Linq
Imports System.Threading
Imports AdvancedOrderManager.Application
Imports Microsoft.Extensions.Logging

Public Class EntityFrameworkQueryForm

    Private _queryService As IOrderHistoryQueryService

    Private _logger As ILogger(
        Of EntityFrameworkQueryForm)

    Public Sub New()

        InitializeComponent()
    End Sub

    Public Sub New(
        queryService As IOrderHistoryQueryService,
        logger As ILogger(Of EntityFrameworkQueryForm))

        InitializeComponent()

        If queryService Is Nothing Then
            Throw New ArgumentNullException(
                NameOf(queryService))
        End If

        If logger Is Nothing Then
            Throw New ArgumentNullException(
                NameOf(logger))
        End If

        _queryService = queryService
        _logger = logger
    End Sub

    Private Async Sub EntityFrameworkQueryForm_Load(
        sender As Object,
        e As EventArgs) _
        Handles MyBase.Load

        ConfigureStatusList()

        If _queryService Is Nothing Then

            lblStatus.Text =
                "EF Core services are unavailable."

            Return
        End If

        Try
            Await RefreshViewAsync()

        Catch ex As Exception
            HandleQueryError(ex)
        End Try
    End Sub

    Private Async Sub btnSearch_Click(
        sender As Object,
        e As EventArgs) _
        Handles btnSearch.Click

        If Not EnsureServiceAvailable() Then
            Return
        End If

        Try
            Await RefreshViewAsync()

        Catch ex As Exception
            HandleQueryError(ex)
        End Try
    End Sub

    Private Async Sub btnReset_Click(
        sender As Object,
        e As EventArgs) _
        Handles btnReset.Click

        If Not EnsureServiceAvailable() Then
            Return
        End If

        txtCustomerFilter.Clear()
        cmbStatus.SelectedIndex = 0
        chkPriorityOnly.Checked = False

        Try
            Await RefreshViewAsync()

        Catch ex As Exception
            HandleQueryError(ex)
        End Try
    End Sub

    Private Async Function RefreshViewAsync() As Task

        SetBusyState(
            True,
            "Executing EF Core queries...")

        Try
            Dim selectedStatus As String =
                GetSelectedStatus()

            Dim criteria As New OrderHistorySearchCriteria(
                txtCustomerFilter.Text,
                selectedStatus,
                chkPriorityOnly.Checked)

            Dim records =
                Await _queryService.SearchAsync(
                    criteria,
                    CancellationToken.None)

            Dim statistics =
                Await _queryService.GetStatisticsAsync(
                    CancellationToken.None)

            DisplayRecords(records)
            DisplayStatistics(statistics)

            lblStatus.Text =
                $"{records.Count} matching records loaded."

        Finally
            SetBusyState(
                False,
                lblStatus.Text)
        End Try
    End Function

    Private Sub ConfigureStatusList()

        If cmbStatus.Items.Count > 0 Then
            Return
        End If

        cmbStatus.Items.Add(
            "All")

        cmbStatus.Items.Add(
            "Processed")

        cmbStatus.Items.Add(
            "Rejected")

        cmbStatus.Items.Add(
            "Pending")

        cmbStatus.SelectedIndex = 0
    End Sub

    Private Function GetSelectedStatus() As String

        If cmbStatus.SelectedIndex <= 0 Then
            Return String.Empty
        End If

        Return CStr(
            cmbStatus.SelectedItem)
    End Function

    Private Sub DisplayRecords(
        records As IReadOnlyList(Of StoredOrderRecord))

        dgvResults.DataSource = Nothing

        dgvResults.DataSource =
            records.ToList()

        If dgvResults.Columns.Contains(
            NameOf(StoredOrderRecord.UnitPrice)) Then

            dgvResults.Columns(
                NameOf(StoredOrderRecord.UnitPrice)) _
                .DefaultCellStyle.Format = "N2"
        End If

        If dgvResults.Columns.Contains(
            NameOf(StoredOrderRecord.TotalAmount)) Then

            dgvResults.Columns(
                NameOf(StoredOrderRecord.TotalAmount)) _
                .DefaultCellStyle.Format = "N2"
        End If

        If dgvResults.Columns.Contains(
            NameOf(StoredOrderRecord.ProcessedAt)) Then

            dgvResults.Columns(
                NameOf(StoredOrderRecord.ProcessedAt)) _
                .DefaultCellStyle.Format =
                "yyyy-MM-dd HH:mm:ss"
        End If
    End Sub

    Private Sub DisplayStatistics(
        statistics As OrderHistoryStatistics)

        lblTotalOrdersValue.Text =
            statistics.TotalOrders.ToString()

        lblPriorityOrdersValue.Text =
            statistics.PriorityOrders.ToString()

        lblTotalAmountValue.Text =
            statistics.TotalAmount.ToString("N2")

        lblAverageAmountValue.Text =
            statistics.AverageAmount.ToString("N2")
    End Sub

    Private Function EnsureServiceAvailable() As Boolean

        If _queryService IsNot Nothing Then
            Return True
        End If

        MessageBox.Show(
            Me,
            "EF Core query services are unavailable. " &
            "Start the application through Program.Main.",
            "Service Unavailable",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

        Return False
    End Function

    Private Sub SetBusyState(
        isBusy As Boolean,
        statusMessage As String)

        txtCustomerFilter.Enabled =
            Not isBusy

        cmbStatus.Enabled =
            Not isBusy

        chkPriorityOnly.Enabled =
            Not isBusy

        btnSearch.Enabled =
            Not isBusy

        btnReset.Enabled =
            Not isBusy

        lblStatus.Text =
            statusMessage

        UseWaitCursor =
            isBusy
    End Sub

    Private Sub HandleQueryError(
        exception As Exception)

        lblStatus.Text =
            "The EF Core query failed."

        If _logger IsNot Nothing Then

            _logger.LogError(
                exception,
                "The EF Core query form encountered an error.")
        End If

        MessageBox.Show(
            Me,
            exception.Message,
            "EF Core Query Error",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
    End Sub

End Class
