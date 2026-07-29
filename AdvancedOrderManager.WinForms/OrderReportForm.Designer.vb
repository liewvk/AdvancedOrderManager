<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OrderReportForm
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
        cboReportStatus = New ComboBox()
        txtReportSearch = New TextBox()
        lblReportStatus = New Label()
        lblReportSearchtxt = New Label()
        btnRefreshReport = New Button()
        btnExportCsv = New Button()
        btnExportJson = New Button()
        btnExportHtml = New Button()
        btnPrintPreview = New Button()
        btnClearReport = New Button()
        dgvOrderReport = New DataGridView()
        lblReportRecords = New Label()
        lblReportProcessed = New Label()
        lblReportRejected = New Label()
        lblReportRevenue = New Label()
        lblReportAverage = New Label()
        btnOpenReport = New Button()
        CType(dgvOrderReport, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' cboReportStatus
        ' 
        cboReportStatus.DropDownStyle = ComboBoxStyle.DropDownList
        cboReportStatus.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        cboReportStatus.FormattingEnabled = True
        cboReportStatus.Location = New Point(187, 36)
        cboReportStatus.Name = "cboReportStatus"
        cboReportStatus.Size = New Size(203, 33)
        cboReportStatus.TabIndex = 0
        ' 
        ' txtReportSearch
        ' 
        txtReportSearch.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txtReportSearch.Location = New Point(593, 29)
        txtReportSearch.Name = "txtReportSearch"
        txtReportSearch.Size = New Size(195, 31)
        txtReportSearch.TabIndex = 1
        ' 
        ' lblReportStatus
        ' 
        lblReportStatus.AutoSize = True
        lblReportStatus.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblReportStatus.Location = New Point(783, 194)
        lblReportStatus.Name = "lblReportStatus"
        lblReportStatus.Size = New Size(60, 25)
        lblReportStatus.TabIndex = 11
        lblReportStatus.Text = "Ready"
        ' 
        ' lblReportSearchtxt
        ' 
        lblReportSearchtxt.AutoSize = True
        lblReportSearchtxt.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblReportSearchtxt.Location = New Point(450, 35)
        lblReportSearchtxt.Name = "lblReportSearchtxt"
        lblReportSearchtxt.Size = New Size(122, 25)
        lblReportSearchtxt.TabIndex = 2
        lblReportSearchtxt.Text = "Report Search"
        ' 
        ' btnRefreshReport
        ' 
        btnRefreshReport.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        btnRefreshReport.Location = New Point(31, 98)
        btnRefreshReport.Name = "btnRefreshReport"
        btnRefreshReport.Size = New Size(152, 36)
        btnRefreshReport.TabIndex = 3
        btnRefreshReport.Text = "Refresh Report"
        btnRefreshReport.UseVisualStyleBackColor = True
        ' 
        ' btnExportCsv
        ' 
        btnExportCsv.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        btnExportCsv.Location = New Point(189, 96)
        btnExportCsv.Name = "btnExportCsv"
        btnExportCsv.Size = New Size(152, 36)
        btnExportCsv.TabIndex = 4
        btnExportCsv.Text = "Export CSV"
        btnExportCsv.UseVisualStyleBackColor = True
        ' 
        ' btnExportJson
        ' 
        btnExportJson.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        btnExportJson.Location = New Point(347, 96)
        btnExportJson.Name = "btnExportJson"
        btnExportJson.Size = New Size(152, 36)
        btnExportJson.TabIndex = 5
        btnExportJson.Text = "Export JSON"
        btnExportJson.UseVisualStyleBackColor = True
        ' 
        ' btnExportHtml
        ' 
        btnExportHtml.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        btnExportHtml.Location = New Point(505, 96)
        btnExportHtml.Name = "btnExportHtml"
        btnExportHtml.Size = New Size(145, 36)
        btnExportHtml.TabIndex = 6
        btnExportHtml.Text = "Export HTML"
        btnExportHtml.UseVisualStyleBackColor = True
        ' 
        ' btnPrintPreview
        ' 
        btnPrintPreview.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        btnPrintPreview.Location = New Point(656, 96)
        btnPrintPreview.Name = "btnPrintPreview"
        btnPrintPreview.Size = New Size(152, 36)
        btnPrintPreview.TabIndex = 7
        btnPrintPreview.Text = "Print Preview"
        btnPrintPreview.UseVisualStyleBackColor = True
        ' 
        ' btnClearReport
        ' 
        btnClearReport.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        btnClearReport.Location = New Point(814, 96)
        btnClearReport.Name = "btnClearReport"
        btnClearReport.Size = New Size(137, 36)
        btnClearReport.TabIndex = 8
        btnClearReport.Text = "Clear Records"
        btnClearReport.UseVisualStyleBackColor = True
        ' 
        ' dgvOrderReport
        ' 
        dgvOrderReport.AllowUserToAddRows = False
        dgvOrderReport.AllowUserToDeleteRows = False
        dgvOrderReport.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvOrderReport.Location = New Point(34, 281)
        dgvOrderReport.Name = "dgvOrderReport"
        dgvOrderReport.ReadOnly = True
        dgvOrderReport.RowHeadersWidth = 51
        dgvOrderReport.Size = New Size(917, 305)
        dgvOrderReport.TabIndex = 9
        ' 
        ' lblReportRecords
        ' 
        lblReportRecords.AutoSize = True
        lblReportRecords.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblReportRecords.Location = New Point(34, 194)
        lblReportRecords.Name = "lblReportRecords"
        lblReportRecords.Size = New Size(94, 25)
        lblReportRecords.TabIndex = 10
        lblReportRecords.Text = "Records: 0"
        ' 
        ' lblReportProcessed
        ' 
        lblReportProcessed.AutoSize = True
        lblReportProcessed.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblReportProcessed.Location = New Point(181, 194)
        lblReportProcessed.Name = "lblReportProcessed"
        lblReportProcessed.Size = New Size(111, 25)
        lblReportProcessed.TabIndex = 10
        lblReportProcessed.Text = "Processed: 0"
        ' 
        ' lblReportRejected
        ' 
        lblReportRejected.AutoSize = True
        lblReportRejected.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblReportRejected.Location = New Point(345, 194)
        lblReportRejected.Name = "lblReportRejected"
        lblReportRejected.Size = New Size(97, 25)
        lblReportRejected.TabIndex = 10
        lblReportRejected.Text = "Rejected: 0"
        ' 
        ' lblReportRevenue
        ' 
        lblReportRevenue.AutoSize = True
        lblReportRevenue.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblReportRevenue.Location = New Point(469, 194)
        lblReportRevenue.Name = "lblReportRevenue"
        lblReportRevenue.Size = New Size(131, 25)
        lblReportRevenue.TabIndex = 10
        lblReportRevenue.Text = "Revenue: $0.00"
        ' 
        ' lblReportAverage
        ' 
        lblReportAverage.AutoSize = True
        lblReportAverage.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblReportAverage.Location = New Point(626, 194)
        lblReportAverage.Name = "lblReportAverage"
        lblReportAverage.Size = New Size(130, 25)
        lblReportAverage.TabIndex = 10
        lblReportAverage.Text = "Average: $0.00"
        ' 
        ' btnOpenReport
        ' 
        btnOpenReport.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        btnOpenReport.Location = New Point(41, 151)
        btnOpenReport.Name = "btnOpenReport"
        btnOpenReport.Size = New Size(228, 29)
        btnOpenReport.TabIndex = 12
        btnOpenReport.Text = "Open Report Center"
        btnOpenReport.UseVisualStyleBackColor = True
        ' 
        ' OrderReportForm
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(982, 653)
        Controls.Add(btnOpenReport)
        Controls.Add(lblReportProcessed)
        Controls.Add(lblReportAverage)
        Controls.Add(lblReportRevenue)
        Controls.Add(lblReportRejected)
        Controls.Add(lblReportRecords)
        Controls.Add(dgvOrderReport)
        Controls.Add(btnClearReport)
        Controls.Add(btnPrintPreview)
        Controls.Add(btnExportHtml)
        Controls.Add(btnExportJson)
        Controls.Add(btnExportCsv)
        Controls.Add(btnRefreshReport)
        Controls.Add(lblReportSearchtxt)
        Controls.Add(lblReportStatus)
        Controls.Add(txtReportSearch)
        Controls.Add(cboReportStatus)
        FormBorderStyle = FormBorderStyle.Fixed3D
        Name = "OrderReportForm"
        Text = "Order Reporting and Export Centre"
        CType(dgvOrderReport, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents cboReportStatus As ComboBox
    Friend WithEvents txtReportSearch As TextBox
    Friend WithEvents lblReportSearchtxt As Label
    Friend WithEvents btnRefreshReport As Button
    Friend WithEvents btnExportCsv As Button
    Friend WithEvents btnExportJson As Button
    Friend WithEvents btnExportHtml As Button
    Friend WithEvents btnPrintPreview As Button
    Friend WithEvents btnClearReport As Button
    Friend WithEvents dgvOrderReport As DataGridView
    Friend WithEvents lblReportRecords As Label
    Friend WithEvents lblReportProcessed As Label
    Friend WithEvents lblReportRejected As Label
    Friend WithEvents lblReportRevenue As Label
    Friend WithEvents lblReportAverage As Label
    Friend WithEvents lblReportStatus As Label
    Friend WithEvents btnOpenReport As Button
End Class
