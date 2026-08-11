Option Explicit On
Option Strict On
Option Infer On

Imports System.Collections.Generic
Imports System.Net
Imports System.Net.Http
Imports System.Net.Http.Json
Imports System.Threading
Imports System.Threading.Tasks
Imports AdvancedOrderManager.Application
Imports Microsoft.Extensions.Logging
Imports Polly.CircuitBreaker
Imports Polly.Timeout

Public NotInheritable Class JsonPlaceholderPostService
    Implements IExternalPostService

    Private ReadOnly _httpClient As HttpClient

    Private ReadOnly _logger As ILogger(
        Of JsonPlaceholderPostService)

    Public Sub New(
        httpClient As HttpClient,
        logger As ILogger(Of JsonPlaceholderPostService))

        If httpClient Is Nothing Then
            Throw New ArgumentNullException(
                NameOf(httpClient))
        End If

        If logger Is Nothing Then
            Throw New ArgumentNullException(
                NameOf(logger))
        End If

        _httpClient = httpClient
        _logger = logger
    End Sub

    Public Async Function GetPostsAsync(
        userId As Integer,
        cancellationToken As CancellationToken) _
        As Task(Of IReadOnlyList(Of ExternalPost)) _
        Implements IExternalPostService.GetPostsAsync

        If userId <= 0 Then
            Throw New ArgumentOutOfRangeException(
                NameOf(userId),
                "The user ID must be greater than zero.")
        End If

        Return Await ExecuteApiOperationAsync(
            $"Load posts for user {userId}",
            Async Function()

                Dim requestUri As String =
                    $"posts?userId={userId}"

                _logger.LogInformation(
                    "Requesting external posts " &
                    "for user {UserId}.",
                    userId)

                Dim posts As List(Of ExternalPost) =
                    Await _httpClient.GetFromJsonAsync(
                        Of List(Of ExternalPost))(
                            requestUri,
                            cancellationToken)

                If posts Is Nothing Then

                    Return CType(
                        New List(Of ExternalPost)() _
                            .AsReadOnly(),
                        IReadOnlyList(Of ExternalPost))
                End If

                _logger.LogInformation(
                    "{PostCount} external posts " &
                    "were returned for user {UserId}.",
                    posts.Count,
                    userId)

                Return CType(
                    posts.AsReadOnly(),
                    IReadOnlyList(Of ExternalPost))

            End Function,
            cancellationToken)
    End Function

    Public Async Function GetPostAsync(
        postId As Integer,
        cancellationToken As CancellationToken) _
        As Task(Of ExternalPost) _
        Implements IExternalPostService.GetPostAsync

        If postId <= 0 Then
            Throw New ArgumentOutOfRangeException(
                NameOf(postId),
                "The post ID must be greater than zero.")
        End If

        Return Await ExecuteApiOperationAsync(
            $"Load post {postId}",
            Async Function()

                Dim requestUri As String =
                    $"posts/{postId}"

                _logger.LogInformation(
                    "Requesting external post {PostId}.",
                    postId)

                Using response As HttpResponseMessage =
                    Await _httpClient.GetAsync(
                        requestUri,
                        cancellationToken)

                    If response.StatusCode =
                       HttpStatusCode.NotFound Then

                        _logger.LogWarning(
                            "External post {PostId} " &
                            "was not found.",
                            postId)

                        Return Nothing
                    End If

                    response.EnsureSuccessStatusCode()

                    Return Await response.Content _
                        .ReadFromJsonAsync(
                            Of ExternalPost)(
                                cancellationToken:=
                                    cancellationToken)

                End Using

            End Function,
            cancellationToken)
    End Function

    Public Async Function CreatePostAsync(
        request As CreateExternalPostRequest,
        cancellationToken As CancellationToken) _
        As Task(Of ExternalPost) _
        Implements IExternalPostService.CreatePostAsync

        If request Is Nothing Then
            Throw New ArgumentNullException(
                NameOf(request))
        End If

        Return Await ExecuteApiOperationAsync(
            "Create demonstration post",
            Async Function()

                _logger.LogInformation(
                    "Sending a demonstration post " &
                    "for user {UserId}.",
                    request.UserId)

                Using response As HttpResponseMessage =
                    Await _httpClient.PostAsJsonAsync(
                        "posts",
                        request,
                        cancellationToken)

                    response.EnsureSuccessStatusCode()

                    Dim createdPost As ExternalPost =
                        Await response.Content _
                            .ReadFromJsonAsync(
                                Of ExternalPost)(
                                    cancellationToken:=
                                        cancellationToken)

                    If createdPost Is Nothing Then

                        Throw New InvalidOperationException(
                            "The API returned an empty response.")
                    End If

                    _logger.LogInformation(
                        "The API returned demonstration " &
                        "post ID {PostId}.",
                        createdPost.Id)

                    Return createdPost
                End Using

            End Function,
            cancellationToken)
    End Function

    Private Async Function ExecuteApiOperationAsync(
        Of TResult)(
        operationName As String,
        operation As Func(Of Task(Of TResult)),
        cancellationToken As CancellationToken) _
        As Task(Of TResult)

        Try
            Return Await operation()

        Catch ex As OperationCanceledException When cancellationToken.IsCancellationRequested

            _logger.LogInformation(
                "{OperationName} was cancelled by the caller.",
                operationName)

            Throw

        Catch ex As TimeoutRejectedException

            _logger.LogWarning(
                ex,
                "{OperationName} exceeded " &
                "the resilience timeout.",
                operationName)

            Throw New ExternalApiTimeoutException(
                "The external API did not respond within " &
                "the permitted time.",
                ex)

        Catch ex As BrokenCircuitException

            _logger.LogWarning(
                ex,
                "The circuit breaker prevented " &
                "{OperationName}.",
                operationName)

            Throw New ExternalApiUnavailableException(
                "The external API is temporarily unavailable. " &
                "Please try again later.",
                ex)

        Catch ex As HttpRequestException

            _logger.LogError(
                ex,
                "{OperationName} failed because of " &
                "an HTTP communication error.",
                operationName)

            Throw New ExternalApiUnavailableException(
                "The application could not communicate " &
                "with the external API.",
                ex)

        End Try
    End Function

End Class
