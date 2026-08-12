Option Explicit On
Option Strict On
Option Infer On

Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Threading
Imports System.Threading.Tasks
Imports AdvancedOrderManager.Application
Imports Microsoft.Extensions.Logging
Imports Microsoft.Extensions.Options

Public NotInheritable Class ExternalApiAuthenticationHandler
    Inherits DelegatingHandler

    Private ReadOnly _options As ExternalApiAuthenticationOptions

    Private ReadOnly _logger As ILogger(
        Of ExternalApiAuthenticationHandler)

    Public Sub New(
        options As IOptions(
            Of ExternalApiAuthenticationOptions),
        logger As ILogger(
            Of ExternalApiAuthenticationHandler))

        If options Is Nothing Then

            Throw New ArgumentNullException(
                NameOf(options))

        End If

        If logger Is Nothing Then

            Throw New ArgumentNullException(
                NameOf(logger))

        End If

        _options =
            options.Value

        _logger =
            logger
    End Sub

    Protected Overrides Async Function SendAsync(
        request As HttpRequestMessage,
        cancellationToken As CancellationToken) _
        As Task(Of HttpResponseMessage)

        If request Is Nothing Then

            Throw New ArgumentNullException(
                NameOf(request))

        End If

        ApplyAuthentication(
            request)

        Return Await MyBase.SendAsync(
            request,
            cancellationToken)
    End Function

    Private Sub ApplyAuthentication(
        request As HttpRequestMessage)

        Select Case _options.Mode

            Case ExternalApiAuthenticationMode.None

                _logger.LogDebug(
                    "No external API authentication " &
                    "is configured.")

            Case ExternalApiAuthenticationMode.ApiKey

                ApplyApiKey(
                    request)

            Case ExternalApiAuthenticationMode.BearerToken

                ApplyBearerToken(
                    request)

            Case Else

                Throw New ExternalApiAuthenticationException(
                    "The configured external API " &
                    "authentication mode is not supported.")

        End Select
    End Sub

    Private Sub ApplyApiKey(
        request As HttpRequestMessage)

        If String.IsNullOrWhiteSpace(
            _options.ApiKeyHeaderName) Then

            Throw New ExternalApiAuthenticationException(
                "The API-key header name " &
                "has not been configured.")

        End If

        If String.IsNullOrWhiteSpace(
            _options.ApiKey) Then

            Throw New ExternalApiAuthenticationException(
                "The external API key is unavailable. " &
                "Configure it through an approved " &
                "secret source.")

        End If

        request.Headers.Remove(
            _options.ApiKeyHeaderName)

        Dim wasAdded As Boolean =
            request.Headers.TryAddWithoutValidation(
                _options.ApiKeyHeaderName,
                _options.ApiKey)

        If Not wasAdded Then

            Throw New ExternalApiAuthenticationException(
                "The API-key authentication header " &
                "could not be added.")

        End If

        _logger.LogDebug(
            "API-key authentication was added " &
            "using header {HeaderName}.",
            _options.ApiKeyHeaderName)
    End Sub

    Private Sub ApplyBearerToken(
        request As HttpRequestMessage)

        If String.IsNullOrWhiteSpace(
            _options.BearerToken) Then

            Throw New ExternalApiAuthenticationException(
                "The external API bearer token is unavailable. " &
                "Configure it through an approved " &
                "secret source.")

        End If

        request.Headers.Authorization =
            New AuthenticationHeaderValue(
                "Bearer",
                _options.BearerToken)

        _logger.LogDebug(
            "Bearer authentication was added " &
            "to the outgoing API request.")
    End Sub

End Class

