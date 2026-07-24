Option Explicit On
Option Strict On
Option Infer On

Namespace Domain

    Public NotInheritable Class StockMovement

        Public Sub New(
            quantityChange As Integer,
            resultingQuantity As Integer,
            reason As String)

            If quantityChange = 0 Then
                Throw New ArgumentException(
                    "A stock movement must change the quantity.",
                    NameOf(quantityChange))
            End If

            If resultingQuantity < 0 Then
                Throw New ArgumentOutOfRangeException(
                    NameOf(resultingQuantity),
                    "The resulting quantity cannot be negative.")
            End If

            If String.IsNullOrWhiteSpace(reason) Then
                Throw New ArgumentException(
                    "A stock movement reason is required.",
                    NameOf(reason))
            End If

            Me.QuantityChange = quantityChange
            Me.ResultingQuantity = resultingQuantity
            Me.Reason = reason.Trim()
            Me.OccurredAt = DateTimeOffset.Now
        End Sub

        Public ReadOnly Property QuantityChange As Integer

        Public ReadOnly Property ResultingQuantity As Integer

        Public ReadOnly Property Reason As String

        Public ReadOnly Property OccurredAt As DateTimeOffset

    End Class

End Namespace

