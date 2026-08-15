Option Explicit On
Option Strict On
Option Infer On

Imports System.Collections.Generic
Imports System.Diagnostics
Imports Microsoft.Extensions.Logging

Public NotInheritable Class OperationDiagnosticsScope
    Implements IDisposable

    Private ReadOnly _logger As ILogger

    Private ReadOnly _loggingScope As IDisposable

    Private ReadOnly _timer As Stopwatch

    Private _outcome As String =
        "Completed"

    Private _disposed As Boolean

    Public Sub New(
        logger As ILogger,
        operationName As String)

        ArgumentNullException.ThrowIfNull(
            logger)

        If String.IsNullOrWhiteSpace(
            operationName) Then

            Throw New ArgumentException(
                "An operation name is required.",
                NameOf(operationName))
        End If

        _logger =
            logger

        Me.OperationName =
            operationName

        Me.CorrelationId =
            Guid.NewGuid().ToString(
                "N")

        Dim scopeState As New Dictionary(Of String, Object) From {
                {
                    "CorrelationId",
                    Me.CorrelationId
                },
                {
                    "OperationName",
                    Me.OperationName
                }
            }

        _loggingScope =
            _logger.BeginScope(
                scopeState)

        _timer =
            Stopwatch.StartNew()

        _logger.LogInformation(
            "Operation {OperationName} started. " &
            "Correlation ID: {CorrelationId}.",
            Me.OperationName,
            Me.CorrelationId)
    End Sub

    Public ReadOnly Property CorrelationId As String

    Public ReadOnly Property OperationName As String

    Public Sub MarkSucceeded()

        _outcome =
            "Succeeded"

    End Sub

    Public Sub MarkRejected()

        _outcome =
            "Rejected"

    End Sub

    Public Sub MarkCancelled()

        _outcome =
            "Cancelled"

    End Sub

    Public Sub MarkFailed()

        _outcome =
            "Failed"

    End Sub

    Public Sub Dispose() _
        Implements IDisposable.Dispose

        If _disposed Then
            Return
        End If

        _disposed =
            True

        _timer.Stop()

        _logger.LogInformation(
            "Operation {OperationName} finished " &
            "with outcome {Outcome} in " &
            "{ElapsedMilliseconds} ms. " &
            "Correlation ID: {CorrelationId}.",
            OperationName,
            _outcome,
            _timer.ElapsedMilliseconds,
            CorrelationId)

        If _loggingScope IsNot Nothing Then

            _loggingScope.Dispose()

        End If
    End Sub

End Class

