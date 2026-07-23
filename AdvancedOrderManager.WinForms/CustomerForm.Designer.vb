<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CustomerForm
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
        lblFirstName = New Label()
        txtFirstName = New TextBox()
        lblLastName = New Label()
        txtLastName = New TextBox()
        lblEmail = New Label()
        txtEmail = New TextBox()
        lblAddressLine = New Label()
        txtAddressLine = New TextBox()
        lblCity = New Label()
        txtCity = New TextBox()
        lblPostalCode = New Label()
        txtPostalCode = New TextBox()
        lblCountry = New Label()
        txtCountry = New TextBox()
        btnRegister = New Button()
        btnClear = New Button()
        lblSearch = New Label()
        txtSearch = New TextBox()
        dgvCustomers = New DataGridView()
        lblCustomerCount = New Label()
        lblStatus = New Label()
        btnCustomers = New Button()
        CType(dgvCustomers, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' lblFirstName
        ' 
        lblFirstName.AutoSize = True
        lblFirstName.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblFirstName.Location = New Point(62, 27)
        lblFirstName.Name = "lblFirstName"
        lblFirstName.Size = New Size(97, 25)
        lblFirstName.TabIndex = 0
        lblFirstName.Text = "First Name"
        ' 
        ' txtFirstName
        ' 
        txtFirstName.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txtFirstName.Location = New Point(206, 24)
        txtFirstName.Name = "txtFirstName"
        txtFirstName.Size = New Size(142, 31)
        txtFirstName.TabIndex = 1
        ' 
        ' lblLastName
        ' 
        lblLastName.AutoSize = True
        lblLastName.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblLastName.Location = New Point(388, 27)
        lblLastName.Name = "lblLastName"
        lblLastName.Size = New Size(95, 25)
        lblLastName.TabIndex = 0
        lblLastName.Text = "Last Name"
        ' 
        ' txtLastName
        ' 
        txtLastName.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txtLastName.Location = New Point(507, 27)
        txtLastName.Name = "txtLastName"
        txtLastName.Size = New Size(142, 31)
        txtLastName.TabIndex = 1
        ' 
        ' lblEmail
        ' 
        lblEmail.AutoSize = True
        lblEmail.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblEmail.Location = New Point(62, 89)
        lblEmail.Name = "lblEmail"
        lblEmail.Size = New Size(124, 25)
        lblEmail.TabIndex = 0
        lblEmail.Text = "Email Address"
        ' 
        ' txtEmail
        ' 
        txtEmail.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txtEmail.Location = New Point(206, 83)
        txtEmail.Name = "txtEmail"
        txtEmail.Size = New Size(226, 31)
        txtEmail.TabIndex = 1
        ' 
        ' lblAddressLine
        ' 
        lblAddressLine.AutoSize = True
        lblAddressLine.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblAddressLine.Location = New Point(62, 150)
        lblAddressLine.Name = "lblAddressLine"
        lblAddressLine.Size = New Size(77, 25)
        lblAddressLine.TabIndex = 0
        lblAddressLine.Text = "Address"
        ' 
        ' txtAddressLine
        ' 
        txtAddressLine.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txtAddressLine.Location = New Point(206, 144)
        txtAddressLine.Name = "txtAddressLine"
        txtAddressLine.Size = New Size(420, 31)
        txtAddressLine.TabIndex = 1
        ' 
        ' lblCity
        ' 
        lblCity.AutoSize = True
        lblCity.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblCity.Location = New Point(62, 209)
        lblCity.Name = "lblCity"
        lblCity.Size = New Size(42, 25)
        lblCity.TabIndex = 0
        lblCity.Text = "City"
        ' 
        ' txtCity
        ' 
        txtCity.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txtCity.Location = New Point(206, 203)
        txtCity.Name = "txtCity"
        txtCity.Size = New Size(420, 31)
        txtCity.TabIndex = 1
        ' 
        ' lblPostalCode
        ' 
        lblPostalCode.AutoSize = True
        lblPostalCode.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblPostalCode.Location = New Point(62, 262)
        lblPostalCode.Name = "lblPostalCode"
        lblPostalCode.Size = New Size(106, 25)
        lblPostalCode.TabIndex = 0
        lblPostalCode.Text = "Postal Code"
        ' 
        ' txtPostalCode
        ' 
        txtPostalCode.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txtPostalCode.Location = New Point(206, 256)
        txtPostalCode.Name = "txtPostalCode"
        txtPostalCode.Size = New Size(142, 31)
        txtPostalCode.TabIndex = 1
        ' 
        ' lblCountry
        ' 
        lblCountry.AutoSize = True
        lblCountry.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblCountry.Location = New Point(62, 314)
        lblCountry.Name = "lblCountry"
        lblCountry.Size = New Size(75, 25)
        lblCountry.TabIndex = 0
        lblCountry.Text = "Country"
        lblCountry.UseWaitCursor = True
        ' 
        ' txtCountry
        ' 
        txtCountry.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txtCountry.Location = New Point(206, 308)
        txtCountry.Name = "txtCountry"
        txtCountry.Size = New Size(142, 31)
        txtCountry.TabIndex = 1
        txtCountry.Text = "USA"
        ' 
        ' btnRegister
        ' 
        btnRegister.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        btnRegister.Location = New Point(753, 32)
        btnRegister.Name = "btnRegister"
        btnRegister.Size = New Size(209, 34)
        btnRegister.TabIndex = 2
        btnRegister.Text = "Register Customer"
        btnRegister.UseVisualStyleBackColor = True
        ' 
        ' btnClear
        ' 
        btnClear.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        btnClear.Location = New Point(753, 79)
        btnClear.Name = "btnClear"
        btnClear.Size = New Size(209, 35)
        btnClear.TabIndex = 3
        btnClear.Text = "Clear"
        btnClear.UseVisualStyleBackColor = True
        ' 
        ' lblSearch
        ' 
        lblSearch.AutoSize = True
        lblSearch.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblSearch.Location = New Point(753, 132)
        lblSearch.Name = "lblSearch"
        lblSearch.Size = New Size(64, 25)
        lblSearch.TabIndex = 4
        lblSearch.Text = "Search"
        ' 
        ' txtSearch
        ' 
        txtSearch.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txtSearch.Location = New Point(753, 170)
        txtSearch.Name = "txtSearch"
        txtSearch.Size = New Size(226, 31)
        txtSearch.TabIndex = 1
        ' 
        ' dgvCustomers
        ' 
        dgvCustomers.AllowUserToAddRows = False
        dgvCustomers.AllowUserToDeleteRows = False
        dgvCustomers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvCustomers.Location = New Point(62, 376)
        dgvCustomers.MultiSelect = False
        dgvCustomers.Name = "dgvCustomers"
        dgvCustomers.ReadOnly = True
        dgvCustomers.RowHeadersWidth = 51
        dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvCustomers.Size = New Size(831, 239)
        dgvCustomers.TabIndex = 5
        ' 
        ' lblCustomerCount
        ' 
        lblCustomerCount.AutoSize = True
        lblCustomerCount.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblCustomerCount.Location = New Point(753, 230)
        lblCustomerCount.Name = "lblCustomerCount"
        lblCustomerCount.Size = New Size(116, 25)
        lblCustomerCount.TabIndex = 4
        lblCustomerCount.Text = "Customers: 0"
        ' 
        ' lblStatus
        ' 
        lblStatus.AutoSize = True
        lblStatus.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblStatus.Location = New Point(753, 271)
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New Size(60, 25)
        lblStatus.TabIndex = 4
        lblStatus.Text = "Ready"
        ' 
        ' btnCustomers
        ' 
        btnCustomers.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        btnCustomers.Location = New Point(758, 310)
        btnCustomers.Name = "btnCustomers"
        btnCustomers.Size = New Size(180, 35)
        btnCustomers.TabIndex = 6
        btnCustomers.Text = "Customer Registry"
        btnCustomers.UseVisualStyleBackColor = True
        ' 
        ' CustomerForm
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1032, 653)
        Controls.Add(btnCustomers)
        Controls.Add(dgvCustomers)
        Controls.Add(lblStatus)
        Controls.Add(lblCustomerCount)
        Controls.Add(lblSearch)
        Controls.Add(btnClear)
        Controls.Add(btnRegister)
        Controls.Add(txtLastName)
        Controls.Add(txtCountry)
        Controls.Add(txtPostalCode)
        Controls.Add(txtCity)
        Controls.Add(txtAddressLine)
        Controls.Add(txtSearch)
        Controls.Add(txtEmail)
        Controls.Add(txtFirstName)
        Controls.Add(lblLastName)
        Controls.Add(lblCountry)
        Controls.Add(lblPostalCode)
        Controls.Add(lblCity)
        Controls.Add(lblAddressLine)
        Controls.Add(lblEmail)
        Controls.Add(lblFirstName)
        MinimumSize = New Size(900, 600)
        Name = "CustomerForm"
        Text = "Customer Profile Registry"
        CType(dgvCustomers, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblFirstName As Label
    Friend WithEvents txtFirstName As TextBox
    Friend WithEvents lblLastName As Label
    Friend WithEvents txtLastName As TextBox
    Friend WithEvents lblEmail As Label
    Friend WithEvents txtEmail As TextBox
    Friend WithEvents lblAddressLine As Label
    Friend WithEvents txtAddressLine As TextBox
    Friend WithEvents lblCity As Label
    Friend WithEvents txtCity As TextBox
    Friend WithEvents lblPostalCode As Label
    Friend WithEvents txtPostalCode As TextBox
    Friend WithEvents lblCountry As Label
    Friend WithEvents txtCountry As TextBox
    Friend WithEvents btnRegister As Button
    Friend WithEvents btnClear As Button
    Friend WithEvents lblSearch As Label
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents dgvCustomers As DataGridView
    Friend WithEvents lblCustomerCount As Label
    Friend WithEvents lblStatus As Label
    Friend WithEvents btnCustomers As Button
End Class
