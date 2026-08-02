Option Explicit On
Option Strict On
Option Infer On

Public NotInheritable Class OrderProcessingSummary

    Public Sub New(
        requestedOrders As Integer,
        processedOrders As Integer,
        elapsedTime As TimeSpan)

        If requestedOrders <= 0 Then
            Throw New ArgumentOutOfRangeException(
                NameOf(requestedOrders))
        End If

        If processedOrders < 0 OrElse
           processedOrders > requestedOrders Then

            Throw New ArgumentOutOfRangeException(
                NameOf(processedOrders))
        End If

        Me.RequestedOrders = requestedOrders
        Me.ProcessedOrders = processedOrders
        Me.ElapsedTime = elapsedTime
    End Sub

    Public ReadOnly Property RequestedOrders As Integer

    Public ReadOnly Property ProcessedOrders As Integer

    Public ReadOnly Property ElapsedTime As TimeSpan

End Class

