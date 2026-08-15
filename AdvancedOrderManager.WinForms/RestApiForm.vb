Option Explicit On
Option Strict On
Option Infer On

Imports System.Collections.Generic
Imports System.Threading
Imports AdvancedOrderManager.Application
Imports AdvancedOrderManager.Infrastructure
Imports Microsoft.Extensions.Logging

Public Class RestApiForm

    Private _postService As IExternalPostService

    Private _postApplicationService As IExternalPostApplicationService

    Private _logger As ILogger(Of RestApiForm)

    Private _cancellationSource As CancellationTokenSource

    Public Sub New()

        InitializeComponent()

    End Sub

    Public Sub New(
        postService As IExternalPostService,
        postApplicationService As IExternalPostApplicationService,
        logger As ILogger(Of RestApiForm))

        InitializeComponent()

        ArgumentNullException.ThrowIfNull(
            postService)

        ArgumentNullException.ThrowIfNull(
            postApplicationService)

        ArgumentNullException.ThrowIfNull(
            logger)

        _postService =
            postService

        _postApplicationService =
            postApplicationService

        _logger =
            logger

    End Sub

    Private Sub RestApiForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Private Async Sub btnCreatePost_Click(
    sender As Object,
    e As EventArgs) _
    Handles btnCreatePost.Click

        If Not EnsureApplicationServiceAvailable() Then

            Return

        End If

        errorProviderInput.Clear()

        Dim request =
        New CreateExternalPostRequest(
            Decimal.ToInt32(
                nudUserId.Value),
            txtTitle.Text,
            txtBody.Text)

        BeginOperation(
        "Validating and sending JSON to the REST API...")

        Using diagnosticScope =
        New OperationDiagnosticsScope(
            _logger,
            "CreateExternalPost")

            Try
                _logger.LogInformation(
                "Create external post requested " &
                "for user {UserId}.",
                request.UserId)

                Dim submissionResult =
                Await _postApplicationService.CreatePostAsync(
                    request,
                    _cancellationSource.Token)

                If Not submissionResult.ValidationResult.IsValid Then

                    diagnosticScope.MarkRejected()

                    _logger.LogWarning(
                    "Create external post was rejected " &
                    "with {ErrorCount} validation error(s).",
                    submissionResult _
                        .ValidationResult _
                        .Errors _
                        .Count)

                    DisplayValidationErrors(submissionResult.ValidationResult)

                    Return
                End If

                Dim createdPost =
                submissionResult.CreatedPost

                If createdPost Is Nothing Then

                    diagnosticScope.MarkFailed()

                    Throw New InvalidOperationException(
                    "The external post operation completed " &
                    "without returning a post.")
                End If

                Dim results As New List(Of ExternalPost) From {
                    createdPost
                }

                DisplayPosts(
                results.AsReadOnly())

                diagnosticScope.MarkSucceeded()

                _logger.LogInformation(
                "Create external post completed " &
                "successfully with post ID {PostId}.",
                createdPost.Id)

                lblStatus.Text =
                $"The API returned post ID {createdPost.Id}."

                MessageBox.Show(
                Me,
                "The demonstration POST request completed " &
                "successfully." &
                Environment.NewLine &
                Environment.NewLine &
                $"Returned ID: {createdPost.Id}",
                "POST Completed",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information)

            Catch ex As OperationCanceledException

                diagnosticScope.MarkCancelled()

                _logger.LogInformation(
                "Create external post was cancelled.")

                lblStatus.Text =
                "The HTTP request was cancelled."

            Catch ex As ExternalApiAuthenticationException

                diagnosticScope.MarkFailed()

                HandleAuthenticationError(
                ex)

            Catch ex As ExternalApiTimeoutException

                diagnosticScope.MarkFailed()

                HandleApiTimeout(
                ex)

            Catch ex As ExternalApiUnavailableException

                diagnosticScope.MarkFailed()

                HandleApiUnavailable(
                ex)

            Catch ex As ExternalApiException

                diagnosticScope.MarkFailed()

                HandleApiError(
                ex)

            Catch ex As Exception

                diagnosticScope.MarkFailed()

                HandleUnexpectedError(
                ex)

            Finally

                EndOperation()

            End Try

        End Using

    End Sub

    Private Function EnsureApplicationServiceAvailable() As Boolean
        If _postApplicationService Is Nothing Then
            MessageBox.Show(Me, "Application service is not available.", "Configuration error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        If _postService Is Nothing Then
            MessageBox.Show(Me, "Post service is not available.", "Configuration error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        If _logger Is Nothing Then
            MessageBox.Show(Me, "Logger is not available.", "Configuration error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        If _cancellationSource Is Nothing OrElse _cancellationSource.IsCancellationRequested Then
            _cancellationSource = New CancellationTokenSource()
        End If

        Return True
    End Function

    Private Sub BeginOperation(message As String)
        ' Update status and UI for a running operation
        If lblStatus IsNot Nothing Then lblStatus.Text = message
        btnCreatePost.Enabled = False
        Cursor = Cursors.WaitCursor
    End Sub

    Private Sub EndOperation()
        ' Restore UI after operation completes
        btnCreatePost.Enabled = True
        Cursor = Cursors.Default
    End Sub

    Private Sub DisplayValidationErrors(validationResult As Object)
        If validationResult Is Nothing Then
            Return
        End If

        Try
            Dim errorsProp = validationResult.GetType().GetProperty("Errors")
            If errorsProp Is Nothing Then
                MessageBox.Show(Me, "Validation failed (no errors property).", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim errorsObj = errorsProp.GetValue(validationResult)
            Dim errors = TryCast(errorsObj, System.Collections.IEnumerable)
            If errors Is Nothing Then
                MessageBox.Show(Me, "Validation failed (errors not enumerable).", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            errorProviderInput.Clear()

            For Each errorItem In errors
                If errorItem Is Nothing Then
                    Continue For
                End If

                Dim errType = errorItem.GetType()
                Dim messageProp = errType.GetProperty("ErrorMessage")
                If messageProp Is Nothing Then
                    messageProp = errType.GetProperty("Message")
                End If
                Dim propertyNameProp = errType.GetProperty("PropertyName")

                Dim message As String = If(messageProp IsNot Nothing, Convert.ToString(messageProp.GetValue(errorItem)), Convert.ToString(errorItem))
                Dim propertyName As String = If(propertyNameProp IsNot Nothing, Convert.ToString(propertyNameProp.GetValue(errorItem)), String.Empty)

                Select Case propertyName
                    Case "UserId", "UserID", "User Id"
                        errorProviderInput.SetError(nudUserId, message)
                    Case "Title"
                        errorProviderInput.SetError(txtTitle, message)
                    Case "Body"
                        errorProviderInput.SetError(txtBody, message)
                    Case Else
                        ' fallback: append to status label
                        lblStatus.Text = If(String.IsNullOrEmpty(lblStatus.Text), message, lblStatus.Text & " " & message)
                End Select
            Next

        Catch
            MessageBox.Show(Me, "Validation failed.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub HandleAuthenticationError(ex As ExternalApiAuthenticationException)
        If _logger IsNot Nothing Then
            _logger.LogWarning("Create external post failed due to authentication: {Message}", ex.Message)
        End If

        lblStatus.Text = "Authentication failed: " & ex.Message

        MessageBox.Show(Me,
                        "Authentication failed when calling the external API." & Environment.NewLine & Environment.NewLine & ex.Message,
                        "Authentication error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)
    End Sub

    Private Sub HandleApiTimeout(ex As ExternalApiTimeoutException)
        If _logger IsNot Nothing Then
            _logger.LogWarning("Create external post failed due to timeout: {Message}", ex.Message)
        End If

        lblStatus.Text = "Request timed out: " & ex.Message

        MessageBox.Show(Me,
                        "The request to the external API timed out." & Environment.NewLine & Environment.NewLine & ex.Message,
                        "Timeout",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning)
    End Sub

    Private Sub HandleApiUnavailable(ex As ExternalApiUnavailableException)
        If _logger IsNot Nothing Then
            _logger.LogWarning("Create external post failed because the external API is unavailable: {Message}", ex.Message)
        End If

        lblStatus.Text = "API unavailable: " & ex.Message

        MessageBox.Show(Me,
                        "The external API is currently unavailable." & Environment.NewLine & Environment.NewLine & ex.Message,
                        "API Unavailable",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning)
    End Sub

    Private Sub HandleApiError(ex As ExternalApiException)
        If _logger IsNot Nothing Then
            _logger.LogWarning("Create external post failed due to API error: {Message}", ex.Message)
        End If

        lblStatus.Text = "API error: " & ex.Message

        MessageBox.Show(Me,
                    "An error occurred calling the external API." & Environment.NewLine & Environment.NewLine & ex.Message,
                    "API error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error)
    End Sub

    Private Sub HandleUnexpectedError(ex As Exception)
        If _logger IsNot Nothing Then
            _logger.LogError(ex, "Create external post failed due to an unexpected error: {Message}", ex.Message)
        End If

        lblStatus.Text = "Unexpected error: " & ex.Message

        MessageBox.Show(Me,
                        "An unexpected error occurred while calling the external API." & Environment.NewLine & Environment.NewLine & ex.Message,
                        "Unexpected error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)
    End Sub

    Private Sub DisplayPosts(posts As IReadOnlyList(Of ExternalPost))
        If posts Is Nothing OrElse posts.Count = 0 Then
            If lblStatus IsNot Nothing Then lblStatus.Text = "No posts to display."
            Return
        End If

        Dim sb As New System.Text.StringBuilder()
        For Each p In posts
            If p IsNot Nothing Then
                sb.Append("ID: ")
                sb.Append(p.Id)
                sb.Append(" ")
                sb.AppendLine(Convert.ToString(If(GetType(ExternalPost).GetProperty("Title")?.GetValue(p), String.Empty)))
            End If
        Next

        If lblStatus IsNot Nothing Then lblStatus.Text = sb.ToString().Trim()
    End Sub

    Private Async Sub btnLoadPosts_Click(
        sender As Object,
        e As EventArgs) _
        Handles btnLoadPosts.Click

        If Not EnsureApplicationServiceAvailable() Then
            Return
        End If

        Dim userId As Integer = Decimal.ToInt32(nudUserId.Value)

        BeginOperation($"Loading posts for user {userId}...")

        Using diagnosticScope = New OperationDiagnosticsScope(_logger, "LoadExternalPosts")

            Try

                _logger.LogInformation("Loading external posts for user {UserId}.", userId)

                Dim posts = Await _postService.GetPostsAsync(userId, _cancellationSource.Token)

                DisplayPosts(posts)

                diagnosticScope.MarkSucceeded()

                _logger.LogInformation("{PostCount} external posts were loaded for user {UserId}.", posts.Count, userId)

                lblStatus.Text = $"{posts.Count} posts loaded for user {userId}."

            Catch ex As OperationCanceledException

                diagnosticScope.MarkCancelled()

                _logger.LogInformation("Loading external posts for user {UserId} was cancelled.", userId)

                lblStatus.Text = "The HTTP request was cancelled."

            Catch ex As ExternalApiAuthenticationException

                diagnosticScope.MarkFailed()

                HandleAuthenticationError(ex)

            Catch ex As ExternalApiTimeoutException

                diagnosticScope.MarkFailed()

                HandleApiTimeout(ex)

            Catch ex As ExternalApiUnavailableException

                diagnosticScope.MarkFailed()

                HandleApiUnavailable(ex)

            Catch ex As ExternalApiException

                diagnosticScope.MarkFailed()

                HandleApiError(ex)

            Catch ex As Exception

                diagnosticScope.MarkFailed()

                HandleUnexpectedError(ex)

            Finally

                EndOperation()

            End Try

        End Using

    End Sub
End Class
