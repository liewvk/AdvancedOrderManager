Option Explicit On
Option Strict On
Option Infer On

Public NotInheritable Class OrderDatabaseOptions

    Public Const SectionName As String =
        "OrderDatabase"

    Public Property ConnectionString As String =
        String.Empty

End Class

