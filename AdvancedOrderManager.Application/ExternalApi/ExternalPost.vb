Option Explicit On
Option Strict On
Option Infer On

Imports System.Text.Json.Serialization

Public NotInheritable Class ExternalPost

    <JsonPropertyName("userId")>
    Public Property UserId As Integer

    <JsonPropertyName("id")>
    Public Property Id As Integer

    <JsonPropertyName("title")>
    Public Property Title As String =
        String.Empty

    <JsonPropertyName("body")>
    Public Property Body As String =
        String.Empty

End Class

