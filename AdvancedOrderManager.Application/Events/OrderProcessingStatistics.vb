Option Explicit On
Option Strict On
Option Infer On

Public NotInheritable Class OrderProcessingStatistics

    Private _processedCount As Integer
    Private _rejectedCount As Integer
    Private _totalRevenue As Decimal

    Public ReadOnly Property ProcessedCount As Integer
        Get
            Return _processedCount
        End Get
    End Property

    Public ReadOnly Property RejectedCount As Integer
        Get
            Return _rejectedCount
        End Get
    End Property

    Public ReadOnly Property TotalRevenue As Decimal
        Get
            Return _totalRevenue
        End Get
    End Property

    Public Sub HandleOrderProcessed(
        sender As Object,
        e As OrderProcessedEventArgs)

        If e Is Nothing Then
            Throw New ArgumentNullException(NameOf(e))
        End If

        _processedCount += 1
        _totalRevenue += e.TotalAmount
    End Sub

    Public Sub HandleOrderRejected(
        sender As Object,
        e As OrderRejectedEventArgs)

        If e Is Nothing Then
            Throw New ArgumentNullException(NameOf(e))
        End If

        _rejectedCount += 1
    End Sub

End Class