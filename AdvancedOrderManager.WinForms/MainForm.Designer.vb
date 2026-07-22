<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        lblCustomerName = New Label()
        txtCustomerName = New TextBox()
        lblProductName = New Label()
        txtProductName = New TextBox()
        lblQuantity = New Label()
        nudQuantity = New NumericUpDown()
        lblUnitPrice = New Label()
        nudUnitPrice = New NumericUpDown()
        btnAddLine = New Button()
        btnRemoveLine = New Button()
        dgvLines = New DataGridView()
        lblDraftTotal = New Label()
        btnCreateOrder = New Button()
        btnClearDraft = New Button()
        lblRecentOrders = New Button()
        lstOrders = New ListBox()
        lblStatus = New Label()
        CType(nudQuantity, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudUnitPrice, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvLines, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' lblCustomerName
        ' 
        lblCustomerName.AutoSize = True
        lblCustomerName.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblCustomerName.Location = New Point(65, 50)
        lblCustomerName.Name = "lblCustomerName"
        lblCustomerName.Size = New Size(141, 25)
        lblCustomerName.TabIndex = 0
        lblCustomerName.Text = "Customer Name"
        ' 
        ' txtCustomerName
        ' 
        txtCustomerName.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txtCustomerName.Location = New Point(234, 47)
        txtCustomerName.Name = "txtCustomerName"
        txtCustomerName.Size = New Size(258, 31)
        txtCustomerName.TabIndex = 1
        ' 
        ' lblProductName
        ' 
        lblProductName.AutoSize = True
        lblProductName.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblProductName.Location = New Point(80, 110)
        lblProductName.Name = "lblProductName"
        lblProductName.Size = New Size(126, 25)
        lblProductName.TabIndex = 0
        lblProductName.Text = "Product Name"
        ' 
        ' txtProductName
        ' 
        txtProductName.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txtProductName.Location = New Point(234, 104)
        txtProductName.Name = "txtProductName"
        txtProductName.Size = New Size(258, 31)
        txtProductName.TabIndex = 1
        ' 
        ' lblQuantity
        ' 
        lblQuantity.AutoSize = True
        lblQuantity.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblQuantity.Location = New Point(126, 155)
        lblQuantity.Name = "lblQuantity"
        lblQuantity.Size = New Size(80, 25)
        lblQuantity.TabIndex = 0
        lblQuantity.Text = "Quantity"
        ' 
        ' nudQuantity
        ' 
        nudQuantity.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        nudQuantity.Location = New Point(240, 158)
        nudQuantity.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        nudQuantity.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        nudQuantity.Name = "nudQuantity"
        nudQuantity.Size = New Size(247, 31)
        nudQuantity.TabIndex = 2
        nudQuantity.Value = New Decimal(New Integer() {1, 0, 0, 0})
        ' 
        ' lblUnitPrice
        ' 
        lblUnitPrice.AutoSize = True
        lblUnitPrice.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblUnitPrice.Location = New Point(120, 213)
        lblUnitPrice.Name = "lblUnitPrice"
        lblUnitPrice.Size = New Size(86, 25)
        lblUnitPrice.TabIndex = 0
        lblUnitPrice.Text = "Unit Price"
        ' 
        ' nudUnitPrice
        ' 
        nudUnitPrice.DecimalPlaces = 2
        nudUnitPrice.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        nudUnitPrice.Location = New Point(234, 213)
        nudUnitPrice.Maximum = New Decimal(New Integer() {1000000, 0, 0, 0})
        nudUnitPrice.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        nudUnitPrice.Name = "nudUnitPrice"
        nudUnitPrice.Size = New Size(247, 31)
        nudUnitPrice.TabIndex = 2
        nudUnitPrice.Value = New Decimal(New Integer() {1, 0, 0, 0})
        ' 
        ' btnAddLine
        ' 
        btnAddLine.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        btnAddLine.Location = New Point(541, 50)
        btnAddLine.Name = "btnAddLine"
        btnAddLine.Size = New Size(120, 35)
        btnAddLine.TabIndex = 3
        btnAddLine.Text = "Add Line"
        btnAddLine.UseVisualStyleBackColor = True
        ' 
        ' btnRemoveLine
        ' 
        btnRemoveLine.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        btnRemoveLine.Location = New Point(703, 50)
        btnRemoveLine.Name = "btnRemoveLine"
        btnRemoveLine.Size = New Size(177, 35)
        btnRemoveLine.TabIndex = 4
        btnRemoveLine.Text = "Remove Selected"
        btnRemoveLine.UseVisualStyleBackColor = True
        ' 
        ' dgvLines
        ' 
        dgvLines.AllowUserToAddRows = False
        dgvLines.AllowUserToDeleteRows = False
        dgvLines.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvLines.Location = New Point(33, 325)
        dgvLines.MultiSelect = False
        dgvLines.Name = "dgvLines"
        dgvLines.ReadOnly = True
        dgvLines.RowHeadersWidth = 51
        dgvLines.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvLines.Size = New Size(572, 207)
        dgvLines.TabIndex = 5
        ' 
        ' lblDraftTotal
        ' 
        lblDraftTotal.AutoSize = True
        lblDraftTotal.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblDraftTotal.Location = New Point(550, 202)
        lblDraftTotal.Name = "lblDraftTotal"
        lblDraftTotal.Size = New Size(147, 25)
        lblDraftTotal.TabIndex = 0
        lblDraftTotal.Text = "Draft Total: $0.00"
        ' 
        ' btnCreateOrder
        ' 
        btnCreateOrder.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        btnCreateOrder.Location = New Point(541, 105)
        btnCreateOrder.Name = "btnCreateOrder"
        btnCreateOrder.Size = New Size(146, 35)
        btnCreateOrder.TabIndex = 6
        btnCreateOrder.Text = "Create Order"
        btnCreateOrder.UseVisualStyleBackColor = True
        ' 
        ' btnClearDraft
        ' 
        btnClearDraft.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        btnClearDraft.Location = New Point(703, 106)
        btnClearDraft.Name = "btnClearDraft"
        btnClearDraft.Size = New Size(129, 33)
        btnClearDraft.TabIndex = 7
        btnClearDraft.Text = "Clear Draft"
        btnClearDraft.UseVisualStyleBackColor = True
        ' 
        ' lblRecentOrders
        ' 
        lblRecentOrders.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblRecentOrders.Location = New Point(541, 150)
        lblRecentOrders.Name = "lblRecentOrders"
        lblRecentOrders.Size = New Size(146, 35)
        lblRecentOrders.TabIndex = 6
        lblRecentOrders.Text = "Recent Orders"
        lblRecentOrders.UseVisualStyleBackColor = True
        ' 
        ' lstOrders
        ' 
        lstOrders.FormattingEnabled = True
        lstOrders.Location = New Point(692, 325)
        lstOrders.Name = "lstOrders"
        lstOrders.Size = New Size(206, 204)
        lstOrders.TabIndex = 8
        ' 
        ' lblStatus
        ' 
        lblStatus.AutoSize = True
        lblStatus.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblStatus.Location = New Point(550, 251)
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New Size(60, 25)
        lblStatus.TabIndex = 0
        lblStatus.Text = "Ready"
        ' 
        ' MainForm
        ' 
        AutoScaleDimensions = New SizeF(8.0F, 20.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(928, 571)
        Controls.Add(lstOrders)
        Controls.Add(btnClearDraft)
        Controls.Add(lblRecentOrders)
        Controls.Add(btnCreateOrder)
        Controls.Add(dgvLines)
        Controls.Add(btnRemoveLine)
        Controls.Add(btnAddLine)
        Controls.Add(nudUnitPrice)
        Controls.Add(nudQuantity)
        Controls.Add(txtProductName)
        Controls.Add(txtCustomerName)
        Controls.Add(lblUnitPrice)
        Controls.Add(lblStatus)
        Controls.Add(lblQuantity)
        Controls.Add(lblProductName)
        Controls.Add(lblDraftTotal)
        Controls.Add(lblCustomerName)
        Name = "MainForm"
        Text = "Professional Order Manager"
        CType(nudQuantity, ComponentModel.ISupportInitialize).EndInit()
        CType(nudUnitPrice, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvLines, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblCustomerName As Label
    Friend WithEvents txtCustomerName As TextBox
    Friend WithEvents lblProductName As Label
    Friend WithEvents txtProductName As TextBox
    Friend WithEvents lblQuantity As Label
    Friend WithEvents nudQuantity As NumericUpDown
    Friend WithEvents lblUnitPrice As Label
    Friend WithEvents nudUnitPrice As NumericUpDown
    Friend WithEvents btnAddLine As Button
    Friend WithEvents btnRemoveLine As Button
    Friend WithEvents dgvLines As DataGridView
    Friend WithEvents lblDraftTotal As Label
    Friend WithEvents btnCreateOrder As Button
    Friend WithEvents btnClearDraft As Button
    Friend WithEvents lblRecentOrders As Button
    Friend WithEvents lstOrders As ListBox
    Friend WithEvents lblStatus As Label

End Class
