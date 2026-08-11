Option Explicit On
Option Strict On
Option Infer On

Public NotInheritable Class ExternalApiTimeoutException
    Inherits ExternalApiException

    Public Sub New(
        message As String,
        innerException As Exception)

        MyBase.New(
            message,
            innerException)
    End Sub

End Class

