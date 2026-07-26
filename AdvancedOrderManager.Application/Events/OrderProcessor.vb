Option Explicit On
Option Strict On
Option Infer On

Public NotInheritable Class OrderProcessor

    Private ReadOnly _validator As Func(Of OrderSubmission, String)

    Private ReadOnly _totalCalculator As Func(Of OrderSubmission, Decimal)

    Public Sub New(
    validator As Func(Of OrderSubmission, String),
    totalCalculator As Func(Of OrderSubmission, Decimal))

        If validator Is Nothing Then

            Throw New ArgumentNullException(
            NameOf(validator))
        End If

        If totalCalculator Is Nothing Then

            Throw New ArgumentNullException(
            NameOf(totalCalculator))
        End If

        _validator = validator
        _totalCalculator = totalCalculator
    End Sub

    Public Event OrderProcessed As EventHandler(Of OrderProcessedEventArgs)

    Public Event OrderRejected As EventHandler(Of OrderRejectedEventArgs)
    Public Function Process(
        order As OrderSubmission) As Boolean

        If order Is Nothing Then
            Throw New ArgumentNullException(
                NameOf(order))
        End If

        Dim rejectionReason =
            _validator(order)

        If Not String.IsNullOrWhiteSpace(
            rejectionReason) Then

            OnOrderRejected(
                New OrderRejectedEventArgs(
                    order.OrderNumber,
                    rejectionReason,
                    DateTimeOffset.UtcNow))

            Return False
        End If

        Dim total =
            _totalCalculator(order)

        OnOrderProcessed(
            New OrderProcessedEventArgs(
                order.OrderNumber,
                order.CustomerName,
                total,
                order.IsPriority,
                DateTimeOffset.UtcNow))

        Return True
    End Function

    Private Sub OnOrderProcessed(
        eventArguments As OrderProcessedEventArgs)

        RaiseEvent OrderProcessed(
            Me,
            eventArguments)
    End Sub

    Private Sub OnOrderRejected(
        eventArguments As OrderRejectedEventArgs)

        RaiseEvent OrderRejected(
            Me,
            eventArguments)
    End Sub

End Class

