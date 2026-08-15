Option Explicit On
Option Strict On
Option Infer On

Public NotInheritable Class CreateExternalPostRequest

    Public Sub New(
        userId As Integer,
        title As String,
        body As String)

        Me.UserId =
            userId

        Me.Title =
            If(
                title,
                String.Empty)

        Me.Body =
            If(
                body,
                String.Empty)

    End Sub

    Public ReadOnly Property UserId As Integer

    Public ReadOnly Property Title As String

    Public ReadOnly Property Body As String

End Class