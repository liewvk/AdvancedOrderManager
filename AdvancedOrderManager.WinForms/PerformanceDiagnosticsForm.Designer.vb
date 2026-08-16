<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PerformanceDiagnosticsForm
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
        lblManagedMemory = New Label()
        lblWorkingSet = New Label()
        lblGeneration0 = New Label()
        lblGeneration1 = New Label()
        lblGeneration2 = New Label()
        lblUptime = New Label()
        btnRefresh = New Button()
        btnRunAllocationDemo = New Button()
        SuspendLayout()
        ' 
        ' lblManagedMemory
        ' 
        lblManagedMemory.AutoSize = True
        lblManagedMemory.Location = New Point(83, 60)
        lblManagedMemory.Name = "lblManagedMemory"
        lblManagedMemory.Size = New Size(122, 20)
        lblManagedMemory.TabIndex = 0
        lblManagedMemory.Text = "Manage memory"
        ' 
        ' lblWorkingSet
        ' 
        lblWorkingSet.AutoSize = True
        lblWorkingSet.Location = New Point(83, 100)
        lblWorkingSet.Name = "lblWorkingSet"
        lblWorkingSet.Size = New Size(89, 20)
        lblWorkingSet.TabIndex = 0
        lblWorkingSet.Text = "Working Set"
        ' 
        ' lblGeneration0
        ' 
        lblGeneration0.AutoSize = True
        lblGeneration0.Location = New Point(83, 138)
        lblGeneration0.Name = "lblGeneration0"
        lblGeneration0.Size = New Size(94, 20)
        lblGeneration0.TabIndex = 0
        lblGeneration0.Text = "Generation 0"
        ' 
        ' lblGeneration1
        ' 
        lblGeneration1.AutoSize = True
        lblGeneration1.Location = New Point(83, 174)
        lblGeneration1.Name = "lblGeneration1"
        lblGeneration1.Size = New Size(94, 20)
        lblGeneration1.TabIndex = 0
        lblGeneration1.Text = "Generation 1"
        ' 
        ' lblGeneration2
        ' 
        lblGeneration2.AutoSize = True
        lblGeneration2.Location = New Point(83, 207)
        lblGeneration2.Name = "lblGeneration2"
        lblGeneration2.Size = New Size(94, 20)
        lblGeneration2.TabIndex = 0
        lblGeneration2.Text = "Generation 2"
        ' 
        ' lblUptime
        ' 
        lblUptime.AutoSize = True
        lblUptime.Location = New Point(83, 240)
        lblUptime.Name = "lblUptime"
        lblUptime.Size = New Size(62, 20)
        lblUptime.TabIndex = 0
        lblUptime.Text = "Up time"
        ' 
        ' btnRefresh
        ' 
        btnRefresh.Location = New Point(344, 61)
        btnRefresh.Name = "btnRefresh"
        btnRefresh.Size = New Size(176, 34)
        btnRefresh.TabIndex = 1
        btnRefresh.Text = "Refresh Snapshot"
        btnRefresh.UseVisualStyleBackColor = True
        ' 
        ' btnRunAllocationDemo
        ' 
        btnRunAllocationDemo.Location = New Point(344, 128)
        btnRunAllocationDemo.Name = "btnRunAllocationDemo"
        btnRunAllocationDemo.Size = New Size(176, 30)
        btnRunAllocationDemo.TabIndex = 2
        btnRunAllocationDemo.Text = "Run Allocation Demo"
        btnRunAllocationDemo.UseVisualStyleBackColor = True
        ' 
        ' PerformanceDiagnosticsForm
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(btnRunAllocationDemo)
        Controls.Add(btnRefresh)
        Controls.Add(lblUptime)
        Controls.Add(lblGeneration2)
        Controls.Add(lblGeneration1)
        Controls.Add(lblGeneration0)
        Controls.Add(lblWorkingSet)
        Controls.Add(lblManagedMemory)
        Name = "PerformanceDiagnosticsForm"
        Text = "Performance Diagnostics"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblManagedMemory As Label
    Friend WithEvents lblWorkingSet As Label
    Friend WithEvents lblGeneration0 As Label
    Friend WithEvents lblGeneration1 As Label
    Friend WithEvents lblGeneration2 As Label
    Friend WithEvents lblUptime As Label
    Friend WithEvents btnRefresh As Button
    Friend WithEvents btnRunAllocationDemo As Button
End Class
