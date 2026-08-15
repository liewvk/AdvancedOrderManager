Option Explicit On
Option Strict On
Option Infer On

Imports System
Imports System.Collections.Generic
Imports System.Net.Http
Imports System.Threading
Imports System.Threading.Tasks
Imports AdvancedOrderManager.Application
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Microsoft.Extensions.Logging.Abstractions

<TestClass>
<TestCategory("Unit")>
Public Class ExternalPostApplicationServiceTests

    Private _validator As CreateExternalPostRequestValidator

    <TestInitialize>
    Public Sub Initialise()

        _validator =
            New CreateExternalPostRequestValidator()

    End Sub

    <TestMethod>
    Public Async Function CreatePostAsync_ValidRequest_CallsExternalService() _
        As Task

        'Arrange

        Dim externalService =
            New RecordingExternalPostService()

        externalService.NextCreatedPost =
            New ExternalPost With {
                .UserId = 1,
                .Id = 101,
                .Title = "Created title",
                .Body = "Created body"
            }

        Dim applicationService =
    New ExternalPostApplicationService(
        externalService,
        _validator,
        NullLogger(
            Of ExternalPostApplicationService).Instance)


        Dim request =
            New CreateExternalPostRequest(
                1,
                "  Quarterly report  ",
                "  Demonstration body  ")

        'Act

        Dim result =
            Await applicationService.CreatePostAsync(
                request,
                CancellationToken.None)

        'Assert

        Assert.IsTrue(
            result.WasSuccessful)

        Assert.IsNotNull(
            result.CreatedPost)

        Assert.AreEqual(
            101,
            result.CreatedPost.Id)

        Assert.AreEqual(
            1,
            externalService.CreateCallCount)

        Assert.IsNotNull(
            externalService.LastCreateRequest)

        Assert.AreEqual(
            "Quarterly report",
            externalService.LastCreateRequest.Title)

        Assert.AreEqual(
            "Demonstration body",
            externalService.LastCreateRequest.Body)

    End Function

    <TestMethod>
    Public Async Function CreatePostAsync_InvalidRequest_DoesNotCallExternalService() _
        As Task

        'Arrange

        Dim externalService =
            New RecordingExternalPostService()

        Dim applicationService =
    New ExternalPostApplicationService(
        externalService,
        _validator,
        NullLogger(
            Of ExternalPostApplicationService).Instance)


        Dim request =
            New CreateExternalPostRequest(
                0,
                String.Empty,
                String.Empty)

        'Act

        Dim result =
            Await applicationService.CreatePostAsync(
                request,
                CancellationToken.None)

        'Assert

        Assert.IsFalse(
            result.WasSuccessful)

        Assert.IsNull(
            result.CreatedPost)

        Assert.HasCount(
            3,
            result.ValidationResult.Errors)

        Assert.AreEqual(
            0,
            externalService.CreateCallCount)

    End Function

    <TestMethod>
    Public Async Function CreatePostAsync_ExternalServiceFails_PropagatesApplicationException() _
        As Task

        'Arrange

        Dim externalService =
            New RecordingExternalPostService()

        externalService.ExceptionToThrow =
            New ExternalApiUnavailableException(
                "The test external API is unavailable.",
                New HttpRequestException(
                    "Simulated HTTP failure."))

        Dim applicationService =
    New ExternalPostApplicationService(
        externalService,
        _validator,
        NullLogger(
            Of ExternalPostApplicationService).Instance)


        Dim request =
            New CreateExternalPostRequest(
                1,
                "Valid title",
                "Valid body")

        Dim failureWasObserved As Boolean =
            False

        'Act

        Try

            Await applicationService.CreatePostAsync(
                request,
                CancellationToken.None)

        Catch ex As ExternalApiUnavailableException

            failureWasObserved =
                True

        End Try

        'Assert

        Assert.IsTrue(
            failureWasObserved)

        Assert.AreEqual(
            1,
            externalService.CreateCallCount)

    End Function

    <TestMethod>
    Public Async Function CreatePostAsync_ValidRequest_ForwardsCancellationToken() _
        As Task

        'Arrange

        Dim externalService =
            New RecordingExternalPostService()

        Dim applicationService =
    New ExternalPostApplicationService(
        externalService,
        _validator,
        NullLogger(
            Of ExternalPostApplicationService).Instance)


        Dim request =
            New CreateExternalPostRequest(
                1,
                "Valid title",
                "Valid body")

        Using cancellationSource =
            New CancellationTokenSource()

            Dim expectedToken =
                cancellationSource.Token

            'Act

            Await applicationService.CreatePostAsync(
                request,
                expectedToken)

            'Assert

            Assert.AreEqual(
                expectedToken,
                externalService.LastCancellationToken)

        End Using

    End Function

    Private NotInheritable Class RecordingExternalPostService
        Implements IExternalPostService

        Public Property CreateCallCount As Integer

        Public Property LastCreateRequest As CreateExternalPostRequest

        Public Property LastCancellationToken As CancellationToken

        Public Property ExceptionToThrow As Exception

        Public Property NextCreatedPost As ExternalPost =
            New ExternalPost With {
                .UserId = 1,
                .Id = 100,
                .Title = "Created",
                .Body = "Created body"
            }

        Public Function GetPostsAsync(
            userId As Integer,
            cancellationToken As CancellationToken) _
            As Task(Of IReadOnlyList(Of ExternalPost)) _
            Implements IExternalPostService.GetPostsAsync

            Dim posts As IReadOnlyList(Of ExternalPost) =
                Array.Empty(Of ExternalPost)()

            Return Task.FromResult(
                posts)
        End Function

        Public Function GetPostAsync(
            postId As Integer,
            cancellationToken As CancellationToken) _
            As Task(Of ExternalPost) _
            Implements IExternalPostService.GetPostAsync

            Return Task.FromResult(
                CType(
                    Nothing,
                    ExternalPost))
        End Function

        Public Function CreatePostAsync(
            request As CreateExternalPostRequest,
            cancellationToken As CancellationToken) _
            As Task(Of ExternalPost) _
            Implements IExternalPostService.CreatePostAsync

            CreateCallCount += 1

            LastCreateRequest =
                request

            LastCancellationToken =
                cancellationToken

            If cancellationToken.IsCancellationRequested Then

                Return Task.FromCanceled(
                    Of ExternalPost)(
                        cancellationToken)

            End If

            If ExceptionToThrow IsNot Nothing Then

                Return Task.FromException(
                    Of ExternalPost)(
                        ExceptionToThrow)

            End If

            Return Task.FromResult(
                NextCreatedPost)
        End Function

    End Class

End Class
