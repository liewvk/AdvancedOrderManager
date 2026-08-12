Option Explicit On
Option Strict On
Option Infer On

Public NotInheritable Class ExternalApiAuthenticationOptions

    Public Const SectionName As String =
        "ExternalApiAuthentication"

    Public Property Mode As ExternalApiAuthenticationMode =
        ExternalApiAuthenticationMode.None

    Public Property ApiKeyHeaderName As String =
        "X-API-Key"

    Public Property ApiKey As String =
        String.Empty

    Public Property BearerToken As String =
        String.Empty

End Class

