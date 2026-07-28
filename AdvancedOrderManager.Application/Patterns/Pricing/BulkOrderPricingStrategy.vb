Option Explicit On
Option Strict On
Option Infer On

Public NotInheritable Class BulkOrderPricingStrategy
    Implements IOrderPricingStrategy

    Private Const MinimumBulkQuantity As Integer =
        10

    Private Const DiscountRate As Decimal =
        0.05D

    Public Function CalculateTotal(
        submission As OrderSubmission) As Decimal _
        Implements IOrderPricingStrategy.CalculateTotal

        If submission Is Nothing Then

            Throw New ArgumentNullException(
                NameOf(submission))
        End If

        Dim discount As Decimal =
            0D

        If submission.Quantity >=
           MinimumBulkQuantity Then

            discount =
                submission.Subtotal *
                DiscountRate
        End If

        Return Decimal.Round(
            submission.Subtotal - discount,
            2,
            MidpointRounding.AwayFromZero)
    End Function

End Class

