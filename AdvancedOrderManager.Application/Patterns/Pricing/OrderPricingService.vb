Option Explicit On
Option Strict On
Option Infer On

Public NotInheritable Class OrderPricingService

    Private Const DemonstrationTaxRate As Decimal =
        0.06D

    Public Function CalculateTotal(
        submission As OrderSubmission,
        applyTax As Boolean) As Decimal

        If submission Is Nothing Then

            Throw New ArgumentNullException(
                NameOf(submission))
        End If

        Dim strategy As IOrderPricingStrategy =
            OrderPricingStrategyFactory.Create(
                submission)

        If applyTax Then

            strategy =
                New TaxPricingDecorator(
                    strategy,
                    DemonstrationTaxRate)
        End If

        Return strategy.CalculateTotal(
            submission)
    End Function

End Class

