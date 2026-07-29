Option Explicit On
Option Strict On
Option Infer On

Public NotInheritable Class OrderManagerOptions

    Public Const SectionName As String =
        "OrderManager"

    Public Property ApplicationTitle As String =
        "Advanced Order Manager"

    Public Property CurrencySymbol As String =
        "RM"

    Public Property DemonstrationTaxRate As Decimal =
        0.06D

    Public Property MinimumBulkQuantity As Integer =
        10

    Public Property BulkDiscountRate As Decimal =
        0.05D

    Public Property PrioritySurchargeRate As Decimal =
        0.1D

    Public Property EnableAuditByDefault As Boolean =
        False

End Class

