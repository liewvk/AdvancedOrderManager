<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EntityFrameworkQueryForm
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
        lblCustomerFilter = New Label()
        txtCustomerFilter = New TextBox()
        lblStatusFilter = New Label()
        cmbStatus = New ComboBox()
        chkPriorityOnly = New CheckBox()
        btnSearch = New Button()
        btnReset = New Button()
        dgvResults = New DataGridView()
        lblTotalOrdersCaption = New Label()
        lblTotalOrdersValue = New Label()
        lblPriorityOrdersCaption = New Label()
        lblPriorityOrdersValue = New Label()
        lblTotalAmountCaption = New Label()
        lblTotalAmountValue = New Label()
        lblAverageAmountCaption = New Label()
        lblAverageAmountValue = New Label()
        lblStatus = New Label()
        CType(dgvResults, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' lblCustomerFilter
        ' 
        lblCustomerFilter.AutoSize = True
        lblCustomerFilter.Location = New Point(76, 74)
        lblCustomerFilter.Name = "lblCustomerFilter"
        lblCustomerFilter.Size = New Size(134, 20)
        lblCustomerFilter.TabIndex = 0
        lblCustomerFilter.Text = "Customer contains:"
        ' 
        ' txtCustomerFilter
        ' 
        txtCustomerFilter.Location = New Point(274, 76)
        txtCustomerFilter.Name = "txtCustomerFilter"
        txtCustomerFilter.Size = New Size(164, 27)
        txtCustomerFilter.TabIndex = 1
        ' 
        ' lblStatusFilter
        ' 
        lblStatusFilter.AutoSize = True
        lblStatusFilter.Location = New Point(158, 119)
        lblStatusFilter.Name = "lblStatusFilter"
        lblStatusFilter.Size = New Size(52, 20)
        lblStatusFilter.TabIndex = 0
        lblStatusFilter.Text = "Status:"
        ' 
        ' cmbStatus
        ' 
        cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList
        cmbStatus.FormattingEnabled = True
        cmbStatus.Location = New Point(274, 119)
        cmbStatus.Name = "cmbStatus"
        cmbStatus.Size = New Size(162, 28)
        cmbStatus.TabIndex = 2
        ' 
        ' chkPriorityOnly
        ' 
        chkPriorityOnly.AutoSize = True
        chkPriorityOnly.Location = New Point(156, 180)
        chkPriorityOnly.Name = "chkPriorityOnly"
        chkPriorityOnly.Size = New Size(156, 24)
        chkPriorityOnly.TabIndex = 3
        chkPriorityOnly.Text = "Priority orders only"
        chkPriorityOnly.UseVisualStyleBackColor = True
        ' 
        ' btnSearch
        ' 
        btnSearch.Location = New Point(592, 71)
        btnSearch.Name = "btnSearch"
        btnSearch.Size = New Size(119, 33)
        btnSearch.TabIndex = 4
        btnSearch.Text = "Search"
        btnSearch.UseVisualStyleBackColor = True
        ' 
        ' btnReset
        ' 
        btnReset.Location = New Point(765, 76)
        btnReset.Name = "btnReset"
        btnReset.Size = New Size(107, 28)
        btnReset.TabIndex = 5
        btnReset.Text = "Reset"
        btnReset.UseVisualStyleBackColor = True
        ' 
        ' dgvResults
        ' 
        dgvResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvResults.Location = New Point(96, 364)
        dgvResults.Name = "dgvResults"
        dgvResults.ReadOnly = True
        dgvResults.RowHeadersWidth = 51
        dgvResults.Size = New Size(757, 163)
        dgvResults.TabIndex = 6
        ' 
        ' lblTotalOrdersCaption
        ' 
        lblTotalOrdersCaption.AutoSize = True
        lblTotalOrdersCaption.Location = New Point(96, 236)
        lblTotalOrdersCaption.Name = "lblTotalOrdersCaption"
        lblTotalOrdersCaption.Size = New Size(91, 20)
        lblTotalOrdersCaption.TabIndex = 0
        lblTotalOrdersCaption.Text = "Total orders:"
        ' 
        ' lblTotalOrdersValue
        ' 
        lblTotalOrdersValue.AutoSize = True
        lblTotalOrdersValue.Location = New Point(216, 236)
        lblTotalOrdersValue.Name = "lblTotalOrdersValue"
        lblTotalOrdersValue.Size = New Size(17, 20)
        lblTotalOrdersValue.TabIndex = 0
        lblTotalOrdersValue.Text = "0"
        ' 
        ' lblPriorityOrdersCaption
        ' 
        lblPriorityOrdersCaption.AutoSize = True
        lblPriorityOrdersCaption.Location = New Point(292, 236)
        lblPriorityOrdersCaption.Name = "lblPriorityOrdersCaption"
        lblPriorityOrdersCaption.Size = New Size(105, 20)
        lblPriorityOrdersCaption.TabIndex = 0
        lblPriorityOrdersCaption.Text = "Priority orders:"
        ' 
        ' lblPriorityOrdersValue
        ' 
        lblPriorityOrdersValue.AutoSize = True
        lblPriorityOrdersValue.Location = New Point(413, 236)
        lblPriorityOrdersValue.Name = "lblPriorityOrdersValue"
        lblPriorityOrdersValue.Size = New Size(17, 20)
        lblPriorityOrdersValue.TabIndex = 0
        lblPriorityOrdersValue.Text = "0"
        ' 
        ' lblTotalAmountCaption
        ' 
        lblTotalAmountCaption.AutoSize = True
        lblTotalAmountCaption.Location = New Point(472, 236)
        lblTotalAmountCaption.Name = "lblTotalAmountCaption"
        lblTotalAmountCaption.Size = New Size(100, 20)
        lblTotalAmountCaption.TabIndex = 0
        lblTotalAmountCaption.Text = "Total amount:"
        ' 
        ' lblTotalAmountValue
        ' 
        lblTotalAmountValue.AutoSize = True
        lblTotalAmountValue.Location = New Point(592, 236)
        lblTotalAmountValue.Name = "lblTotalAmountValue"
        lblTotalAmountValue.Size = New Size(36, 20)
        lblTotalAmountValue.TabIndex = 0
        lblTotalAmountValue.Text = "0.00"
        ' 
        ' lblAverageAmountCaption
        ' 
        lblAverageAmountCaption.AutoSize = True
        lblAverageAmountCaption.Location = New Point(664, 236)
        lblAverageAmountCaption.Name = "lblAverageAmountCaption"
        lblAverageAmountCaption.Size = New Size(122, 20)
        lblAverageAmountCaption.TabIndex = 0
        lblAverageAmountCaption.Text = "Average amount:"
        ' 
        ' lblAverageAmountValue
        ' 
        lblAverageAmountValue.AutoSize = True
        lblAverageAmountValue.Location = New Point(805, 236)
        lblAverageAmountValue.Name = "lblAverageAmountValue"
        lblAverageAmountValue.Size = New Size(36, 20)
        lblAverageAmountValue.TabIndex = 0
        lblAverageAmountValue.Text = "0.00"
        ' 
        ' lblStatus
        ' 
        lblStatus.AutoSize = True
        lblStatus.Location = New Point(96, 297)
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New Size(50, 20)
        lblStatus.TabIndex = 0
        lblStatus.Text = "Ready"
        ' 
        ' EntityFrameworkQueryForm
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(982, 603)
        Controls.Add(dgvResults)
        Controls.Add(btnReset)
        Controls.Add(btnSearch)
        Controls.Add(chkPriorityOnly)
        Controls.Add(cmbStatus)
        Controls.Add(txtCustomerFilter)
        Controls.Add(lblStatusFilter)
        Controls.Add(lblTotalOrdersValue)
        Controls.Add(lblPriorityOrdersValue)
        Controls.Add(lblTotalAmountValue)
        Controls.Add(lblAverageAmountValue)
        Controls.Add(lblAverageAmountCaption)
        Controls.Add(lblTotalAmountCaption)
        Controls.Add(lblPriorityOrdersCaption)
        Controls.Add(lblStatus)
        Controls.Add(lblTotalOrdersCaption)
        Controls.Add(lblCustomerFilter)
        Name = "EntityFrameworkQueryForm"
        Text = "EF Core Order Queries"
        CType(dgvResults, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblCustomerFilter As Label
    Friend WithEvents txtCustomerFilter As TextBox
    Friend WithEvents lblStatusFilter As Label
    Friend WithEvents cmbStatus As ComboBox
    Friend WithEvents chkPriorityOnly As CheckBox
    Friend WithEvents btnSearch As Button
    Friend WithEvents btnReset As Button
    Friend WithEvents dgvResults As DataGridView
    Friend WithEvents lblTotalOrdersCaption As Label
    Friend WithEvents lblTotalOrdersValue As Label
    Friend WithEvents lblPriorityOrdersCaption As Label
    Friend WithEvents lblPriorityOrdersValue As Label
    Friend WithEvents lblTotalAmountCaption As Label
    Friend WithEvents lblTotalAmountValue As Label
    Friend WithEvents lblAverageAmountCaption As Label
    Friend WithEvents lblAverageAmountValue As Label
    Friend WithEvents lblStatus As Label
End Class
