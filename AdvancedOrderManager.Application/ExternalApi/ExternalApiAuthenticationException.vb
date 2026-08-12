Option Explicit On
Option Strict On
Option Infer On

Public NotInheritable Class ExternalApiAuthenticationException
    Inherits ExternalApiException

    Public Sub New(
        message As String)

        MyBase.New(message)
    End Sub

    Public Sub New(
        message As String,
        innerException As Exception)

        MyBase.New(
            message,
            innerException)
    End Sub

End Class

