Option Explicit On
Option Strict On
Option Infer On

Imports System.Collections.Generic

Public NotInheritable Class ConcurrentBatchSummary

    Public Sub New(
        requestedOrders As Integer,
        completedOrders As Integer,
        failedOrders As Integer,
        peakConcurrency As Integer,
        elapsedTime As TimeSpan,
        results As IReadOnlyList(Of ConcurrentOrderResult))

        If requestedOrders <= 0 Then
            Throw New ArgumentOutOfRangeException(
                NameOf(requestedOrders))
        End If

        If completedOrders < 0 OrElse
           completedOrders > requestedOrders Then

            Throw New ArgumentOutOfRangeException(
                NameOf(completedOrders))
        End If

        If failedOrders < 0 OrElse
           failedOrders > completedOrders Then

            Throw New ArgumentOutOfRangeException(
                NameOf(failedOrders))
        End If

        If peakConcurrency < 0 Then
            Throw New ArgumentOutOfRangeException(
                NameOf(peakConcurrency))
        End If

        If elapsedTime < TimeSpan.Zero Then
            Throw New ArgumentOutOfRangeException(
                NameOf(elapsedTime))
        End If

        If results Is Nothing Then
            Throw New ArgumentNullException(
                NameOf(results))
        End If

        Me.RequestedOrders = requestedOrders
        Me.CompletedOrders = completedOrders
        Me.FailedOrders = failedOrders
        Me.PeakConcurrency = peakConcurrency
        Me.ElapsedTime = elapsedTime
        Me.Results = results
    End Sub

    Public ReadOnly Property RequestedOrders As Integer

    Public ReadOnly Property CompletedOrders As Integer

    Public ReadOnly Property FailedOrders As Integer

    Public ReadOnly Property PeakConcurrency As Integer

    Public ReadOnly Property ElapsedTime As TimeSpan

    Public ReadOnly Property Results As IReadOnlyList(
        Of ConcurrentOrderResult)

End Class

