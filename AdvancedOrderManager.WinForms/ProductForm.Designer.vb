<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ProductForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        grpProductRegistration = New GroupBox()
        lblRestockQueue = New Label()
        btnProcessRestock = New Button()
        btnUndoAdjustment = New Button()
        btnQueueRestock = New Button()
        btnApplyAdjustment = New Button()
        btnClearProduct = New Button()
        btnRegisterProduct = New Button()
        nudReorderLevel = New NumericUpDown()
        nudAdjustment = New NumericUpDown()
        NumericUpDown2 = New NumericUpDown()
        NumericUpDown1 = New NumericUpDown()
        nudOpeningStock = New NumericUpDown()
        nudUnitPrice = New NumericUpDown()
        cboCategory = New ComboBox()
        txtProductName = New TextBox()
        txtAdjustmentReason = New TextBox()
        lblAdjustmentReason = New Label()
        txtProductCode = New TextBox()
        lblAdjustment = New Label()
        lblReorderLevel = New Label()
        lblOpeningStock = New Label()
        lblUnitPrice = New Label()
        lblCategory = New Label()
        lblProductName = New Label()
        lblProductCode = New Label()
        grpSearchCOntrol = New GroupBox()
        chkIncludeInactive = New CheckBox()
        cboCategoryFilter = New ComboBox()
        lblSearchProducts = New Label()
        txtProductSearch = New TextBox()
        dgvProducts = New DataGridView()
        grpStatistics = New GroupBox()
        lblInventoryStatus = New Label()
        lblInventoryValue = New Label()
        lblStockUnits = New Label()
        lblLowStockCount = New Label()
        lblProductCount = New Label()
        btnInventory = New Button()
        grpProductRegistration.SuspendLayout()
        CType(nudReorderLevel, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudAdjustment, ComponentModel.ISupportInitialize).BeginInit()
        CType(NumericUpDown2, ComponentModel.ISupportInitialize).BeginInit()
        CType(NumericUpDown1, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudOpeningStock, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudUnitPrice, ComponentModel.ISupportInitialize).BeginInit()
        grpSearchCOntrol.SuspendLayout()
        CType(dgvProducts, ComponentModel.ISupportInitialize).BeginInit()
        grpStatistics.SuspendLayout()
        SuspendLayout()
        ' 
        ' grpProductRegistration
        ' 
        grpProductRegistration.Controls.Add(lblRestockQueue)
        grpProductRegistration.Controls.Add(btnProcessRestock)
        grpProductRegistration.Controls.Add(btnUndoAdjustment)
        grpProductRegistration.Controls.Add(btnQueueRestock)
        grpProductRegistration.Controls.Add(btnApplyAdjustment)
        grpProductRegistration.Controls.Add(btnClearProduct)
        grpProductRegistration.Controls.Add(btnRegisterProduct)
        grpProductRegistration.Controls.Add(nudReorderLevel)
        grpProductRegistration.Controls.Add(nudAdjustment)
        grpProductRegistration.Controls.Add(NumericUpDown2)
        grpProductRegistration.Controls.Add(NumericUpDown1)
        grpProductRegistration.Controls.Add(nudOpeningStock)
        grpProductRegistration.Controls.Add(nudUnitPrice)
        grpProductRegistration.Controls.Add(cboCategory)
        grpProductRegistration.Controls.Add(txtProductName)
        grpProductRegistration.Controls.Add(txtAdjustmentReason)
        grpProductRegistration.Controls.Add(lblAdjustmentReason)
        grpProductRegistration.Controls.Add(txtProductCode)
        grpProductRegistration.Controls.Add(lblAdjustment)
        grpProductRegistration.Controls.Add(lblReorderLevel)
        grpProductRegistration.Controls.Add(lblOpeningStock)
        grpProductRegistration.Controls.Add(lblUnitPrice)
        grpProductRegistration.Controls.Add(lblCategory)
        grpProductRegistration.Controls.Add(lblProductName)
        grpProductRegistration.Controls.Add(lblProductCode)
        grpProductRegistration.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0)
        grpProductRegistration.Location = New Point(12, 12)
        grpProductRegistration.Name = "grpProductRegistration"
        grpProductRegistration.Size = New Size(590, 425)
        grpProductRegistration.TabIndex = 0
        grpProductRegistration.TabStop = False
        grpProductRegistration.Text = "Product Registration"
        ' 
        ' lblRestockQueue
        ' 
        lblRestockQueue.AutoSize = True
        lblRestockQueue.Location = New Point(38, 384)
        lblRestockQueue.Name = "lblRestockQueue"
        lblRestockQueue.Size = New Size(149, 25)
        lblRestockQueue.TabIndex = 0
        lblRestockQueue.Text = "Restock Queue: 0"
        ' 
        ' btnProcessRestock
        ' 
        btnProcessRestock.Location = New Point(241, 346)
        btnProcessRestock.Name = "btnProcessRestock"
        btnProcessRestock.Size = New Size(207, 33)
        btnProcessRestock.TabIndex = 1
        btnProcessRestock.Text = "Process Next Restock"
        btnProcessRestock.UseVisualStyleBackColor = True
        ' 
        ' btnUndoAdjustment
        ' 
        btnUndoAdjustment.Location = New Point(241, 304)
        btnUndoAdjustment.Name = "btnUndoAdjustment"
        btnUndoAdjustment.Size = New Size(207, 32)
        btnUndoAdjustment.TabIndex = 7
        btnUndoAdjustment.Text = "Undo Last Adjustment"
        btnUndoAdjustment.UseVisualStyleBackColor = True
        ' 
        ' btnQueueRestock
        ' 
        btnQueueRestock.Location = New Point(24, 346)
        btnQueueRestock.Name = "btnQueueRestock"
        btnQueueRestock.Size = New Size(192, 33)
        btnQueueRestock.TabIndex = 0
        btnQueueRestock.Text = "Add to Restock Queue"
        btnQueueRestock.UseVisualStyleBackColor = True
        ' 
        ' btnApplyAdjustment
        ' 
        btnApplyAdjustment.Location = New Point(26, 304)
        btnApplyAdjustment.Name = "btnApplyAdjustment"
        btnApplyAdjustment.Size = New Size(190, 36)
        btnApplyAdjustment.TabIndex = 6
        btnApplyAdjustment.Text = "Apply Adjustment"
        btnApplyAdjustment.UseVisualStyleBackColor = True
        ' 
        ' btnClearProduct
        ' 
        btnClearProduct.Location = New Point(485, 131)
        btnClearProduct.Name = "btnClearProduct"
        btnClearProduct.Size = New Size(86, 33)
        btnClearProduct.TabIndex = 5
        btnClearProduct.Text = "Clear"
        btnClearProduct.UseVisualStyleBackColor = True
        ' 
        ' btnRegisterProduct
        ' 
        btnRegisterProduct.Location = New Point(326, 129)
        btnRegisterProduct.Name = "btnRegisterProduct"
        btnRegisterProduct.Size = New Size(153, 35)
        btnRegisterProduct.TabIndex = 4
        btnRegisterProduct.Text = "Register Product"
        btnRegisterProduct.UseVisualStyleBackColor = True
        ' 
        ' nudReorderLevel
        ' 
        nudReorderLevel.Location = New Point(172, 181)
        nudReorderLevel.Maximum = New Decimal(New Integer() {1000000, 0, 0, 0})
        nudReorderLevel.Name = "nudReorderLevel"
        nudReorderLevel.Size = New Size(144, 31)
        nudReorderLevel.TabIndex = 3
        ' 
        ' nudAdjustment
        ' 
        nudAdjustment.Location = New Point(26, 263)
        nudAdjustment.Maximum = New Decimal(New Integer() {1000000, 0, 0, 0})
        nudAdjustment.Minimum = New Decimal(New Integer() {1000000, 0, 0, 0})
        nudAdjustment.Name = "nudAdjustment"
        nudAdjustment.Size = New Size(144, 31)
        nudAdjustment.TabIndex = 3
        nudAdjustment.Value = New Decimal(New Integer() {1000000, 0, 0, 0})
        ' 
        ' NumericUpDown2
        ' 
        NumericUpDown2.Location = New Point(165, 131)
        NumericUpDown2.Maximum = New Decimal(New Integer() {1000000, 0, 0, 0})
        NumericUpDown2.Name = "NumericUpDown2"
        NumericUpDown2.Size = New Size(144, 31)
        NumericUpDown2.TabIndex = 3
        ' 
        ' NumericUpDown1
        ' 
        NumericUpDown1.Location = New Point(164, 133)
        NumericUpDown1.Maximum = New Decimal(New Integer() {1000000, 0, 0, 0})
        NumericUpDown1.Name = "NumericUpDown1"
        NumericUpDown1.Size = New Size(144, 31)
        NumericUpDown1.TabIndex = 3
        ' 
        ' nudOpeningStock
        ' 
        nudOpeningStock.Location = New Point(164, 133)
        nudOpeningStock.Maximum = New Decimal(New Integer() {1000000, 0, 0, 0})
        nudOpeningStock.Name = "nudOpeningStock"
        nudOpeningStock.Size = New Size(144, 31)
        nudOpeningStock.TabIndex = 3
        ' 
        ' nudUnitPrice
        ' 
        nudUnitPrice.DecimalPlaces = 2
        nudUnitPrice.Location = New Point(402, 84)
        nudUnitPrice.Maximum = New Decimal(New Integer() {1000000, 0, 0, 0})
        nudUnitPrice.Name = "nudUnitPrice"
        nudUnitPrice.Size = New Size(144, 31)
        nudUnitPrice.TabIndex = 3
        ' 
        ' cboCategory
        ' 
        cboCategory.FormattingEnabled = True
        cboCategory.Location = New Point(124, 81)
        cboCategory.Name = "cboCategory"
        cboCategory.Size = New Size(125, 33)
        cboCategory.TabIndex = 2
        ' 
        ' txtProductName
        ' 
        txtProductName.BorderStyle = BorderStyle.FixedSingle
        txtProductName.Location = New Point(392, 30)
        txtProductName.Name = "txtProductName"
        txtProductName.Size = New Size(179, 31)
        txtProductName.TabIndex = 1
        ' 
        ' txtAdjustmentReason
        ' 
        txtAdjustmentReason.BorderStyle = BorderStyle.FixedSingle
        txtAdjustmentReason.Location = New Point(275, 263)
        txtAdjustmentReason.Name = "txtAdjustmentReason"
        txtAdjustmentReason.Size = New Size(152, 31)
        txtAdjustmentReason.TabIndex = 1
        ' 
        ' lblAdjustmentReason
        ' 
        lblAdjustmentReason.AutoSize = True
        lblAdjustmentReason.Location = New Point(200, 268)
        lblAdjustmentReason.Name = "lblAdjustmentReason"
        lblAdjustmentReason.Size = New Size(69, 25)
        lblAdjustmentReason.TabIndex = 0
        lblAdjustmentReason.Text = "Reason"
        ' 
        ' txtProductCode
        ' 
        txtProductCode.BorderStyle = BorderStyle.FixedSingle
        txtProductCode.Location = New Point(155, 34)
        txtProductCode.Name = "txtProductCode"
        txtProductCode.Size = New Size(94, 31)
        txtProductCode.TabIndex = 1
        ' 
        ' lblAdjustment
        ' 
        lblAdjustment.AutoSize = True
        lblAdjustment.Location = New Point(24, 231)
        lblAdjustment.Name = "lblAdjustment"
        lblAdjustment.Size = New Size(145, 25)
        lblAdjustment.TabIndex = 0
        lblAdjustment.Text = "Quantity Change"
        ' 
        ' lblReorderLevel
        ' 
        lblReorderLevel.AutoSize = True
        lblReorderLevel.Location = New Point(24, 187)
        lblReorderLevel.Name = "lblReorderLevel"
        lblReorderLevel.Size = New Size(118, 25)
        lblReorderLevel.TabIndex = 0
        lblReorderLevel.Text = "Reorder Level"
        ' 
        ' lblOpeningStock
        ' 
        lblOpeningStock.AutoSize = True
        lblOpeningStock.Location = New Point(24, 133)
        lblOpeningStock.Name = "lblOpeningStock"
        lblOpeningStock.Size = New Size(129, 25)
        lblOpeningStock.TabIndex = 0
        lblOpeningStock.Text = "Opening Stock"
        ' 
        ' lblUnitPrice
        ' 
        lblUnitPrice.AutoSize = True
        lblUnitPrice.Location = New Point(300, 84)
        lblUnitPrice.Name = "lblUnitPrice"
        lblUnitPrice.Size = New Size(86, 25)
        lblUnitPrice.TabIndex = 0
        lblUnitPrice.Text = "Unit Price"
        ' 
        ' lblCategory
        ' 
        lblCategory.AutoSize = True
        lblCategory.Location = New Point(24, 84)
        lblCategory.Name = "lblCategory"
        lblCategory.Size = New Size(84, 25)
        lblCategory.TabIndex = 0
        lblCategory.Text = "Category"
        ' 
        ' lblProductName
        ' 
        lblProductName.AutoSize = True
        lblProductName.Location = New Point(260, 36)
        lblProductName.Name = "lblProductName"
        lblProductName.Size = New Size(126, 25)
        lblProductName.TabIndex = 0
        lblProductName.Text = "Product Name"
        ' 
        ' lblProductCode
        ' 
        lblProductCode.AutoSize = True
        lblProductCode.Location = New Point(17, 40)
        lblProductCode.Name = "lblProductCode"
        lblProductCode.Size = New Size(121, 25)
        lblProductCode.TabIndex = 0
        lblProductCode.Text = "Product Code"
        ' 
        ' grpSearchCOntrol
        ' 
        grpSearchCOntrol.Controls.Add(chkIncludeInactive)
        grpSearchCOntrol.Controls.Add(cboCategoryFilter)
        grpSearchCOntrol.Controls.Add(lblSearchProducts)
        grpSearchCOntrol.Controls.Add(txtProductSearch)
        grpSearchCOntrol.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0)
        grpSearchCOntrol.Location = New Point(617, 28)
        grpSearchCOntrol.Name = "grpSearchCOntrol"
        grpSearchCOntrol.Size = New Size(385, 178)
        grpSearchCOntrol.TabIndex = 1
        grpSearchCOntrol.TabStop = False
        ' 
        ' chkIncludeInactive
        ' 
        chkIncludeInactive.AutoSize = True
        chkIncludeInactive.Location = New Point(118, 123)
        chkIncludeInactive.Name = "chkIncludeInactive"
        chkIncludeInactive.Size = New Size(231, 29)
        chkIncludeInactive.TabIndex = 3
        chkIncludeInactive.Text = "Include inactive products"
        chkIncludeInactive.UseVisualStyleBackColor = True
        ' 
        ' cboCategoryFilter
        ' 
        cboCategoryFilter.FormattingEnabled = True
        cboCategoryFilter.Location = New Point(118, 84)
        cboCategoryFilter.Name = "cboCategoryFilter"
        cboCategoryFilter.Size = New Size(192, 33)
        cboCategoryFilter.TabIndex = 2
        cboCategoryFilter.Text = "All Categories"
        ' 
        ' lblSearchProducts
        ' 
        lblSearchProducts.AutoSize = True
        lblSearchProducts.Location = New Point(24, 31)
        lblSearchProducts.Name = "lblSearchProducts"
        lblSearchProducts.Size = New Size(64, 25)
        lblSearchProducts.TabIndex = 0
        lblSearchProducts.Text = "Search"
        ' 
        ' txtProductSearch
        ' 
        txtProductSearch.BorderStyle = BorderStyle.FixedSingle
        txtProductSearch.Location = New Point(118, 31)
        txtProductSearch.Name = "txtProductSearch"
        txtProductSearch.Size = New Size(186, 31)
        txtProductSearch.TabIndex = 1
        ' 
        ' dgvProducts
        ' 
        dgvProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvProducts.Location = New Point(12, 467)
        dgvProducts.Name = "dgvProducts"
        dgvProducts.RowHeadersWidth = 51
        dgvProducts.Size = New Size(966, 234)
        dgvProducts.TabIndex = 2
        ' 
        ' grpStatistics
        ' 
        grpStatistics.Controls.Add(lblInventoryStatus)
        grpStatistics.Controls.Add(lblInventoryValue)
        grpStatistics.Controls.Add(lblStockUnits)
        grpStatistics.Controls.Add(lblLowStockCount)
        grpStatistics.Controls.Add(lblProductCount)
        grpStatistics.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0)
        grpStatistics.Location = New Point(617, 212)
        grpStatistics.Name = "grpStatistics"
        grpStatistics.Size = New Size(440, 162)
        grpStatistics.TabIndex = 4
        grpStatistics.TabStop = False
        ' 
        ' lblInventoryStatus
        ' 
        lblInventoryStatus.AutoSize = True
        lblInventoryStatus.Location = New Point(31, 115)
        lblInventoryStatus.Name = "lblInventoryStatus"
        lblInventoryStatus.Size = New Size(60, 25)
        lblInventoryStatus.TabIndex = 0
        lblInventoryStatus.Text = "Ready"
        ' 
        ' lblInventoryValue
        ' 
        lblInventoryValue.AutoSize = True
        lblInventoryValue.Location = New Point(144, 65)
        lblInventoryValue.Name = "lblInventoryValue"
        lblInventoryValue.Size = New Size(187, 25)
        lblInventoryValue.TabIndex = 0
        lblInventoryValue.Text = "Inventory Value: $0.00"
        ' 
        ' lblStockUnits
        ' 
        lblStockUnits.AutoSize = True
        lblStockUnits.Location = New Point(20, 68)
        lblStockUnits.Name = "lblStockUnits"
        lblStockUnits.Size = New Size(71, 25)
        lblStockUnits.TabIndex = 0
        lblStockUnits.Text = "Units: 0"
        ' 
        ' lblLowStockCount
        ' 
        lblLowStockCount.AutoSize = True
        lblLowStockCount.Location = New Point(181, 27)
        lblLowStockCount.Name = "lblLowStockCount"
        lblLowStockCount.Size = New Size(111, 25)
        lblLowStockCount.TabIndex = 0
        lblLowStockCount.Text = "Low Stock: 0"
        ' 
        ' lblProductCount
        ' 
        lblProductCount.AutoSize = True
        lblProductCount.Location = New Point(18, 27)
        lblProductCount.Name = "lblProductCount"
        lblProductCount.Size = New Size(101, 25)
        lblProductCount.TabIndex = 0
        lblProductCount.Text = "Products: 0"
        ' 
        ' btnInventory
        ' 
        btnInventory.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0)
        btnInventory.Location = New Point(626, 396)
        btnInventory.Name = "btnInventory"
        btnInventory.Size = New Size(184, 41)
        btnInventory.TabIndex = 5
        btnInventory.Text = "Inventory Catalogue"
        btnInventory.UseVisualStyleBackColor = True
        ' 
        ' ProductForm
        ' 
        AutoScaleDimensions = New SizeF(8.0F, 20.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1082, 713)
        Controls.Add(btnInventory)
        Controls.Add(grpStatistics)
        Controls.Add(dgvProducts)
        Controls.Add(grpSearchCOntrol)
        Controls.Add(grpProductRegistration)
        Name = "ProductForm"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Inventory Catalogue Manager"
        grpProductRegistration.ResumeLayout(False)
        grpProductRegistration.PerformLayout()
        CType(nudReorderLevel, ComponentModel.ISupportInitialize).EndInit()
        CType(nudAdjustment, ComponentModel.ISupportInitialize).EndInit()
        CType(NumericUpDown2, ComponentModel.ISupportInitialize).EndInit()
        CType(NumericUpDown1, ComponentModel.ISupportInitialize).EndInit()
        CType(nudOpeningStock, ComponentModel.ISupportInitialize).EndInit()
        CType(nudUnitPrice, ComponentModel.ISupportInitialize).EndInit()
        grpSearchCOntrol.ResumeLayout(False)
        grpSearchCOntrol.PerformLayout()
        CType(dgvProducts, ComponentModel.ISupportInitialize).EndInit()
        grpStatistics.ResumeLayout(False)
        grpStatistics.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents grpProductRegistration As GroupBox
    Friend WithEvents txtAdjustmentReason As TextBox
    Friend WithEvents txtProductCode As TextBox
    Friend WithEvents lblProductName As Label
    Friend WithEvents lblProductCode As Label
    Friend WithEvents nudUnitPrice As NumericUpDown
    Friend WithEvents cboCategory As ComboBox
    Friend WithEvents lblUnitPrice As Label
    Friend WithEvents lblCategory As Label
    Friend WithEvents nudOpeningStock As NumericUpDown
    Friend WithEvents lblOpeningStock As Label
    Friend WithEvents nudReorderLevel As NumericUpDown
    Friend WithEvents lblReorderLevel As Label
    Friend WithEvents btnClearProduct As Button
    Friend WithEvents btnRegisterProduct As Button
    Friend WithEvents grpSearchCOntrol As GroupBox
    Friend WithEvents lblSearchProducts As Label
    Friend WithEvents txtProductSearch As TextBox
    Friend WithEvents chkIncludeInactive As CheckBox
    Friend WithEvents cboCategoryFilter As ComboBox
    Friend WithEvents dgvProducts As DataGridView
    Friend WithEvents nudAdjustment As NumericUpDown
    Friend WithEvents NumericUpDown2 As NumericUpDown
    Friend WithEvents NumericUpDown1 As NumericUpDown
    Friend WithEvents lblAdjustmentReason As Label
    Friend WithEvents lblAdjustment As Label
    Friend WithEvents btnUndoAdjustment As Button
    Friend WithEvents btnApplyAdjustment As Button
    Friend WithEvents btnProcessRestock As Button
    Friend WithEvents btnQueueRestock As Button
    Friend WithEvents lblRestockQueue As Label
    Friend WithEvents txtProductName As TextBox
    Friend WithEvents grpStatistics As GroupBox
    Friend WithEvents lblInventoryValue As Label
    Friend WithEvents lblStockUnits As Label
    Friend WithEvents lblLowStockCount As Label
    Friend WithEvents lblProductCount As Label
    Friend WithEvents lblInventoryStatus As Label
    Friend WithEvents btnInventory As Button
End Class
