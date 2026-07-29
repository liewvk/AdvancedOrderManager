Option Explicit On
Option Strict On
Option Infer On

Imports System.Threading
Imports Microsoft.Extensions.Hosting
Imports Microsoft.Extensions.Logging

Public NotInheritable Class ApplicationStartupReporter
    Implements IHostedService

    Private ReadOnly _logger As ILogger(Of ApplicationStartupReporter)

    Public Sub New(
        logger As ILogger(
            Of ApplicationStartupReporter))

        If logger Is Nothing Then

            Throw New ArgumentNullException(
                NameOf(logger))
        End If

        _logger = logger
    End Sub

    Public Function StartAsync(
        cancellationToken As CancellationToken) _
        As Task _
        Implements IHostedService.StartAsync

        _logger.LogInformation(
            "Advanced Order Manager host started at {StartedAt}.",
            DateTimeOffset.Now)

        Return Task.CompletedTask
    End Function

    Public Function StopAsync(
        cancellationToken As CancellationToken) _
        As Task _
        Implements IHostedService.StopAsync

        _logger.LogInformation(
            "Advanced Order Manager host stopped at {StoppedAt}.",
            DateTimeOffset.Now)

        Return Task.CompletedTask
    End Function

End Class

