Option Explicit On
Option Strict On
Option Infer On

Public NotInheritable Class ExternalApiOptions

    Public Const SectionName As String =
        "ExternalApi"

    Public Property BaseAddress As String =
        String.Empty

    Public Property TimeoutSeconds As Integer = 15

End Class

