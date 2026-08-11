Option Explicit On
Option Strict On
Option Infer On

Public NotInheritable Class ExternalApiResilienceOptions

    Public Const SectionName As String =
        "ExternalApiResilience"

    Public Property MaxRetryAttempts As Integer = 3

    Public Property RetryDelaySeconds As Double = 1.0

    Public Property AttemptTimeoutSeconds As Double = 5.0

    Public Property TotalTimeoutSeconds As Double = 20.0

End Class
