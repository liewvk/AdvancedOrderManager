Option Explicit On
Option Strict On
Option Infer On

Imports System

Friend Module Program

    <STAThread>
    Public Sub Main()

        Global.System.Windows.Forms.Application.EnableVisualStyles()
        Global.System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(False)

        Dim productForm As ProductForm =
            Bootstrapper.CreateProductForm()

        Global.System.Windows.Forms.Application.Run(productForm)

    End Sub

End Module

