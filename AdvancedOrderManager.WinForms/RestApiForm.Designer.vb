<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RestApiForm
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
        components = New ComponentModel.Container()
        lblUserId = New Label()
        nudUserId = New NumericUpDown()
        btnLoadPosts = New Button()
        lblPostId = New Label()
        nudPostId = New NumericUpDown()
        btnFindPost = New Button()
        lblTitle = New Label()
        txtTitle = New TextBox()
        lblBody = New Label()
        txtBody = New TextBox()
        btnCreatePost = New Button()
        btnCancel = New Button()
        dgvPosts = New DataGridView()
        lblStatus = New Label()
        errorProviderInput = New ErrorProvider(components)
        CType(nudUserId, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudPostId, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvPosts, ComponentModel.ISupportInitialize).BeginInit()
        CType(errorProviderInput, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' lblUserId
        ' 
        lblUserId.AutoSize = True
        lblUserId.Location = New Point(44, 53)
        lblUserId.Name = "lblUserId"
        lblUserId.Size = New Size(60, 20)
        lblUserId.TabIndex = 0
        lblUserId.Text = "User ID:"
        ' 
        ' nudUserId
        ' 
        nudUserId.Location = New Point(136, 46)
        nudUserId.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        nudUserId.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        nudUserId.Name = "nudUserId"
        nudUserId.Size = New Size(214, 27)
        nudUserId.TabIndex = 1
        nudUserId.Value = New Decimal(New Integer() {1, 0, 0, 0})
        ' 
        ' btnLoadPosts
        ' 
        btnLoadPosts.Location = New Point(735, 42)
        btnLoadPosts.Name = "btnLoadPosts"
        btnLoadPosts.Size = New Size(160, 31)
        btnLoadPosts.TabIndex = 2
        btnLoadPosts.Text = "Load User Posts"
        btnLoadPosts.UseVisualStyleBackColor = True
        ' 
        ' lblPostId
        ' 
        lblPostId.AutoSize = True
        lblPostId.Location = New Point(44, 101)
        lblPostId.Name = "lblPostId"
        lblPostId.Size = New Size(58, 20)
        lblPostId.TabIndex = 0
        lblPostId.Text = "Post ID:"
        ' 
        ' nudPostId
        ' 
        nudPostId.Location = New Point(136, 94)
        nudPostId.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        nudPostId.Name = "nudPostId"
        nudPostId.Size = New Size(208, 27)
        nudPostId.TabIndex = 3
        nudPostId.Value = New Decimal(New Integer() {1, 0, 0, 0})
        ' 
        ' btnFindPost
        ' 
        btnFindPost.Location = New Point(735, 97)
        btnFindPost.Name = "btnFindPost"
        btnFindPost.Size = New Size(162, 28)
        btnFindPost.TabIndex = 4
        btnFindPost.Text = "Find Post"
        btnFindPost.UseVisualStyleBackColor = True
        ' 
        ' lblTitle
        ' 
        lblTitle.AutoSize = True
        lblTitle.Location = New Point(46, 155)
        lblTitle.Name = "lblTitle"
        lblTitle.Size = New Size(41, 20)
        lblTitle.TabIndex = 0
        lblTitle.Text = "Title:"
        ' 
        ' txtTitle
        ' 
        txtTitle.Location = New Point(136, 152)
        txtTitle.Name = "txtTitle"
        txtTitle.Size = New Size(418, 27)
        txtTitle.TabIndex = 5
        ' 
        ' lblBody
        ' 
        lblBody.AutoSize = True
        lblBody.Location = New Point(46, 194)
        lblBody.Name = "lblBody"
        lblBody.Size = New Size(46, 20)
        lblBody.TabIndex = 0
        lblBody.Text = "Body:"
        ' 
        ' txtBody
        ' 
        txtBody.Location = New Point(136, 194)
        txtBody.Multiline = True
        txtBody.Name = "txtBody"
        txtBody.Size = New Size(549, 27)
        txtBody.TabIndex = 5
        ' 
        ' btnCreatePost
        ' 
        btnCreatePost.Location = New Point(747, 144)
        btnCreatePost.Name = "btnCreatePost"
        btnCreatePost.Size = New Size(160, 31)
        btnCreatePost.TabIndex = 6
        btnCreatePost.Text = "Create Demo Post"
        btnCreatePost.UseVisualStyleBackColor = True
        ' 
        ' btnCancel
        ' 
        btnCancel.Enabled = False
        btnCancel.Location = New Point(747, 194)
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(140, 33)
        btnCancel.TabIndex = 7
        btnCancel.Text = "Cancel"
        btnCancel.UseVisualStyleBackColor = True
        ' 
        ' dgvPosts
        ' 
        dgvPosts.AllowUserToAddRows = False
        dgvPosts.AllowUserToDeleteRows = False
        dgvPosts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvPosts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvPosts.Location = New Point(46, 283)
        dgvPosts.MultiSelect = False
        dgvPosts.Name = "dgvPosts"
        dgvPosts.ReadOnly = True
        dgvPosts.RowHeadersWidth = 51
        dgvPosts.Size = New Size(900, 155)
        dgvPosts.TabIndex = 8
        ' 
        ' lblStatus
        ' 
        lblStatus.AutoSize = True
        lblStatus.Location = New Point(46, 241)
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New Size(50, 20)
        lblStatus.TabIndex = 0
        lblStatus.Text = "Ready"
        ' 
        ' errorProviderInput
        ' 
        errorProviderInput.BlinkStyle = ErrorBlinkStyle.NeverBlink
        errorProviderInput.ContainerControl = Me
        ' 
        ' RestApiForm
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(980, 468)
        Controls.Add(dgvPosts)
        Controls.Add(btnCancel)
        Controls.Add(btnCreatePost)
        Controls.Add(txtBody)
        Controls.Add(txtTitle)
        Controls.Add(btnFindPost)
        Controls.Add(nudPostId)
        Controls.Add(btnLoadPosts)
        Controls.Add(nudUserId)
        Controls.Add(lblStatus)
        Controls.Add(lblBody)
        Controls.Add(lblTitle)
        Controls.Add(lblPostId)
        Controls.Add(lblUserId)
        Name = "RestApiForm"
        Text = "REST API and JSON Demo"
        CType(nudUserId, ComponentModel.ISupportInitialize).EndInit()
        CType(nudPostId, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvPosts, ComponentModel.ISupportInitialize).EndInit()
        CType(errorProviderInput, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblUserId As Label
    Friend WithEvents nudUserId As NumericUpDown
    Friend WithEvents btnLoadPosts As Button
    Friend WithEvents lblPostId As Label
    Friend WithEvents nudPostId As NumericUpDown
    Friend WithEvents btnFindPost As Button
    Friend WithEvents lblTitle As Label
    Friend WithEvents txtTitle As TextBox
    Friend WithEvents lblBody As Label
    Friend WithEvents txtBody As TextBox
    Friend WithEvents btnCreatePost As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents dgvPosts As DataGridView
    Friend WithEvents lblStatus As Label
    Friend WithEvents errorProviderInput As ErrorProvider
End Class
