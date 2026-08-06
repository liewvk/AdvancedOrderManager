<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OrderDatabaseForm
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
        lblOrderId = New Label()
        txtOrderId = New TextBox()
        lblCustomerName = New Label()
        txtCustomerName = New TextBox()
        lblQuantity = New Label()
        nudQuantity = New NumericUpDown()
        lblUnitPrice = New Label()
        nudUnitPrice = New NumericUpDown()
        chkPriority = New CheckBox()
        btnSave = New Button()
        btnLoad = New Button()
        btnFind = New Button()
        btnDeleteSelected = New Button()
        btnDeleteAll = New Button()
        dgvOrders = New DataGridView()
        lblStatus = New Label()
        CType(nudQuantity, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudUnitPrice, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvOrders, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' lblOrderId
        ' 
        lblOrderId.AutoSize = True
        lblOrderId.Location = New Point(122, 38)
        lblOrderId.Name = "lblOrderId"
        lblOrderId.Size = New Size(69, 20)
        lblOrderId.TabIndex = 0
        lblOrderId.Text = "Order ID:"
        ' 
        ' txtOrderId
        ' 
        txtOrderId.BorderStyle = BorderStyle.FixedSingle
        txtOrderId.Location = New Point(206, 31)
        txtOrderId.Name = "txtOrderId"
        txtOrderId.Size = New Size(193, 27)
        txtOrderId.TabIndex = 1
        ' 
        ' lblCustomerName
        ' 
        lblCustomerName.AutoSize = True
        lblCustomerName.Location = New Point(75, 82)
        lblCustomerName.Name = "lblCustomerName"
        lblCustomerName.Size = New Size(116, 20)
        lblCustomerName.TabIndex = 0
        lblCustomerName.Text = "Customer name:"
        ' 
        ' txtCustomerName
        ' 
        txtCustomerName.BorderStyle = BorderStyle.FixedSingle
        txtCustomerName.Location = New Point(206, 75)
        txtCustomerName.Name = "txtCustomerName"
        txtCustomerName.Size = New Size(260, 27)
        txtCustomerName.TabIndex = 1
        ' 
        ' lblQuantity
        ' 
        lblQuantity.AutoSize = True
        lblQuantity.Location = New Point(123, 128)
        lblQuantity.Name = "lblQuantity"
        lblQuantity.Size = New Size(68, 20)
        lblQuantity.TabIndex = 0
        lblQuantity.Text = "Quantity:"
        ' 
        ' nudQuantity
        ' 
        nudQuantity.Location = New Point(215, 124)
        nudQuantity.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        nudQuantity.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        nudQuantity.Name = "nudQuantity"
        nudQuantity.Size = New Size(190, 27)
        nudQuantity.TabIndex = 2
        nudQuantity.Value = New Decimal(New Integer() {1, 0, 0, 0})
        ' 
        ' lblUnitPrice
        ' 
        lblUnitPrice.AutoSize = True
        lblUnitPrice.Location = New Point(122, 174)
        lblUnitPrice.Name = "lblUnitPrice"
        lblUnitPrice.Size = New Size(75, 20)
        lblUnitPrice.TabIndex = 0
        lblUnitPrice.Text = "Unit Price:"
        ' 
        ' nudUnitPrice
        ' 
        nudUnitPrice.DecimalPlaces = 2
        nudUnitPrice.Location = New Point(209, 167)
        nudUnitPrice.Maximum = New Decimal(New Integer() {100000, 0, 0, 0})
        nudUnitPrice.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        nudUnitPrice.Name = "nudUnitPrice"
        nudUnitPrice.Size = New Size(190, 27)
        nudUnitPrice.TabIndex = 2
        nudUnitPrice.Value = New Decimal(New Integer() {1, 0, 0, 0})
        ' 
        ' chkPriority
        ' 
        chkPriority.AutoSize = True
        chkPriority.Location = New Point(132, 220)
        chkPriority.Name = "chkPriority"
        chkPriority.Size = New Size(118, 24)
        chkPriority.TabIndex = 3
        chkPriority.Text = "Priority order"
        chkPriority.UseVisualStyleBackColor = True
        ' 
        ' btnSave
        ' 
        btnSave.Location = New Point(582, 31)
        btnSave.Name = "btnSave"
        btnSave.Size = New Size(143, 35)
        btnSave.TabIndex = 4
        btnSave.Text = "Save Order"
        btnSave.UseVisualStyleBackColor = True
        ' 
        ' btnLoad
        ' 
        btnLoad.Location = New Point(582, 75)
        btnLoad.Name = "btnLoad"
        btnLoad.Size = New Size(143, 38)
        btnLoad.TabIndex = 5
        btnLoad.Text = "Load All"
        btnLoad.UseVisualStyleBackColor = True
        ' 
        ' btnFind
        ' 
        btnFind.Location = New Point(585, 128)
        btnFind.Name = "btnFind"
        btnFind.Size = New Size(140, 35)
        btnFind.TabIndex = 6
        btnFind.Text = "Find Order"
        btnFind.UseVisualStyleBackColor = True
        ' 
        ' btnDeleteSelected
        ' 
        btnDeleteSelected.Location = New Point(585, 177)
        btnDeleteSelected.Name = "btnDeleteSelected"
        btnDeleteSelected.Size = New Size(140, 36)
        btnDeleteSelected.TabIndex = 7
        btnDeleteSelected.Text = "Delete Selected"
        btnDeleteSelected.UseVisualStyleBackColor = True
        ' 
        ' btnDeleteAll
        ' 
        btnDeleteAll.Location = New Point(586, 227)
        btnDeleteAll.Name = "btnDeleteAll"
        btnDeleteAll.Size = New Size(136, 36)
        btnDeleteAll.TabIndex = 8
        btnDeleteAll.Text = "Delete All"
        btnDeleteAll.UseVisualStyleBackColor = True
        ' 
        ' dgvOrders
        ' 
        dgvOrders.AllowUserToAddRows = False
        dgvOrders.AllowUserToDeleteRows = False
        dgvOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvOrders.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvOrders.Location = New Point(66, 320)
        dgvOrders.Name = "dgvOrders"
        dgvOrders.ReadOnly = True
        dgvOrders.RowHeadersWidth = 51
        dgvOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvOrders.Size = New Size(659, 168)
        dgvOrders.TabIndex = 9
        ' 
        ' lblStatus
        ' 
        lblStatus.AutoSize = True
        lblStatus.Location = New Point(127, 261)
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New Size(50, 20)
        lblStatus.TabIndex = 10
        lblStatus.Text = "Ready"
        ' 
        ' OrderDatabaseForm
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 500)
        Controls.Add(lblStatus)
        Controls.Add(dgvOrders)
        Controls.Add(btnDeleteAll)
        Controls.Add(btnDeleteSelected)
        Controls.Add(btnFind)
        Controls.Add(btnLoad)
        Controls.Add(btnSave)
        Controls.Add(chkPriority)
        Controls.Add(nudUnitPrice)
        Controls.Add(nudQuantity)
        Controls.Add(txtCustomerName)
        Controls.Add(txtOrderId)
        Controls.Add(lblUnitPrice)
        Controls.Add(lblQuantity)
        Controls.Add(lblCustomerName)
        Controls.Add(lblOrderId)
        Name = "OrderDatabaseForm"
        Text = "Order Database History"
        CType(nudQuantity, ComponentModel.ISupportInitialize).EndInit()
        CType(nudUnitPrice, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvOrders, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblOrderId As Label
    Friend WithEvents txtOrderId As TextBox
    Friend WithEvents lblCustomerName As Label
    Friend WithEvents txtCustomerName As TextBox
    Friend WithEvents lblQuantity As Label
    Friend WithEvents nudQuantity As NumericUpDown
    Friend WithEvents lblUnitPrice As Label
    Friend WithEvents nudUnitPrice As NumericUpDown
    Friend WithEvents chkPriority As CheckBox
    Friend WithEvents btnSave As Button
    Friend WithEvents btnLoad As Button
    Friend WithEvents btnFind As Button
    Friend WithEvents btnDeleteSelected As Button
    Friend WithEvents btnDeleteAll As Button
    Friend WithEvents dgvOrders As DataGridView
    Friend WithEvents lblStatus As Label
End Class
