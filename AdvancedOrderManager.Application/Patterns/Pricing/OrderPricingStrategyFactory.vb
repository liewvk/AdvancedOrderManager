Option Explicit On
Option Strict On
Option Infer On

Public NotInheritable Class OrderPricingStrategyFactory

    Private Const MinimumBulkQuantity As Integer =
        10

    Private Const BulkDiscountRate As Decimal =
        0.05D

    Private Const PrioritySurchargeRate As Decimal =
        0.05D

    Private Sub New()
    End Sub

    Public Shared Function Create(
        submission As OrderSubmission) _
        As IOrderPricingStrategy

        If submission Is Nothing Then

            Throw New ArgumentNullException(
                NameOf(submission))
        End If

        If submission.IsPriority Then

            Return New PriorityOrderPricingStrategy(
                PrioritySurchargeRate)
        End If

        If submission.Quantity >=
           MinimumBulkQuantity Then

            Return New BulkOrderPricingStrategy(
                MinimumBulkQuantity,
                BulkDiscountRate)
        End If

        Return New StandardOrderPricingStrategy()
    End Function

End Class