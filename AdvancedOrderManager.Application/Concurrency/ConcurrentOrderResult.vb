Option Explicit On
Option Strict On
Option Infer On

Public NotInheritable Class ConcurrentOrderResult

    Public Sub New(
        orderNumber As Integer,
        wasSuccessful As Boolean,
        duration As TimeSpan,
        message As String)

        If orderNumber <= 0 Then
            Throw New ArgumentOutOfRangeException(
                NameOf(orderNumber))
        End If

        If duration < TimeSpan.Zero Then
            Throw New ArgumentOutOfRangeException(
                NameOf(duration))
        End If

        If String.IsNullOrWhiteSpace(message) Then
            Throw New ArgumentException(
                "A result message is required.",
                NameOf(message))
        End If

        Me.OrderNumber = orderNumber
        Me.WasSuccessful = wasSuccessful
        Me.Duration = duration
        Me.Message = message
    End Sub

    Public ReadOnly Property OrderNumber As Integer

    Public ReadOnly Property WasSuccessful As Boolean

    Public ReadOnly Property Duration As TimeSpan

    Public ReadOnly Property Message As String

End Class

