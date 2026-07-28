Option Explicit On
Option Strict On
Option Infer On

Public Interface IOrderPricingStrategy

    Function CalculateTotal(
        submission As OrderSubmission) As Decimal

End Interface

