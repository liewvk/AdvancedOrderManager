Option Explicit On
Option Strict On
Option Infer On

Imports System.Text.Json.Serialization

Public NotInheritable Class CreateExternalPostRequest

    Public Sub New(
        userId As Integer,
        title As String,
        body As String)

        If userId <= 0 Then
            Throw New ArgumentOutOfRangeException(
                NameOf(userId))
        End If

        If String.IsNullOrWhiteSpace(title) Then
            Throw New ArgumentException(
                "A title is required.",
                NameOf(title))
        End If

        If String.IsNullOrWhiteSpace(body) Then
            Throw New ArgumentException(
                "Post content is required.",
                NameOf(body))
        End If

        Me.UserId = userId
        Me.Title = title.Trim()
        Me.Body = body.Trim()
    End Sub

    <JsonPropertyName("userId")>
    Public ReadOnly Property UserId As Integer

    <JsonPropertyName("title")>
    Public ReadOnly Property Title As String

    <JsonPropertyName("body")>
    Public ReadOnly Property Body As String

End Class

