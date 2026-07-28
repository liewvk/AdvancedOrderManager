<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OrderProcessingEventForm
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
        txtOrderNumber = New TextBox()
        txtCustomerName = New TextBox()
        nudUnitPrice = New NumericUpDown()
        chkPriority = New CheckBox()
        chkEnableAudit = New CheckBox()
        btnProcessOrder = New Button()
        btnClearActivity = New Button()
        lstOrderActivity = New ListBox()
        lblProcessedCount = New Label()
        lblRejectedCount = New Label()
        lblTotalRevenue = New Label()
        lblProcessingStatus = New Label()
        lblOrderNumner = New Label()
        lblCustomerName = New Label()
        lblQuantity = New Label()
        nudQuantity = New NumericUpDown()
        Label1 = New Label()
        btnOpenReport = New Button()
        chkApplyTax = New CheckBox()
        CType(nudUnitPrice, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudQuantity, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' txtOrderNumber
        ' 
        txtOrderNumber.BorderStyle = BorderStyle.FixedSingle
        txtOrderNumber.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txtOrderNumber.Location = New Point(187, 65)
        txtOrderNumber.Name = "txtOrderNumber"
        txtOrderNumber.Size = New Size(185, 31)
        txtOrderNumber.TabIndex = 0
        ' 
        ' txtCustomerName
        ' 
        txtCustomerName.BorderStyle = BorderStyle.FixedSingle
        txtCustomerName.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txtCustomerName.Location = New Point(187, 128)
        txtCustomerName.Name = "txtCustomerName"
        txtCustomerName.Size = New Size(232, 31)
        txtCustomerName.TabIndex = 0
        ' 
        ' nudUnitPrice
        ' 
        nudUnitPrice.DecimalPlaces = 2
        nudUnitPrice.Location = New Point(187, 238)
        nudUnitPrice.Name = "nudUnitPrice"
        nudUnitPrice.Size = New Size(155, 27)
        nudUnitPrice.TabIndex = 1
        ' 
        ' chkPriority
        ' 
        chkPriority.AutoSize = True
        chkPriority.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        chkPriority.Location = New Point(35, 285)
        chkPriority.Name = "chkPriority"
        chkPriority.Size = New Size(164, 29)
        chkPriority.TabIndex = 2
        chkPriority.Text = "Priority handling"
        chkPriority.UseVisualStyleBackColor = True
        ' 
        ' chkEnableAudit
        ' 
        chkEnableAudit.AutoSize = True
        chkEnableAudit.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        chkEnableAudit.Location = New Point(267, 285)
        chkEnableAudit.Name = "chkEnableAudit"
        chkEnableAudit.Size = New Size(217, 29)
        chkEnableAudit.TabIndex = 2
        chkEnableAudit.Text = "Enable audit subscriber"
        chkEnableAudit.UseVisualStyleBackColor = True
        ' 
        ' btnProcessOrder
        ' 
        btnProcessOrder.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        btnProcessOrder.Location = New Point(35, 360)
        btnProcessOrder.Name = "btnProcessOrder"
        btnProcessOrder.Size = New Size(155, 38)
        btnProcessOrder.TabIndex = 3
        btnProcessOrder.Text = "Process Order"
        btnProcessOrder.UseVisualStyleBackColor = True
        ' 
        ' btnClearActivity
        ' 
        btnClearActivity.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        btnClearActivity.Location = New Point(267, 360)
        btnClearActivity.Name = "btnClearActivity"
        btnClearActivity.Size = New Size(152, 38)
        btnClearActivity.TabIndex = 4
        btnClearActivity.Text = "Clear Activity"
        btnClearActivity.UseVisualStyleBackColor = True
        ' 
        ' lstOrderActivity
        ' 
        lstOrderActivity.FormattingEnabled = True
        lstOrderActivity.Location = New Point(42, 428)
        lstOrderActivity.Name = "lstOrderActivity"
        lstOrderActivity.Size = New Size(707, 144)
        lstOrderActivity.TabIndex = 5
        ' 
        ' lblProcessedCount
        ' 
        lblProcessedCount.AutoSize = True
        lblProcessedCount.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblProcessedCount.Location = New Point(562, 75)
        lblProcessedCount.Name = "lblProcessedCount"
        lblProcessedCount.Size = New Size(111, 25)
        lblProcessedCount.TabIndex = 6
        lblProcessedCount.Text = "Processed: 0"
        ' 
        ' lblRejectedCount
        ' 
        lblRejectedCount.AutoSize = True
        lblRejectedCount.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblRejectedCount.Location = New Point(562, 131)
        lblRejectedCount.Name = "lblRejectedCount"
        lblRejectedCount.Size = New Size(97, 25)
        lblRejectedCount.TabIndex = 6
        lblRejectedCount.Text = "Rejected: 0"
        ' 
        ' lblTotalRevenue
        ' 
        lblTotalRevenue.AutoSize = True
        lblTotalRevenue.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblTotalRevenue.Location = New Point(562, 185)
        lblTotalRevenue.Name = "lblTotalRevenue"
        lblTotalRevenue.Size = New Size(131, 25)
        lblTotalRevenue.TabIndex = 6
        lblTotalRevenue.Text = "Revenue: $0.00"
        ' 
        ' lblProcessingStatus
        ' 
        lblProcessingStatus.AutoSize = True
        lblProcessingStatus.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblProcessingStatus.Location = New Point(562, 234)
        lblProcessingStatus.Name = "lblProcessingStatus"
        lblProcessingStatus.Size = New Size(60, 25)
        lblProcessingStatus.TabIndex = 6
        lblProcessingStatus.Text = "Ready"
        ' 
        ' lblOrderNumner
        ' 
        lblOrderNumner.AutoSize = True
        lblOrderNumner.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblOrderNumner.Location = New Point(32, 65)
        lblOrderNumner.Name = "lblOrderNumner"
        lblOrderNumner.Size = New Size(132, 25)
        lblOrderNumner.TabIndex = 6
        lblOrderNumner.Text = "Order Number:"
        ' 
        ' lblCustomerName
        ' 
        lblCustomerName.AutoSize = True
        lblCustomerName.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblCustomerName.Location = New Point(19, 128)
        lblCustomerName.Name = "lblCustomerName"
        lblCustomerName.Size = New Size(145, 25)
        lblCustomerName.TabIndex = 6
        lblCustomerName.Text = "Customer Name:"
        ' 
        ' lblQuantity
        ' 
        lblQuantity.AutoSize = True
        lblQuantity.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblQuantity.Location = New Point(86, 189)
        lblQuantity.Name = "lblQuantity"
        lblQuantity.Size = New Size(84, 25)
        lblQuantity.TabIndex = 6
        lblQuantity.Text = "Quantity:"
        ' 
        ' nudQuantity
        ' 
        nudQuantity.Location = New Point(187, 187)
        nudQuantity.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        nudQuantity.Name = "nudQuantity"
        nudQuantity.Size = New Size(155, 27)
        nudQuantity.TabIndex = 1
        nudQuantity.Value = New Decimal(New Integer() {1, 0, 0, 0})
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(80, 236)
        Label1.Name = "Label1"
        Label1.Size = New Size(90, 25)
        Label1.TabIndex = 6
        Label1.Text = "Unit Price:"
        ' 
        ' btnOpenReport
        ' 
        btnOpenReport.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        btnOpenReport.Location = New Point(572, 292)
        btnOpenReport.Name = "btnOpenReport"
        btnOpenReport.Size = New Size(224, 44)
        btnOpenReport.TabIndex = 7
        btnOpenReport.Text = "Open Report Centre"
        btnOpenReport.UseVisualStyleBackColor = True
        ' 
        ' chkApplyTax
        ' 
        chkApplyTax.AutoSize = True
        chkApplyTax.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        chkApplyTax.Location = New Point(32, 320)
        chkApplyTax.Name = "chkApplyTax"
        chkApplyTax.Size = New Size(264, 29)
        chkApplyTax.TabIndex = 8
        chkApplyTax.Text = "Apply 6% Demonstration Tax"
        chkApplyTax.UseVisualStyleBackColor = True
        ' 
        ' OrderProcessingEventForm
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(882, 603)
        Controls.Add(chkApplyTax)
        Controls.Add(btnOpenReport)
        Controls.Add(Label1)
        Controls.Add(lblQuantity)
        Controls.Add(lblCustomerName)
        Controls.Add(lblOrderNumner)
        Controls.Add(lblProcessingStatus)
        Controls.Add(lblTotalRevenue)
        Controls.Add(lblRejectedCount)
        Controls.Add(lblProcessedCount)
        Controls.Add(lstOrderActivity)
        Controls.Add(btnClearActivity)
        Controls.Add(btnProcessOrder)
        Controls.Add(chkEnableAudit)
        Controls.Add(chkPriority)
        Controls.Add(nudUnitPrice)
        Controls.Add(nudQuantity)
        Controls.Add(txtCustomerName)
        Controls.Add(txtOrderNumber)
        Name = "OrderProcessingEventForm"
        Text = "Order Processing Event Monitor"
        CType(nudUnitPrice, ComponentModel.ISupportInitialize).EndInit()
        CType(nudQuantity, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents txtOrderNumber As TextBox
    Friend WithEvents txtCustomerName As TextBox
    Friend WithEvents nudUnitPrice As NumericUpDown
    Friend WithEvents chkPriority As CheckBox
    Friend WithEvents chkEnableAudit As CheckBox
    Friend WithEvents btnProcessOrder As Button
    Friend WithEvents btnClearActivity As Button
    Friend WithEvents lstOrderActivity As ListBox
    Friend WithEvents lblProcessedCount As Label
    Friend WithEvents lblRejectedCount As Label
    Friend WithEvents lblTotalRevenue As Label
    Friend WithEvents lblProcessingStatus As Label
    Friend WithEvents lblOrderNumner As Label
    Friend WithEvents lblCustomerName As Label
    Friend WithEvents lblQuantity As Label
    Friend WithEvents nudQuantity As NumericUpDown
    Friend WithEvents Label1 As Label
    Friend WithEvents btnOpenReport As Button
    Friend WithEvents chkApplyTax As CheckBox
End Class
