Option Explicit On
Option Strict On
Option Infer On

Imports System

Friend Module Program

    <STAThread>
    Public Sub Main()

        Global.System.Windows.Forms.Application.EnableVisualStyles()
        Global.System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(False)

        Dim customerForm As CustomerForm =
    Bootstrapper.CreateCustomerForm()

        Global.System.Windows.Forms.Application.Run(customerForm)

    End Sub

End Module

