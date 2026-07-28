Option Explicit On
Option Strict On
Option Infer On

Public NotInheritable Class PriorityOrderPricingStrategy
    Implements IOrderPricingStrategy

    Private Const SurchargeRate As Decimal =
        0.1D

    Public Function CalculateTotal(
        submission As OrderSubmission) As Decimal _
        Implements IOrderPricingStrategy.CalculateTotal

        If submission Is Nothing Then

            Throw New ArgumentNullException(
                NameOf(submission))
        End If

        Dim surcharge =
            submission.Subtotal *
            SurchargeRate

        Return Decimal.Round(
            submission.Subtotal + surcharge,
            2,
            MidpointRounding.AwayFromZero)
    End Function

End Class

