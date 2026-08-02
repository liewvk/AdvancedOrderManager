Option Explicit On
Option Strict On
Option Infer On

Public NotInheritable Class ConcurrentBatchProgress

    Public Sub New(
        completedOrders As Integer,
        totalOrders As Integer,
        activeOperations As Integer,
        message As String)

        If totalOrders <= 0 Then
            Throw New ArgumentOutOfRangeException(
                NameOf(totalOrders))
        End If

        If completedOrders < 0 OrElse
           completedOrders > totalOrders Then

            Throw New ArgumentOutOfRangeException(
                NameOf(completedOrders))
        End If

        If activeOperations < 0 Then
            Throw New ArgumentOutOfRangeException(
                NameOf(activeOperations))
        End If

        If String.IsNullOrWhiteSpace(message) Then
            Throw New ArgumentException(
                "A progress message is required.",
                NameOf(message))
        End If

        Me.CompletedOrders = completedOrders
        Me.TotalOrders = totalOrders
        Me.ActiveOperations = activeOperations
        Me.Message = message
    End Sub

    Public ReadOnly Property CompletedOrders As Integer

    Public ReadOnly Property TotalOrders As Integer

    Public ReadOnly Property ActiveOperations As Integer

    Public ReadOnly Property Message As String

    Public ReadOnly Property Percentage As Integer
        Get
            Dim percentageValue As Decimal =
                CompletedOrders /
                CDec(TotalOrders) *
                100D

            Return CInt(
                Math.Floor(percentageValue))
        End Get
    End Property

End Class

