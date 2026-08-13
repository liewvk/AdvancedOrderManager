Option Explicit On
Option Strict On
Option Infer On

Imports System.Collections.Generic
Imports System.Linq
Imports System.Threading
Imports AdvancedOrderManager.Application
Imports Microsoft.Extensions.Logging

Public Class RestApiForm

    Private _postService As IExternalPostService

    Private _logger As ILogger(Of RestApiForm)

    Private _requestValidator As IInputValidator(
        Of CreateExternalPostRequest)


    Private _cancellationSource As CancellationTokenSource

    Public Sub New()

        InitializeComponent()

    End Sub

    Public Sub New(
    postService As IExternalPostService,
    logger As ILogger(Of RestApiForm),
    requestValidator As IInputValidator(
        Of CreateExternalPostRequest))

        InitializeComponent()

        ArgumentNullException.ThrowIfNull(
        postService)

        ArgumentNullException.ThrowIfNull(
        logger)

        ArgumentNullException.ThrowIfNull(
        requestValidator)

        _postService =
        postService

        _logger =
        logger

        _requestValidator =
        requestValidator
    End Sub


    Private Sub RestApiForm_Load(
        sender As Object,
        e As EventArgs) _
        Handles MyBase.Load

        If _postService Is Nothing Then

            lblStatus.Text =
                "REST API services are unavailable."

            Return
        End If

        txtTitle.Text =
            "Advanced Order Manager API Test"

        txtBody.Text =
            "This post was submitted from a VB.NET " &
            "Windows Forms application."

        lblStatus.Text =
            "Ready"
    End Sub

    Private Async Sub btnLoadPosts_Click(
        sender As Object,
        e As EventArgs) _
        Handles btnLoadPosts.Click

        If Not EnsureServiceAvailable() Then
            Return
        End If

        Dim userId As Integer =
            Decimal.ToInt32(
                nudUserId.Value)

        BeginOperation(
            "Loading posts from the REST API...")

        Try
            Dim posts =
                Await _postService.GetPostsAsync(
                    userId,
                    _cancellationSource.Token)

            DisplayPosts(
                posts)

            lblStatus.Text =
                $"{posts.Count} posts loaded for user {userId}."

        Catch ex As OperationCanceledException

            lblStatus.Text =
                "The HTTP request was cancelled."

        Catch ex As ExternalApiAuthenticationException

            HandleAuthenticationError(
        ex)

        Catch ex As ExternalApiTimeoutException

            HandleApiTimeout(
                ex)

        Catch ex As ExternalApiUnavailableException

            HandleApiUnavailable(
                ex)

        Catch ex As ExternalApiException

            HandleApiError(
                ex)

        Catch ex As Exception

            HandleUnexpectedError(
                ex)

        Finally
            EndOperation()
        End Try
    End Sub

    Private Async Sub btnFindPost_Click(
        sender As Object,
        e As EventArgs) _
        Handles btnFindPost.Click

        If Not EnsureServiceAvailable() Then
            Return
        End If

        Dim postId As Integer =
            Decimal.ToInt32(
                nudPostId.Value)

        BeginOperation(
            $"Requesting post {postId}...")

        Try
            Dim post =
                Await _postService.GetPostAsync(
                    postId,
                    _cancellationSource.Token)

            If post Is Nothing Then

                dgvPosts.DataSource =
                    Nothing

                lblStatus.Text =
                    $"Post {postId} was not found."

                Return
            End If

            Dim results As New List(Of ExternalPost) From {
                post
            }

            DisplayPosts(
                results.AsReadOnly())

            txtTitle.Text =
                post.Title

            txtBody.Text =
                post.Body

            lblStatus.Text =
                $"Post {post.Id} loaded successfully."

        Catch ex As OperationCanceledException

            lblStatus.Text =
                "The HTTP request was cancelled."

        Catch ex As ExternalApiTimeoutException

            HandleApiTimeout(
                ex)

        Catch ex As ExternalApiUnavailableException

            HandleApiUnavailable(
                ex)

        Catch ex As ExternalApiException

            HandleApiError(
                ex)

        Catch ex As Exception

            HandleUnexpectedError(
                ex)

        Finally
            EndOperation()
        End Try
    End Sub

    Private Async Sub btnCreatePost_Click(
        sender As Object,
        e As EventArgs) _
        Handles btnCreatePost.Click

        If Not EnsureServiceAvailable() Then
            Return
        End If

        If Not EnsureServiceAvailable() Then

            Return

        End If

        If Not EnsureRequestValidatorAvailable() Then

            Return

        End If

        Dim request =
    New CreateExternalPostRequest(
        Decimal.ToInt32(
            nudUserId.Value),
        txtTitle.Text.Trim(),
        txtBody.Text.Trim())

        If Not ValidateCreateRequest(
    request) Then

            Return

        End If



        BeginOperation(
            "Sending JSON to the REST API...")

        Try
            Dim createdPost =
                Await _postService.CreatePostAsync(
                    request,
                    _cancellationSource.Token)

            Dim results As New List(Of ExternalPost) From {
                createdPost
            }

            DisplayPosts(
                results.AsReadOnly())

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

            lblStatus.Text =
                "The HTTP request was cancelled."

        Catch ex As ExternalApiTimeoutException

            HandleApiTimeout(
                ex)

        Catch ex As ExternalApiUnavailableException

            HandleApiUnavailable(
                ex)

        Catch ex As ExternalApiException

            HandleApiError(
                ex)

        Catch ex As Exception

            HandleUnexpectedError(
                ex)

        Finally
            EndOperation()
        End Try
    End Sub

    Private Sub btnCancel_Click(
        sender As Object,
        e As EventArgs) _
        Handles btnCancel.Click

        If _cancellationSource Is Nothing Then
            Return
        End If

        btnCancel.Enabled =
            False

        lblStatus.Text =
            "Cancellation requested..."

        _cancellationSource.Cancel()
    End Sub

    Private Sub DisplayPosts(
        posts As IReadOnlyList(Of ExternalPost))

        dgvPosts.DataSource =
            Nothing

        dgvPosts.DataSource =
            posts.ToList()
    End Sub

    Private Sub BeginOperation(
        statusMessage As String)

        If _cancellationSource IsNot Nothing Then

            _cancellationSource.Dispose()

            _cancellationSource =
                Nothing
        End If

        _cancellationSource =
            New CancellationTokenSource()

        SetBusyState(
            True)

        lblStatus.Text =
            statusMessage
    End Sub

    Private Sub EndOperation()

        SetBusyState(
            False)

        If _cancellationSource IsNot Nothing Then

            _cancellationSource.Dispose()

            _cancellationSource =
                Nothing
        End If
    End Sub

    Private Sub SetBusyState(
        isBusy As Boolean)

        btnLoadPosts.Enabled =
            Not isBusy

        btnFindPost.Enabled =
            Not isBusy

        btnCreatePost.Enabled =
            Not isBusy

        btnCancel.Enabled =
            isBusy

        nudUserId.Enabled =
            Not isBusy

        nudPostId.Enabled =
            Not isBusy

        txtTitle.Enabled =
            Not isBusy

        txtBody.Enabled =
            Not isBusy

        UseWaitCursor =
            isBusy
    End Sub

    Private Function EnsureServiceAvailable() _
        As Boolean

        If _postService IsNot Nothing Then
            Return True
        End If

        MessageBox.Show(
        Me,
        "Input validation services are unavailable. " &
        "Start the application through Program.Main.",
        "Validation Service Unavailable",
        MessageBoxButtons.OK,
        MessageBoxIcon.Error)


        Return False
    End Function
    Private Function ValidateCreateRequest(
    request As CreateExternalPostRequest) _
    As Boolean

        errorProviderInput.Clear()

        Dim result As InputValidationResult =
        _requestValidator.Validate(
            request)

        If result.IsValid Then

            Return True

        End If

        For Each validationError As InputValidationError In
        result.Errors

            Select Case validationError.FieldName

                Case NameOf(
                CreateExternalPostRequest.UserId)

                    errorProviderInput.SetError(
                    nudUserId,
                    validationError.Message)

                Case NameOf(
                CreateExternalPostRequest.Title)

                    errorProviderInput.SetError(
                    txtTitle,
                    validationError.Message)

                Case NameOf(
                CreateExternalPostRequest.Body)

                    errorProviderInput.SetError(
                    txtBody,
                    validationError.Message)

            End Select
        Next

        lblStatus.Text =
        "Please correct the highlighted input."

        Return False
    End Function


    Private Sub HandleApiTimeout(
        exception As ExternalApiTimeoutException)

        lblStatus.Text =
            "The external API timed out."

        If _logger IsNot Nothing Then

            _logger.LogWarning(
                exception,
                "The external API operation timed out.")
        End If

        MessageBox.Show(
            Me,
            "The external service took too long to respond." &
            Environment.NewLine &
            "Please try again.",
            "API Timeout",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning)
    End Sub

    Private Sub HandleApiUnavailable(
        exception As ExternalApiUnavailableException)

        lblStatus.Text =
            "The external API is unavailable."

        If _logger IsNot Nothing Then

            _logger.LogWarning(
                exception,
                "The external API is currently unavailable.")
        End If

        MessageBox.Show(
            Me,
            exception.Message,
            "API Unavailable",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning)
    End Sub
    Private Sub HandleApiError(
    exception As ExternalApiException)

        lblStatus.Text =
        "The external API operation failed."

        If _logger IsNot Nothing Then

            _logger.LogError(
            exception,
            "An external API operation failed.")

        End If

        MessageBox.Show(
        Me,
        exception.Message,
        "External API Error",
        MessageBoxButtons.OK,
        MessageBoxIcon.Error)

    End Sub


    Private Sub HandleUnexpectedError(
        exception As Exception)

        lblStatus.Text =
            "An unexpected REST API error occurred."

        If _logger IsNot Nothing Then

            _logger.LogError(
                exception,
                "An unexpected REST API error occurred.")
        End If

        MessageBox.Show(
            Me,
            exception.Message,
            "REST API Error",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
    End Sub

    Private Sub RestApiForm_FormClosing(
        sender As Object,
        e As FormClosingEventArgs) _
        Handles MyBase.FormClosing

        If _cancellationSource IsNot Nothing Then

            _cancellationSource.Cancel()

        End If
    End Sub
    Private Sub HandleAuthenticationError(
    exception As ExternalApiAuthenticationException)

        lblStatus.Text =
        "External API authentication failed."

        If _logger IsNot Nothing Then

            _logger.LogWarning(
            exception,
            "The external API authentication " &
            "configuration is unavailable or invalid.")

        End If

        MessageBox.Show(
        Me,
        exception.Message,
        "API Authentication Error",
        MessageBoxButtons.OK,
        MessageBoxIcon.Warning)

    End Sub

    Private Function EnsureRequestValidatorAvailable() As Boolean
        If _requestValidator IsNot Nothing Then
            Return True
        End If

        MessageBox.Show(
            Me,
            "Input validation services are unavailable. Start the application through Program.Main.",
            "Validation Service Unavailable",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

        Return False
    End Function
    Private Sub txtTitle_TextChanged(
    sender As Object,
    e As EventArgs) _
    Handles txtTitle.TextChanged

        errorProviderInput.SetError(
        txtTitle,
        String.Empty)
    End Sub

    Private Sub txtBody_TextChanged(
    sender As Object,
    e As EventArgs) _
    Handles txtBody.TextChanged

        errorProviderInput.SetError(
        txtBody,
        String.Empty)
    End Sub

    Private Sub nudUserId_ValueChanged(
    sender As Object,
    e As EventArgs) _
    Handles nudUserId.ValueChanged

        errorProviderInput.SetError(
        nudUserId,
        String.Empty)
    End Sub

End Class