Option Explicit On
Option Strict On
Option Infer On

Imports System.Collections.Generic
Imports System.Threading
Imports System.Threading.Tasks

Public Interface IExternalPostService

    Function GetPostsAsync(
        userId As Integer,
        cancellationToken As CancellationToken) _
        As Task(Of IReadOnlyList(Of ExternalPost))

    Function GetPostAsync(
        postId As Integer,
        cancellationToken As CancellationToken) _
        As Task(Of ExternalPost)

    Function CreatePostAsync(
        request As CreateExternalPostRequest,
        cancellationToken As CancellationToken) _
        As Task(Of ExternalPost)

End Interface

