Option Explicit On
Option Strict On
Option Infer On

Imports System.ComponentModel
Imports System.Linq
Imports AdvancedOrderManager.Application
Imports AdvancedOrderManager.Domain
Imports AdvancedOrderManager.Application.Contracts



Public Class MainForm

    Private ReadOnly _draftLines As New BindingList(Of OrderLine)()


    Private ReadOnly _draftSource As New BindingSource()


    Private _createOrderService As CreateOrderService

    Private _repository As IOrderRepository

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(
        createOrderService As CreateOrderService,
        repository As IOrderRepository)

        Me.New()

        If createOrderService Is Nothing Then
            Throw New ArgumentNullException(
                NameOf(createOrderService))
        End If

        If repository Is Nothing Then
            Throw New ArgumentNullException(
                NameOf(repository))
        End If

        _createOrderService = createOrderService
        _repository = repository
    End Sub

    Private Sub MainForm_Load(
        sender As Object,
        e As EventArgs) Handles MyBase.Load

        If _createOrderService Is Nothing OrElse
           _repository Is Nothing Then

            Throw New InvalidOperationException(
                "The application services were not configured.")
        End If

        ConfigureOrderGrid()

        _draftSource.DataSource = _draftLines
        dgvLines.DataSource = _draftSource

        UpdateDraftTotal()
        RefreshOrderList()

        txtCustomerName.Focus()
    End Sub

    Private Sub ConfigureOrderGrid()

        dgvLines.AutoGenerateColumns = False
        dgvLines.Columns.Clear()

        Dim productColumn As New DataGridViewTextBoxColumn() With {
            .Name = "ProductColumn",
            .HeaderText = "Product",
            .DataPropertyName = NameOf(OrderLine.ProductName),
            .FillWeight = 40
        }

        Dim quantityColumn As New DataGridViewTextBoxColumn() With {
            .Name = "QuantityColumn",
            .HeaderText = "Quantity",
            .DataPropertyName = NameOf(OrderLine.Quantity),
            .FillWeight = 15
        }

        Dim priceColumn As New DataGridViewTextBoxColumn() With {
            .Name = "PriceColumn",
            .HeaderText = "Unit Price",
            .DataPropertyName = NameOf(OrderLine.UnitPrice),
            .DefaultCellStyle =
                New DataGridViewCellStyle() With {
                    .Format = "C2"
                },
            .FillWeight = 20
        }

        Dim totalColumn As New DataGridViewTextBoxColumn() With {
            .Name = "TotalColumn",
            .HeaderText = "Line Total",
            .DataPropertyName = NameOf(OrderLine.LineTotal),
            .DefaultCellStyle =
                New DataGridViewCellStyle() With {
                    .Format = "C2"
                },
            .FillWeight = 25
        }

        dgvLines.Columns.Add(productColumn)
        dgvLines.Columns.Add(quantityColumn)
        dgvLines.Columns.Add(priceColumn)
        dgvLines.Columns.Add(totalColumn)
    End Sub

    Private Sub btnAddLine_Click(
        sender As Object,
        e As EventArgs) Handles btnAddLine.Click

        Try
            Dim quantity As Integer =
                Decimal.ToInt32(nudQuantity.Value)

            Dim line As New OrderLine(
                txtProductName.Text,
                quantity,
                nudUnitPrice.Value)

            _draftLines.Add(line)

            txtProductName.Clear()
            nudQuantity.Value = nudQuantity.Minimum
            nudUnitPrice.Value = nudUnitPrice.Minimum

            UpdateDraftTotal()

            lblStatus.Text = "Order line added."
            txtProductName.Focus()

        Catch ex As ArgumentException
            MessageBox.Show(
                ex.Message,
                "Invalid Order Line",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning)

        Catch ex As Exception
            ShowUnexpectedError(ex)
        End Try
    End Sub

    Private Sub btnRemoveLine_Click(
        sender As Object,
        e As EventArgs) Handles btnRemoveLine.Click

        If dgvLines.CurrentRow Is Nothing Then
            MessageBox.Show(
                "Select an order line to remove.",
                "No Selection",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information)

            Return
        End If

        Dim selectedLine As OrderLine =
            TryCast(dgvLines.CurrentRow.DataBoundItem,
                    OrderLine)

        If selectedLine Is Nothing Then
            Return
        End If

        _draftLines.Remove(selectedLine)
        UpdateDraftTotal()

        lblStatus.Text = "Order line removed."
    End Sub

    Private Sub btnCreateOrder_Click(
        sender As Object,
        e As EventArgs) Handles btnCreateOrder.Click

        Try
            Dim requestedLines =
                _draftLines _
                    .Select(
                        Function(line)
                            Return New OrderLineRequest(
                                line.ProductName,
                                line.Quantity,
                                line.UnitPrice)
                        End Function) _
                    .ToList()

            Dim request As New CreateOrderRequest(
                txtCustomerName.Text,
                requestedLines)

            Dim order As Order =
                _createOrderService.Execute(request)

            MessageBox.Show(
                $"Order created successfully." &
                Environment.NewLine &
                $"Order ID: {order.OrderId}" &
                Environment.NewLine &
                $"Total: {order.Total:C2}",
                "Order Created",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information)

            ClearDraft()
            RefreshOrderList()

            lblStatus.Text =
                $"Order for {order.CustomerName} was created."

        Catch ex As ArgumentException
            MessageBox.Show(
                ex.Message,
                "Invalid Order",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning)

        Catch ex As InvalidOperationException
            MessageBox.Show(
                ex.Message,
                "Order Cannot Be Created",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning)

        Catch ex As Exception
            ShowUnexpectedError(ex)
        End Try
    End Sub

    Private Sub btnClearDraft_Click(
        sender As Object,
        e As EventArgs) Handles btnClearDraft.Click

        If _draftLines.Count = 0 AndAlso
           String.IsNullOrWhiteSpace(
               txtCustomerName.Text) Then

            Return
        End If

        Dim answer As DialogResult =
            MessageBox.Show(
                "Clear the current draft order?",
                "Confirm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)

        If answer = DialogResult.Yes Then
            ClearDraft()
            lblStatus.Text = "Draft cleared."
        End If
    End Sub

    Private Sub UpdateDraftTotal()

        Dim total As Decimal =
            _draftLines.Sum(
                Function(line) line.LineTotal)

        lblDraftTotal.Text =
            $"Draft Total: {total:C2}"
    End Sub

    Private Sub ClearDraft()

        txtCustomerName.Clear()
        txtProductName.Clear()

        nudQuantity.Value = 1D
        nudUnitPrice.Value = 0D

        _draftLines.Clear()

        UpdateDraftTotal()

        txtCustomerName.Focus()
    End Sub

    Private Sub RefreshOrderList()

        lstOrders.Items.Clear()

        For Each order In _repository.GetAll()

            lstOrders.Items.Add(
                $"{order.CreatedAt:yyyy-MM-dd HH:mm} | " &
                $"{order.CustomerName} | " &
                $"{order.Lines.Count} item(s) | " &
                $"{order.Total:C2}")
        Next
    End Sub

    Private Sub ShowUnexpectedError(ex As Exception)

        lblStatus.Text = "An unexpected error occurred."

        MessageBox.Show(
            "The operation could not be completed." &
            Environment.NewLine &
            Environment.NewLine &
            ex.Message,
            "Application Error",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
    End Sub

End Class

