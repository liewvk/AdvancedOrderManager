Option Explicit On
Option Strict On
Option Infer On

Public NotInheritable Class OrderProcessingProgress

    Public Sub New(
        processedOrders As Integer,
        totalOrders As Integer,
        message As String)

        If totalOrders <= 0 Then
            Throw New ArgumentOutOfRangeException(
                NameOf(totalOrders))
        End If

        If processedOrders < 0 OrElse
           processedOrders > totalOrders Then

            Throw New ArgumentOutOfRangeException(
                NameOf(processedOrders))
        End If

        If String.IsNullOrWhiteSpace(message) Then
            Throw New ArgumentException(
                "A progress message is required.",
                NameOf(message))
        End If

        Me.ProcessedOrders = processedOrders
        Me.TotalOrders = totalOrders
        Me.Message = message
    End Sub

    Public ReadOnly Property ProcessedOrders As Integer

    Public ReadOnly Property TotalOrders As Integer

    Public ReadOnly Property Message As String

    Public ReadOnly Property Percentage As Integer
        Get
            Dim percentageValue =
                ProcessedOrders /
                CDec(TotalOrders) *
                100D

            Return CInt(
                Math.Floor(percentageValue))
        End Get
    End Property

End Class

