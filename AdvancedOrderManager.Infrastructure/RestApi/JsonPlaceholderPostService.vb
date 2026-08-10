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

        Dim requestUri As String =
            $"posts?userId={userId}"

        _logger.LogInformation(
            "Requesting external posts for user {UserId}.",
            userId)

        Dim posts As List(Of ExternalPost) =
            Await _httpClient.GetFromJsonAsync(
                Of List(Of ExternalPost))(
                    requestUri,
                    cancellationToken)

        If posts Is Nothing Then
            Return New List(Of ExternalPost)().AsReadOnly()
        End If

        _logger.LogInformation(
            "{PostCount} external posts were returned " &
            "for user {UserId}.",
            posts.Count,
            userId)

        Return posts.AsReadOnly()
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
                    "External post {PostId} was not found.",
                    postId)

                Return Nothing
            End If

            response.EnsureSuccessStatusCode()

            Dim post As ExternalPost =
                Await response.Content.ReadFromJsonAsync(
                    Of ExternalPost)(
                        cancellationToken:=
                            cancellationToken)

            Return post
        End Using
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

        _logger.LogInformation(
            "Sending a demonstration post for user {UserId}.",
            request.UserId)

        Using response As HttpResponseMessage =
            Await _httpClient.PostAsJsonAsync(
                "posts",
                request,
                cancellationToken)

            response.EnsureSuccessStatusCode()

            Dim createdPost As ExternalPost =
                Await response.Content.ReadFromJsonAsync(
                    Of ExternalPost)(
                        cancellationToken:=
                            cancellationToken)

            If createdPost Is Nothing Then

                Throw New InvalidOperationException(
                    "The API returned an empty response.")
            End If

            _logger.LogInformation(
                "The API returned demonstration post ID {PostId}.",
                createdPost.Id)

            Return createdPost
        End Using
    End Function

End Class
