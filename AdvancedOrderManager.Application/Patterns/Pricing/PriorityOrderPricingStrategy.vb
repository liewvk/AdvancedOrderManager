Option Explicit On
Option Strict On
Option Infer On

Public NotInheritable Class PriorityOrderPricingStrategy
    Implements IOrderPricingStrategy

    Private ReadOnly _surchargeRate As Decimal

    Public Sub New(
        surchargeRate As Decimal)

        If surchargeRate < 0D OrElse
           surchargeRate > 1D Then

            Throw New ArgumentOutOfRangeException(
                NameOf(surchargeRate),
                "The surcharge rate must be between 0 and 1.")
        End If

        _surchargeRate = surchargeRate
    End Sub

    Public Function CalculateTotal(
        submission As OrderSubmission) As Decimal _
        Implements IOrderPricingStrategy.CalculateTotal

        If submission Is Nothing Then

            Throw New ArgumentNullException(
                NameOf(submission))
        End If

        Dim surcharge =
            submission.Subtotal *
            _surchargeRate

        Return Decimal.Round(
            submission.Subtotal + surcharge,
            2,
            MidpointRounding.AwayFromZero)
    End Function

End Class

