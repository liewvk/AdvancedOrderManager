Option Explicit On
Option Strict On
Option Infer On

Imports System.Collections.Generic
Imports System.Linq
Imports System.Threading
Imports AdvancedOrderManager.Application
Imports Microsoft.Extensions.DependencyInjection
Imports Microsoft.Extensions.Logging

Public Class OrderDatabaseForm

    Private _repository As IOrderDataRepository

    Private _logger As ILogger(
        Of OrderDatabaseForm)

    Public Sub New()

        InitializeComponent()

    End Sub
    Private _serviceProvider As IServiceProvider
    Public Sub New(
        repository As IOrderDataRepository,
        logger As ILogger(Of OrderDatabaseForm))

        InitializeComponent()

        If repository Is Nothing Then
            Throw New ArgumentNullException(
                NameOf(repository))
        End If

        If logger Is Nothing Then
            Throw New ArgumentNullException(
                NameOf(logger))
        End If

        _repository = repository
        _logger = logger
    End Sub

    Private Async Sub OrderDatabaseForm_Load(
        sender As Object,
        e As EventArgs) _
        Handles MyBase.Load

        If _repository Is Nothing Then
            lblStatus.Text =
                "Database services are unavailable."

            Return
        End If

        Try
            Await LoadOrdersAsync()

        Catch ex As Exception
            HandleDatabaseError(ex)
        End Try
    End Sub

    Private Async Sub btnSave_Click(
        sender As Object,
        e As EventArgs) _
        Handles btnSave.Click

        If Not EnsureRepositoryAvailable() Then
            Return
        End If

        If String.IsNullOrWhiteSpace(
            txtOrderId.Text) Then

            MessageBox.Show(
                Me,
                "Please enter an order ID.",
                "Missing Order ID",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning)

            txtOrderId.Focus()
            Return
        End If

        If String.IsNullOrWhiteSpace(
            txtCustomerName.Text) Then

            MessageBox.Show(
                Me,
                "Please enter a customer name.",
                "Missing Customer Name",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning)

            txtCustomerName.Focus()
            Return
        End If

        Dim quantity As Integer =
            Decimal.ToInt32(
                nudQuantity.Value)

        Dim unitPrice As Decimal =
            nudUnitPrice.Value

        Dim totalAmount As Decimal =
            Decimal.Round(
                quantity * unitPrice,
                2,
                MidpointRounding.AwayFromZero)

        Dim record As New StoredOrderRecord(
            txtOrderId.Text,
            txtCustomerName.Text,
            quantity,
            unitPrice,
            chkPriority.Checked,
            totalAmount,
            "Processed",
            DateTimeOffset.Now)

        SetBusyState(
            True,
            "Saving order...")

        Try
            Await _repository.AddAsync(
                record,
                CancellationToken.None)

            lblStatus.Text =
                $"Order {record.OrderId} was saved."

            ClearInputControls()

            Await LoadOrdersAsync()

        Catch ex As Exception
            HandleDatabaseError(ex)

        Finally
            SetBusyState(
                False,
                lblStatus.Text)
        End Try
    End Sub

    Private Async Sub btnLoad_Click(
        sender As Object,
        e As EventArgs) _
        Handles btnLoad.Click

        If Not EnsureRepositoryAvailable() Then
            Return
        End If

        Try
            Await LoadOrdersAsync()

        Catch ex As Exception
            HandleDatabaseError(ex)
        End Try
    End Sub

    Private Async Sub btnFind_Click(
        sender As Object,
        e As EventArgs) _
        Handles btnFind.Click

        If Not EnsureRepositoryAvailable() Then
            Return
        End If

        If String.IsNullOrWhiteSpace(
            txtOrderId.Text) Then

            MessageBox.Show(
                Me,
                "Enter the order ID that you want to find.",
                "Order ID Required",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning)

            txtOrderId.Focus()
            Return
        End If

        SetBusyState(
            True,
            "Searching...")

        Try
            Dim record As StoredOrderRecord =
                Await _repository.FindByOrderIdAsync(
                    txtOrderId.Text,
                    CancellationToken.None)

            If record Is Nothing Then

                dgvOrders.DataSource = Nothing

                lblStatus.Text =
                    "The order was not found."

                Return
            End If

            Dim records As New List(
                Of StoredOrderRecord) From {
                    record
                }

            DisplayRecords(
                records.AsReadOnly())

            lblStatus.Text =
                $"Order {record.OrderId} was found."

        Catch ex As Exception
            HandleDatabaseError(ex)

        Finally
            SetBusyState(
                False,
                lblStatus.Text)
        End Try
    End Sub

    Private Async Sub btnDeleteSelected_Click(
        sender As Object,
        e As EventArgs) _
        Handles btnDeleteSelected.Click

        If Not EnsureRepositoryAvailable() Then
            Return
        End If

        If dgvOrders.CurrentRow Is Nothing Then

            MessageBox.Show(
                Me,
                "Please select an order to delete.",
                "No Order Selected",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning)

            Return
        End If

        Dim selectedRecord As StoredOrderRecord =
            TryCast(
                dgvOrders.CurrentRow.DataBoundItem,
                StoredOrderRecord)

        If selectedRecord Is Nothing Then
            Return
        End If

        Dim confirmation As DialogResult =
            MessageBox.Show(
                Me,
                $"Delete order {selectedRecord.OrderId}?",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)

        If confirmation <> DialogResult.Yes Then
            Return
        End If

        SetBusyState(
            True,
            "Deleting order...")

        Try
            Dim wasDeleted As Boolean =
                Await _repository.DeleteByOrderIdAsync(
                    selectedRecord.OrderId,
                    CancellationToken.None)

            If wasDeleted Then
                lblStatus.Text =
                    $"Order {selectedRecord.OrderId} was deleted."
            Else
                lblStatus.Text =
                    "The order no longer exists."
            End If

            Await LoadOrdersAsync()

        Catch ex As Exception
            HandleDatabaseError(ex)

        Finally
            SetBusyState(
                False,
                lblStatus.Text)
        End Try
    End Sub

    Private Async Sub btnDeleteAll_Click(
        sender As Object,
        e As EventArgs) _
        Handles btnDeleteAll.Click

        If Not EnsureRepositoryAvailable() Then
            Return
        End If

        Dim confirmation As DialogResult =
            MessageBox.Show(
                Me,
                "Delete every stored order record?" &
                Environment.NewLine &
                Environment.NewLine &
                "This action cannot be undone.",
                "Confirm Delete All",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning)

        If confirmation <> DialogResult.Yes Then
            Return
        End If

        SetBusyState(
            True,
            "Deleting all records...")

        Try
            Dim deletedCount As Integer =
                Await _repository.DeleteAllAsync(
                    CancellationToken.None)

            lblStatus.Text =
                $"{deletedCount} records were deleted."

            Await LoadOrdersAsync()

        Catch ex As Exception
            HandleDatabaseError(ex)

        Finally
            SetBusyState(
                False,
                lblStatus.Text)
        End Try
    End Sub

    Private Async Function LoadOrdersAsync() As Task

        SetBusyState(
            True,
            "Loading database records...")

        Try
            Dim records =
                Await _repository.GetAllAsync(
                    CancellationToken.None)

            DisplayRecords(records)

            lblStatus.Text =
                $"{records.Count} records loaded."

        Finally
            SetBusyState(
                False,
                lblStatus.Text)
        End Try
    End Function

    Private Sub DisplayRecords(
        records As IReadOnlyList(Of StoredOrderRecord))

        dgvOrders.DataSource = Nothing

        dgvOrders.DataSource =
            records.ToList()

        If dgvOrders.Columns.Contains(
            NameOf(StoredOrderRecord.UnitPrice)) Then

            dgvOrders.Columns(
                NameOf(StoredOrderRecord.UnitPrice)) _
                .DefaultCellStyle.Format = "N2"
        End If

        If dgvOrders.Columns.Contains(
            NameOf(StoredOrderRecord.TotalAmount)) Then

            dgvOrders.Columns(
                NameOf(StoredOrderRecord.TotalAmount)) _
                .DefaultCellStyle.Format = "N2"
        End If

        If dgvOrders.Columns.Contains(
            NameOf(StoredOrderRecord.ProcessedAt)) Then

            dgvOrders.Columns(
                NameOf(StoredOrderRecord.ProcessedAt)) _
                .DefaultCellStyle.Format =
                "yyyy-MM-dd HH:mm:ss"
        End If
    End Sub

    Private Function EnsureRepositoryAvailable() As Boolean

        If _repository IsNot Nothing Then
            Return True
        End If

        MessageBox.Show(
            Me,
            "The database repository is unavailable. " &
            "Start the application through Program.Main.",
            "Database Service Unavailable",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

        Return False
    End Function

    Private Sub SetBusyState(
        isBusy As Boolean,
        statusMessage As String)

        btnSave.Enabled = Not isBusy
        btnLoad.Enabled = Not isBusy
        btnFind.Enabled = Not isBusy
        btnDeleteSelected.Enabled = Not isBusy
        btnDeleteAll.Enabled = Not isBusy

        txtOrderId.Enabled = Not isBusy
        txtCustomerName.Enabled = Not isBusy
        nudQuantity.Enabled = Not isBusy
        nudUnitPrice.Enabled = Not isBusy
        chkPriority.Enabled = Not isBusy

        lblStatus.Text = statusMessage

        UseWaitCursor = isBusy
    End Sub

    Private Sub ClearInputControls()

        txtOrderId.Clear()
        txtCustomerName.Clear()

        nudQuantity.Value = 1D
        nudUnitPrice.Value = 100D

        chkPriority.Checked = False

        txtOrderId.Focus()
    End Sub

    Private Sub HandleDatabaseError(
        exception As Exception)

        lblStatus.Text =
            "A database operation failed."

        If _logger IsNot Nothing Then

            _logger.LogError(
                exception,
                "The database form encountered an error.")
        End If

        MessageBox.Show(
            Me,
            exception.Message,
            "Database Error",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
    End Sub


End Class
