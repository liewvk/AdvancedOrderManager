Option Explicit On
Option Strict On
Option Infer On

Public NotInheritable Class OrderHistoryStatistics

    Public Sub New(
        totalOrders As Integer,
        priorityOrders As Integer,
        totalAmount As Decimal,
        averageAmount As Decimal)

        If totalOrders < 0 Then
            Throw New ArgumentOutOfRangeException(
                NameOf(totalOrders))
        End If

        If priorityOrders < 0 OrElse
           priorityOrders > totalOrders Then

            Throw New ArgumentOutOfRangeException(
                NameOf(priorityOrders))
        End If

        If totalAmount < 0D Then
            Throw New ArgumentOutOfRangeException(
                NameOf(totalAmount))
        End If

        If averageAmount < 0D Then
            Throw New ArgumentOutOfRangeException(
                NameOf(averageAmount))
        End If

        Me.TotalOrders = totalOrders
        Me.PriorityOrders = priorityOrders
        Me.TotalAmount = totalAmount
        Me.AverageAmount = averageAmount
    End Sub

    Public ReadOnly Property TotalOrders As Integer

    Public ReadOnly Property PriorityOrders As Integer

    Public ReadOnly Property TotalAmount As Decimal

    Public ReadOnly Property AverageAmount As Decimal

End Class

