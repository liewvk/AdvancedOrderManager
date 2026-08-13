Option Explicit On
Option Strict On
Option Infer On

Public NotInheritable Class InputValidationError

    Public Sub New(
        fieldName As String,
        message As String)

        If String.IsNullOrWhiteSpace(
            fieldName) Then

            Throw New ArgumentException(
                "A field name is required.",
                NameOf(fieldName))

        End If

        If String.IsNullOrWhiteSpace(
            message) Then

            Throw New ArgumentException(
                "A validation message is required.",
                NameOf(message))

        End If

        Me.FieldName =
            fieldName

        Me.Message =
            message
    End Sub

    Public ReadOnly Property FieldName As String

    Public ReadOnly Property Message As String

End Class

