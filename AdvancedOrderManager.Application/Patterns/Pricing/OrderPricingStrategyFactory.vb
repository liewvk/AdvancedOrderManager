Option Explicit On
Option Strict On
Option Infer On

Public NotInheritable Class OrderPricingStrategyFactory

    Private Const MinimumBulkQuantity As Integer =
        10

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

            Return New PriorityOrderPricingStrategy()
        End If

        If submission.Quantity >=
           MinimumBulkQuantity Then

            Return New BulkOrderPricingStrategy()
        End If

        Return New StandardOrderPricingStrategy()
    End Function

End Class

