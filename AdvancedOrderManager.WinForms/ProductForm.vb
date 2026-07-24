Option Explicit On
Option Strict On
Option Infer On

Imports System.ComponentModel
Imports AdvancedOrderManager.Application
Imports AdvancedOrderManager.Domain

Public Class ProductForm

    Private ReadOnly _repository As IProductRepository

    Private ReadOnly _registerProductService As RegisterProductService

    Private ReadOnly _searchProductsService As SearchProductsService

    Private ReadOnly _adjustmentService As InventoryAdjustmentService

    Private ReadOnly _restockQueueService As RestockQueueService

    Private ReadOnly _statisticsService As InventoryStatisticsService

    Private ReadOnly _rows As New BindingList(Of ProductGridRow)()

    Private ReadOnly _source As New BindingSource()

    Public Sub New(
        repository As IProductRepository,
        registerProductService As RegisterProductService,
        searchProductsService As SearchProductsService,
        adjustmentService As InventoryAdjustmentService,
        restockQueueService As RestockQueueService,
        statisticsService As InventoryStatisticsService)

        InitializeComponent()

        _repository = repository
        _registerProductService = registerProductService
        _searchProductsService = searchProductsService
        _adjustmentService = adjustmentService
        _restockQueueService = restockQueueService
        _statisticsService = statisticsService
    End Sub

    Private Sub ProductForm_Load(
        sender As Object,
        e As EventArgs) Handles MyBase.Load

        ConfigureProductGrid()

        _source.DataSource = _rows
        dgvProducts.DataSource = _source

        cboCategory.Items.AddRange(
            New Object() {
                "Hardware",
                "Software",
                "Accessories",
                "Networking",
                "Office Equipment"
            })

        RefreshCategoryFilter()
        RefreshProducts()
        RefreshStatistics()
        RefreshButtonStates()

        txtProductCode.Focus()
    End Sub

    Private Sub ConfigureProductGrid()

        dgvProducts.Columns.Clear()

        AddTextColumn(
            "CodeColumn",
            "Code",
            NameOf(ProductGridRow.Code),
            12)

        AddTextColumn(
            "NameColumn",
            "Product",
            NameOf(ProductGridRow.ProductName),
            22)

        AddTextColumn(
            "CategoryColumn",
            "Category",
            NameOf(ProductGridRow.Category),
            15)

        AddCurrencyColumn(
            "PriceColumn",
            "Unit Price",
            NameOf(ProductGridRow.UnitPrice),
            12)

        AddTextColumn(
            "QuantityColumn",
            "Quantity",
            NameOf(ProductGridRow.QuantityInStock),
            10)

        AddTextColumn(
            "ReorderColumn",
            "Reorder",
            NameOf(ProductGridRow.ReorderLevel),
            10)

        AddTextColumn(
            "StatusColumn",
            "Status",
            NameOf(ProductGridRow.StockStatus),
            12)

        AddCurrencyColumn(
            "ValueColumn",
            "Stock Value",
            NameOf(ProductGridRow.StockValue),
            15)
    End Sub

    Private Sub AddTextColumn(
        columnName As String,
        headerText As String,
        propertyName As String,
        fillWeight As Single)

        Dim column As New DataGridViewTextBoxColumn() With {
            .Name = columnName,
            .HeaderText = headerText,
            .DataPropertyName = propertyName,
            .FillWeight = fillWeight,
            .ReadOnly = True
        }

        dgvProducts.Columns.Add(column)
    End Sub

    Private Sub AddCurrencyColumn(
        columnName As String,
        headerText As String,
        propertyName As String,
        fillWeight As Single)

        Dim column As New DataGridViewTextBoxColumn() With {
            .Name = columnName,
            .HeaderText = headerText,
            .DataPropertyName = propertyName,
            .FillWeight = fillWeight,
            .ReadOnly = True,
            .DefaultCellStyle =
                New DataGridViewCellStyle() With {
                    .Format = "C2"
                }
        }

        dgvProducts.Columns.Add(column)
    End Sub

    Private Sub btnRegisterProduct_Click(
        sender As Object,
        e As EventArgs) Handles btnRegisterProduct.Click

        Dim category As String =
            cboCategory.Text.Trim()

        Dim request As New RegisterProductRequest(
            txtProductCode.Text,
            txtProductName.Text,
            category,
            nudUnitPrice.Value,
            Decimal.ToInt32(nudOpeningStock.Value),
            Decimal.ToInt32(nudReorderLevel.Value))

        Dim result =
            _registerProductService.Execute(request)

        If Not result.IsSuccess Then

            MessageBox.Show(
                result.ErrorMessage,
                "Product Cannot Be Registered",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning)

            lblInventoryStatus.Text =
                "Product registration failed."

            Return
        End If

        MessageBox.Show(
            $"Product registered successfully." &
            Environment.NewLine &
            $"Code: {result.Value.Code}" &
            Environment.NewLine &
            $"Product: {result.Value.Name}",
            "Product Registered",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information)

        ClearProductInputs()
        RefreshCategoryFilter()
        RefreshProducts()
        RefreshStatistics()

        lblInventoryStatus.Text =
            $"Registered {result.Value.Name}."
    End Sub

    Private Sub btnClearProduct_Click(
        sender As Object,
        e As EventArgs) Handles btnClearProduct.Click

        ClearProductInputs()

        lblInventoryStatus.Text =
            "Product input cleared."
    End Sub

    Private Sub txtProductSearch_TextChanged(
        sender As Object,
        e As EventArgs) _
        Handles txtProductSearch.TextChanged

        RefreshProducts()
    End Sub

    Private Sub cboCategoryFilter_SelectedIndexChanged(
        sender As Object,
        e As EventArgs) _
        Handles cboCategoryFilter.SelectedIndexChanged

        RefreshProducts()
    End Sub

    Private Sub chkIncludeInactive_CheckedChanged(
        sender As Object,
        e As EventArgs) _
        Handles chkIncludeInactive.CheckedChanged

        RefreshProducts()
    End Sub

    Private Sub dgvProducts_SelectionChanged(
        sender As Object,
        e As EventArgs) _
        Handles dgvProducts.SelectionChanged

        RefreshButtonStates()
    End Sub

    Private Sub btnApplyAdjustment_Click(
        sender As Object,
        e As EventArgs) _
        Handles btnApplyAdjustment.Click

        Dim selectedRow =
            GetSelectedProductRow()

        If selectedRow Is Nothing Then

            MessageBox.Show(
                "Select a product first.",
                "No Product Selected",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information)

            Return
        End If

        Dim quantityChange As Integer =
            Decimal.ToInt32(
                nudAdjustment.Value)

        Dim result =
            _adjustmentService.Adjust(
                selectedRow.ProductId,
                quantityChange,
                txtAdjustmentReason.Text)

        If Not result.IsSuccess Then

            MessageBox.Show(
                result.ErrorMessage,
                "Adjustment Failed",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning)

            Return
        End If

        nudAdjustment.Value = 0D
        txtAdjustmentReason.Clear()

        RefreshProducts()
        RefreshStatistics()
        RefreshButtonStates()

        lblInventoryStatus.Text =
            $"Stock adjusted for {result.Value.Name}."
    End Sub

    Private Sub btnUndoAdjustment_Click(
        sender As Object,
        e As EventArgs) _
        Handles btnUndoAdjustment.Click

        Dim result =
            _adjustmentService.UndoLast()

        If Not result.IsSuccess Then

            MessageBox.Show(
                result.ErrorMessage,
                "Undo Failed",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information)

            Return
        End If

        RefreshProducts()
        RefreshStatistics()
        RefreshButtonStates()

        lblInventoryStatus.Text =
            $"Last adjustment for {result.Value.Name} was undone."
    End Sub

    Private Sub btnQueueRestock_Click(
        sender As Object,
        e As EventArgs) _
        Handles btnQueueRestock.Click

        Dim selectedRow =
            GetSelectedProductRow()

        If selectedRow Is Nothing Then

            MessageBox.Show(
                "Select a product first.",
                "No Product Selected",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information)

            Return
        End If

        Dim result =
            _restockQueueService.Enqueue(
                selectedRow.ProductId)

        If Not result.IsSuccess Then

            MessageBox.Show(
                result.ErrorMessage,
                "Restock Queue",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information)

            Return
        End If

        RefreshQueueStatus()

        lblInventoryStatus.Text =
            $"{result.Value.Name} was added to the restock queue."
    End Sub

    Private Sub btnProcessRestock_Click(
        sender As Object,
        e As EventArgs) _
        Handles btnProcessRestock.Click

        Dim result =
            _restockQueueService.TryProcessNext()

        If Not result.IsSuccess Then

            MessageBox.Show(
                result.ErrorMessage,
                "Restock Queue",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information)

            Return
        End If

        MessageBox.Show(
            $"Process restocking for:" &
            Environment.NewLine &
            $"{result.Value.Code} - {result.Value.Name}" &
            Environment.NewLine &
            $"Current quantity: {result.Value.QuantityInStock}" &
            Environment.NewLine &
            $"Reorder level: {result.Value.ReorderLevel}",
            "Next Restock Item",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information)

        RefreshQueueStatus()

        lblInventoryStatus.Text =
            $"Processing restock for {result.Value.Name}."
    End Sub

    Private Function GetSelectedProductRow() _
        As ProductGridRow

        If dgvProducts.CurrentRow Is Nothing Then
            Return Nothing
        End If

        Return TryCast(
            dgvProducts.CurrentRow.DataBoundItem,
            ProductGridRow)
    End Function

    Private Sub RefreshProducts()

        Dim categoryFilter As String =
            String.Empty

        If cboCategoryFilter.SelectedIndex > 0 Then
            categoryFilter =
                cboCategoryFilter.Text
        End If

        Dim products =
            _searchProductsService.Execute(
                txtProductSearch.Text,
                categoryFilter,
                chkIncludeInactive.Checked)

        _rows.Clear()

        For Each product In products
            _rows.Add(
                New ProductGridRow(product))
        Next

        RefreshButtonStates()
    End Sub

    Private Sub RefreshCategoryFilter()

        Dim selectedCategory As String =
            cboCategoryFilter.Text

        cboCategoryFilter.Items.Clear()
        cboCategoryFilter.Items.Add(
            "All Categories")

        For Each category In
            _repository.GetCategories()

            cboCategoryFilter.Items.Add(category)
        Next

        Dim index As Integer =
            cboCategoryFilter.FindStringExact(
                selectedCategory)

        If index >= 0 Then
            cboCategoryFilter.SelectedIndex = index
        Else
            cboCategoryFilter.SelectedIndex = 0
        End If
    End Sub

    Private Sub RefreshStatistics()

        Dim statistics =
            _statisticsService.Execute()

        lblProductCount.Text =
            $"Products: {statistics.ProductCount}"

        lblLowStockCount.Text =
            $"Low Stock: {statistics.LowStockCount}"

        lblStockUnits.Text =
            $"Units: {statistics.TotalStockUnits}"

        lblInventoryValue.Text =
            $"Inventory Value: {statistics.TotalStockValue:C2}"
    End Sub

    Private Sub RefreshQueueStatus()

        lblRestockQueue.Text =
            $"Restock Queue: {_restockQueueService.PendingCount}"
    End Sub

    Private Sub RefreshButtonStates()

        Dim hasSelection As Boolean =
            GetSelectedProductRow() IsNot Nothing

        btnApplyAdjustment.Enabled =
            hasSelection

        btnQueueRestock.Enabled =
            hasSelection

        btnUndoAdjustment.Enabled =
            _adjustmentService.CanUndo

        btnProcessRestock.Enabled =
            _restockQueueService.PendingCount > 0

        RefreshQueueStatus()
    End Sub

    Private Sub ClearProductInputs()

        txtProductCode.Clear()
        txtProductName.Clear()

        cboCategory.SelectedIndex = -1
        cboCategory.Text = String.Empty

        nudUnitPrice.Value = 0D
        nudOpeningStock.Value = 0D
        nudReorderLevel.Value = 0D

        txtProductCode.Focus()
    End Sub

    Private Sub btnInventory_Click(sender As Object, e As EventArgs) Handles btnInventory.Click
        Using productForm As ProductForm =
        Bootstrapper.CreateProductForm()

            productForm.ShowDialog(Me)
        End Using

    End Sub

    Private Sub dgvProducts_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvProducts.CellContentClick

    End Sub
End Class
