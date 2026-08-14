Option Explicit On
Option Strict On
Option Infer On

Imports System.Threading
Imports System.Threading.Tasks

Public NotInheritable Class ExternalPostApplicationService
    Implements IExternalPostApplicationService

    Private ReadOnly _postService As IExternalPostService

    Private ReadOnly _requestValidator As IInputValidator(
            Of CreateExternalPostRequest)

    Public Sub New(
        postService As IExternalPostService,
        requestValidator As IInputValidator(
            Of CreateExternalPostRequest))

        ArgumentNullException.ThrowIfNull(
            postService)

        ArgumentNullException.ThrowIfNull(
            requestValidator)

        _postService =
            postService

        _requestValidator =
            requestValidator
    End Sub

    Public Async Function CreatePostAsync(
        request As CreateExternalPostRequest,
        cancellationToken As CancellationToken) _
        As Task(Of ExternalPostSubmissionResult) _
        Implements IExternalPostApplicationService.CreatePostAsync

        ArgumentNullException.ThrowIfNull(
            request)

        Dim validationResult =
            _requestValidator.Validate(
                request)

        If Not validationResult.IsValid Then

            Return ExternalPostSubmissionResult _
                .ValidationFailed(
                    validationResult)

        End If

        Dim normalizedRequest =
            New CreateExternalPostRequest(
                request.UserId,
                request.Title.Trim(),
                request.Body.Trim())

        Dim createdPost =
            Await _postService.CreatePostAsync(
                normalizedRequest,
                cancellationToken)

        Return ExternalPostSubmissionResult _
            .Success(
                createdPost)
    End Function

End Class

