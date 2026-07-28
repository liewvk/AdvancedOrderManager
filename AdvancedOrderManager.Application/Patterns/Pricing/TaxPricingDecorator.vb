Option Explicit On
Option Strict On
Option Infer On

Public NotInheritable Class TaxPricingDecorator
    Implements IOrderPricingStrategy

    Private ReadOnly _innerStrategy As IOrderPricingStrategy

    Private ReadOnly _taxRate As Decimal

    Public Sub New(
        innerStrategy As IOrderPricingStrategy,
        taxRate As Decimal)

        If innerStrategy Is Nothing Then

            Throw New ArgumentNullException(
                NameOf(innerStrategy))
        End If

        If taxRate < 0D OrElse
           taxRate > 1D Then

            Throw New ArgumentOutOfRangeException(
                NameOf(taxRate),
                "The tax rate must be between 0 and 1.")
        End If

        _innerStrategy = innerStrategy
        _taxRate = taxRate
    End Sub

    Public Function CalculateTotal(
        submission As OrderSubmission) As Decimal _
        Implements IOrderPricingStrategy.CalculateTotal

        Dim baseTotal =
            _innerStrategy.CalculateTotal(
                submission)

        Dim taxAmount =
            baseTotal *
            _taxRate

        Return Decimal.Round(
            baseTotal + taxAmount,
            2,
            MidpointRounding.AwayFromZero)
    End Function

End Class

