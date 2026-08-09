Option Explicit On
Option Strict On
Option Infer On

Public NotInheritable Class ProcessedOrderEntity

    Public Property Id As Integer

    Public Property OrderId As String =
        String.Empty

    Public Property CustomerName As String =
        String.Empty

    Public Property Quantity As Integer

    Public Property UnitPrice As Decimal

    Public Property IsPriority As Boolean

    Public Property TotalAmount As Decimal

    Public Property Status As String =
        String.Empty

    Public Property ProcessedAt As DateTimeOffset

End Class

