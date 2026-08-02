<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AsyncOrderProcessingForm
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
        lblOrderCount = New Label()
        nudOrderCount = New NumericUpDown()
        btnStart = New Button()
        btnCancel = New Button()
        prgProcessing = New ProgressBar()
        lblStatus = New Label()
        txtLog = New TextBox()
        btnAsyncProcessing = New Button()
        CType(nudOrderCount, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' lblOrderCount
        ' 
        lblOrderCount.AutoSize = True
        lblOrderCount.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblOrderCount.Location = New Point(72, 52)
        lblOrderCount.Name = "lblOrderCount"
        lblOrderCount.Size = New Size(159, 25)
        lblOrderCount.TabIndex = 0
        lblOrderCount.Text = "Number of orders:"
        ' 
        ' nudOrderCount
        ' 
        nudOrderCount.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        nudOrderCount.Location = New Point(296, 54)
        nudOrderCount.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        nudOrderCount.Name = "nudOrderCount"
        nudOrderCount.Size = New Size(204, 31)
        nudOrderCount.TabIndex = 1
        nudOrderCount.Value = New Decimal(New Integer() {10, 0, 0, 0})
        ' 
        ' btnStart
        ' 
        btnStart.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        btnStart.Location = New Point(81, 125)
        btnStart.Name = "btnStart"
        btnStart.Size = New Size(173, 34)
        btnStart.TabIndex = 2
        btnStart.Text = "Start Processing"
        btnStart.UseVisualStyleBackColor = True
        ' 
        ' btnCancel
        ' 
        btnCancel.Enabled = False
        btnCancel.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        btnCancel.Location = New Point(296, 125)
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(186, 34)
        btnCancel.TabIndex = 3
        btnCancel.Text = "Cancel"
        btnCancel.UseVisualStyleBackColor = True
        ' 
        ' prgProcessing
        ' 
        prgProcessing.Location = New Point(81, 213)
        prgProcessing.Name = "prgProcessing"
        prgProcessing.Size = New Size(431, 35)
        prgProcessing.TabIndex = 4
        ' 
        ' lblStatus
        ' 
        lblStatus.AutoSize = True
        lblStatus.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblStatus.Location = New Point(84, 295)
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New Size(60, 25)
        lblStatus.TabIndex = 5
        lblStatus.Text = "Ready"
        ' 
        ' txtLog
        ' 
        txtLog.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txtLog.Location = New Point(308, 292)
        txtLog.Multiline = True
        txtLog.Name = "txtLog"
        txtLog.ReadOnly = True
        txtLog.ScrollBars = ScrollBars.Vertical
        txtLog.Size = New Size(148, 38)
        txtLog.TabIndex = 6
        ' 
        ' btnAsyncProcessing
        ' 
        btnAsyncProcessing.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        btnAsyncProcessing.Location = New Point(523, 125)
        btnAsyncProcessing.Name = "btnAsyncProcessing"
        btnAsyncProcessing.Size = New Size(229, 34)
        btnAsyncProcessing.TabIndex = 7
        btnAsyncProcessing.Text = "Async Processing Demo"
        btnAsyncProcessing.UseVisualStyleBackColor = True
        ' 
        ' AsyncOrderProcessingForm
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(btnAsyncProcessing)
        Controls.Add(txtLog)
        Controls.Add(lblStatus)
        Controls.Add(prgProcessing)
        Controls.Add(btnCancel)
        Controls.Add(btnStart)
        Controls.Add(nudOrderCount)
        Controls.Add(lblOrderCount)
        Name = "AsyncOrderProcessingForm"
        Text = "Asynchronous Order Processing"
        CType(nudOrderCount, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblOrderCount As Label
    Friend WithEvents nudOrderCount As NumericUpDown
    Friend WithEvents btnStart As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents prgProcessing As ProgressBar
    Friend WithEvents lblStatus As Label
    Friend WithEvents txtLog As TextBox
    Friend WithEvents btnAsyncProcessing As Button
End Class
