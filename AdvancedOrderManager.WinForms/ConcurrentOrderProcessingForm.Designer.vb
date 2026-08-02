<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ConcurrentOrderProcessingForm
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
        lblMaximumConcurrency = New Label()
        nudMaximumConcurrency = New NumericUpDown()
        btnStart = New Button()
        btnCancel = New Button()
        prgProcessing = New ProgressBar()
        lblStatus = New Label()
        lblActiveOperations = New Label()
        txtLog = New TextBox()
        CType(nudOrderCount, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudMaximumConcurrency, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' lblOrderCount
        ' 
        lblOrderCount.AutoSize = True
        lblOrderCount.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblOrderCount.Location = New Point(67, 48)
        lblOrderCount.Name = "lblOrderCount"
        lblOrderCount.Size = New Size(159, 25)
        lblOrderCount.TabIndex = 0
        lblOrderCount.Text = "Number of orders:"
        ' 
        ' nudOrderCount
        ' 
        nudOrderCount.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        nudOrderCount.Location = New Point(338, 57)
        nudOrderCount.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        nudOrderCount.Name = "nudOrderCount"
        nudOrderCount.Size = New Size(250, 31)
        nudOrderCount.TabIndex = 1
        nudOrderCount.Value = New Decimal(New Integer() {12, 0, 0, 0})
        ' 
        ' lblMaximumConcurrency
        ' 
        lblMaximumConcurrency.AutoSize = True
        lblMaximumConcurrency.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblMaximumConcurrency.Location = New Point(67, 119)
        lblMaximumConcurrency.Name = "lblMaximumConcurrency"
        lblMaximumConcurrency.Size = New Size(195, 25)
        lblMaximumConcurrency.TabIndex = 0
        lblMaximumConcurrency.Text = "Maximum concurrency:"
        ' 
        ' nudMaximumConcurrency
        ' 
        nudMaximumConcurrency.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        nudMaximumConcurrency.Location = New Point(339, 122)
        nudMaximumConcurrency.Maximum = New Decimal(New Integer() {20, 0, 0, 0})
        nudMaximumConcurrency.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        nudMaximumConcurrency.Name = "nudMaximumConcurrency"
        nudMaximumConcurrency.Size = New Size(255, 31)
        nudMaximumConcurrency.TabIndex = 4
        nudMaximumConcurrency.Value = New Decimal(New Integer() {4, 0, 0, 0})
        ' 
        ' btnStart
        ' 
        btnStart.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        btnStart.Location = New Point(78, 195)
        btnStart.Name = "btnStart"
        btnStart.Size = New Size(261, 39)
        btnStart.TabIndex = 5
        btnStart.Text = "Start Concurrent Processing"
        btnStart.UseVisualStyleBackColor = True
        ' 
        ' btnCancel
        ' 
        btnCancel.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        btnCancel.Location = New Point(416, 198)
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(158, 33)
        btnCancel.TabIndex = 6
        btnCancel.Text = "Cancel"
        btnCancel.UseVisualStyleBackColor = True
        ' 
        ' prgProcessing
        ' 
        prgProcessing.Location = New Point(77, 264)
        prgProcessing.Name = "prgProcessing"
        prgProcessing.Size = New Size(570, 32)
        prgProcessing.TabIndex = 7
        ' 
        ' lblStatus
        ' 
        lblStatus.AutoSize = True
        lblStatus.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblStatus.Location = New Point(78, 328)
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New Size(60, 25)
        lblStatus.TabIndex = 0
        lblStatus.Text = "Ready"
        ' 
        ' lblActiveOperations
        ' 
        lblActiveOperations.AutoSize = True
        lblActiveOperations.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblActiveOperations.Location = New Point(78, 391)
        lblActiveOperations.Name = "lblActiveOperations"
        lblActiveOperations.Size = New Size(169, 25)
        lblActiveOperations.TabIndex = 0
        lblActiveOperations.Text = "Active operations: 0"
        ' 
        ' txtLog
        ' 
        txtLog.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txtLog.Location = New Point(583, 323)
        txtLog.Multiline = True
        txtLog.Name = "txtLog"
        txtLog.ReadOnly = True
        txtLog.ScrollBars = ScrollBars.Vertical
        txtLog.Size = New Size(139, 30)
        txtLog.TabIndex = 8
        ' 
        ' ConcurrentOrderProcessingForm
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(txtLog)
        Controls.Add(prgProcessing)
        Controls.Add(btnCancel)
        Controls.Add(btnStart)
        Controls.Add(nudMaximumConcurrency)
        Controls.Add(nudOrderCount)
        Controls.Add(lblMaximumConcurrency)
        Controls.Add(lblActiveOperations)
        Controls.Add(lblStatus)
        Controls.Add(lblOrderCount)
        Name = "ConcurrentOrderProcessingForm"
        Text = "Concurrent Order Processing"
        CType(nudOrderCount, ComponentModel.ISupportInitialize).EndInit()
        CType(nudMaximumConcurrency, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblOrderCount As Label
    Friend WithEvents nudOrderCount As NumericUpDown
    Friend WithEvents lblMaximumConcurrency As Label
    Friend WithEvents nudMaximumConcurrency As NumericUpDown
    Friend WithEvents btnStart As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents prgProcessing As ProgressBar
    Friend WithEvents lblStatus As Label
    Friend WithEvents lblActiveOperations As Label
    Friend WithEvents txtLog As TextBox
End Class
