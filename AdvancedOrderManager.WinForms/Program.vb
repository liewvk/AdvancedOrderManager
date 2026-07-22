Option Explicit On
Option Strict On
Option Infer On

Imports System

Friend Module Program

    <STAThread>
    Public Sub Main()

        Global.System.Windows.Forms.Application.EnableVisualStyles()
        Global.System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(False)

        Dim mainForm As MainForm = Bootstrapper.CreateMainForm()

        Global.System.Windows.Forms.Application.Run(mainForm)

    End Sub

End Module

