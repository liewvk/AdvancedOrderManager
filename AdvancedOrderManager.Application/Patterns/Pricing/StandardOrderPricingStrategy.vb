Option Explicit On
Option Strict On
Option Infer On

Public NotInheritable Class StandardOrderPricingStrategy
    Implements IOrderPricingStrategy

    Public Function CalculateTotal(
        submission As OrderSubmission) As Decimal _
        Implements IOrderPricingStrategy.CalculateTotal

        If submission Is Nothing Then

            Throw New ArgumentNullException(
                NameOf(submission))
        End If

        Return Decimal.Round(
            submission.Subtotal,
            2,
            MidpointRounding.AwayFromZero)
    End Function

End Class

