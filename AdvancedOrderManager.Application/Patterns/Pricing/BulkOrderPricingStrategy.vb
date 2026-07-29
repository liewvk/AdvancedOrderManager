Option Explicit On
Option Strict On
Option Infer On

Public NotInheritable Class BulkOrderPricingStrategy
    Implements IOrderPricingStrategy

    Private ReadOnly _minimumQuantity As Integer
    Private ReadOnly _discountRate As Decimal

    Public Sub New(
        minimumQuantity As Integer,
        discountRate As Decimal)

        If minimumQuantity <= 0 Then

            Throw New ArgumentOutOfRangeException(
                NameOf(minimumQuantity))
        End If

        If discountRate < 0D OrElse
           discountRate > 1D Then

            Throw New ArgumentOutOfRangeException(
                NameOf(discountRate),
                "The discount rate must be between 0 and 1.")
        End If

        _minimumQuantity = minimumQuantity
        _discountRate = discountRate
    End Sub

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
           _minimumQuantity Then

            discount =
                submission.Subtotal *
                _discountRate
        End If

        Return Decimal.Round(
            submission.Subtotal - discount,
            2,
            MidpointRounding.AwayFromZero)
    End Function


End Class

