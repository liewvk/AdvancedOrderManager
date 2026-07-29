Option Explicit On
Option Strict On
Option Infer On

Public NotInheritable Class OrderPricingService

    Private ReadOnly _options As OrderManagerOptions

    Public Sub New(options As OrderManagerOptions)

        If options Is Nothing Then
            Throw New ArgumentNullException(
                NameOf(options))
        End If

        _options = options
    End Sub

    Public Function CalculateTotal(
        submission As OrderSubmission,
        applyTax As Boolean) As Decimal

        If submission Is Nothing Then
            Throw New ArgumentNullException(
                NameOf(submission))
        End If

        Dim strategy As IOrderPricingStrategy =
            CreateStrategy(submission)

        If applyTax Then
            strategy =
                New TaxPricingDecorator(
                    strategy,
                    _options.DemonstrationTaxRate)
        End If

        Return strategy.CalculateTotal(
            submission)
    End Function

    Private Function CreateStrategy(
        submission As OrderSubmission) As IOrderPricingStrategy

        If submission.IsPriority Then
            Return New PriorityOrderPricingStrategy(
                _options.PrioritySurchargeRate)
        End If

        If submission.Quantity >=
           _options.MinimumBulkQuantity Then

            Return New BulkOrderPricingStrategy(
                _options.MinimumBulkQuantity,
                _options.BulkDiscountRate)
        End If

        Return New StandardOrderPricingStrategy()
    End Function

End Class