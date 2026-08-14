Option Explicit On
Option Strict On
Option Infer On

Imports System.Threading
Imports System.Threading.Tasks

Public Interface IExternalPostApplicationService

    Function CreatePostAsync(
        request As CreateExternalPostRequest,
        cancellationToken As CancellationToken) _
        As Task(Of ExternalPostSubmissionResult)

End Interface

