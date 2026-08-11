Option Explicit On
Option Strict On
Option Infer On

Imports System
Imports System.Net
Imports System.Net.Http
Imports System.Text
Imports System.Threading
Imports System.Threading.Tasks
Imports AdvancedOrderManager.Application
Imports AdvancedOrderManager.Infrastructure
Imports Microsoft.Extensions.DependencyInjection
Imports Microsoft.Extensions.Http.Resilience
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass>
<TestCategory("Unit")>
Public Class ResilientHttpClientTests

    <TestMethod>
    Public Async Function GetPostsAsync_TemporaryFailures_Retries() _
        As Task

        Dim handler As New SequenceHttpMessageHandler(
            2)

        Dim services As New ServiceCollection()

        services.AddLogging()

        Dim clientBuilder =
            services.AddHttpClient(
                Of IExternalPostService,
                   JsonPlaceholderPostService)(
                    Sub(client)

                        client.BaseAddress =
                            New Uri(
                                "https://example.test/")

                    End Sub)

        clientBuilder _
            .ConfigurePrimaryHttpMessageHandler(
                Function() As HttpMessageHandler

                    Return handler

                End Function)

        clientBuilder _
            .AddStandardResilienceHandler(
                Sub(options)

                    options.Retry.MaxRetryAttempts =
                        3

                    options.Retry.Delay =
                        TimeSpan.Zero

                    options.Retry _
                        .DisableForUnsafeHttpMethods()

                    options.AttemptTimeout.Timeout =
                        TimeSpan.FromSeconds(2)

                    options.TotalRequestTimeout.Timeout =
                        TimeSpan.FromSeconds(5)

                End Sub)

        Using provider =
            services.BuildServiceProvider()

            Dim service =
                provider.GetRequiredService(
                    Of IExternalPostService)()

            Dim posts =
                Await service.GetPostsAsync(
                    1,
                    CancellationToken.None)

            Assert.HasCount(
                1,
                posts)

            Assert.AreEqual(
                3,
                handler.RequestCount)

            Assert.AreEqual(
                1,
                posts(0).Id)

            Assert.AreEqual(
                1,
                posts(0).UserId)

            Assert.AreEqual(
                "Recovered",
                posts(0).Title)

            Assert.AreEqual(
                "Request succeeded after retries",
                posts(0).Body)

        End Using

    End Function

    <TestMethod>
    Public Async Function CreatePostAsync_ServerFailure_DoesNotRetry() _
        As Task

        Dim handler As New AlwaysFailHttpMessageHandler()

        Dim services As New ServiceCollection()

        services.AddLogging()

        Dim clientBuilder =
            services.AddHttpClient(
                Of IExternalPostService,
                   JsonPlaceholderPostService)(
                    Sub(client)

                        client.BaseAddress =
                            New Uri(
                                "https://example.test/")

                    End Sub)

        clientBuilder _
            .ConfigurePrimaryHttpMessageHandler(
                Function() As HttpMessageHandler

                    Return handler

                End Function)

        clientBuilder _
            .AddStandardResilienceHandler(
                Sub(options)

                    options.Retry.MaxRetryAttempts =
                        3

                    options.Retry.Delay =
                        TimeSpan.Zero

                    options.Retry _
                        .DisableForUnsafeHttpMethods()

                    options.AttemptTimeout.Timeout =
                        TimeSpan.FromSeconds(2)

                    options.TotalRequestTimeout.Timeout =
                        TimeSpan.FromSeconds(5)

                End Sub)

        Using provider =
            services.BuildServiceProvider()

            Dim service =
                provider.GetRequiredService(
                    Of IExternalPostService)()

            Dim request =
                New CreateExternalPostRequest(
                    1,
                    "Test",
                    "Test body")

            Dim failureWasObserved As Boolean =
                False

            Try
                Await service.CreatePostAsync(
                    request,
                    CancellationToken.None)

            Catch ex As ExternalApiUnavailableException

                failureWasObserved =
                    True

            End Try

            Assert.IsTrue(
                failureWasObserved,
                "A server failure should produce " &
                "ExternalApiUnavailableException.")

            Assert.AreEqual(
                1,
                handler.RequestCount)

        End Using

    End Function

    Private NotInheritable Class SequenceHttpMessageHandler
        Inherits HttpMessageHandler

        Private ReadOnly _failuresBeforeSuccess As Integer

        Private _requestCount As Integer

        Public Sub New(
            failuresBeforeSuccess As Integer)

            If failuresBeforeSuccess < 0 Then

                Throw New ArgumentOutOfRangeException(
                    NameOf(failuresBeforeSuccess))

            End If

            _failuresBeforeSuccess =
                failuresBeforeSuccess

        End Sub

        Public ReadOnly Property RequestCount As Integer

            Get
                Return Volatile.Read(
                    _requestCount)
            End Get

        End Property

        Protected Overrides Function SendAsync(
            request As HttpRequestMessage,
            cancellationToken As CancellationToken) _
            As Task(Of HttpResponseMessage)

            cancellationToken _
                .ThrowIfCancellationRequested()

            Dim currentAttempt As Integer =
                Interlocked.Increment(
                    _requestCount)

            If currentAttempt <=
               _failuresBeforeSuccess Then

                Dim failedResponse =
                    New HttpResponseMessage(
                        HttpStatusCode.ServiceUnavailable)

                Return Task.FromResult(
                    failedResponse)

            End If

            Const json As String =
                "[" &
                "{" &
                """userId"":1," &
                """id"":1," &
                """title"":""Recovered""," &
                """body"":""Request succeeded after retries""" &
                "}" &
                "]"

            Dim successfulResponse =
                New HttpResponseMessage(
                    HttpStatusCode.OK) With {
                        .Content =
                            New StringContent(
                                json,
                                Encoding.UTF8,
                                "application/json")
                    }

            Return Task.FromResult(
                successfulResponse)

        End Function

    End Class

    Private NotInheritable Class AlwaysFailHttpMessageHandler
        Inherits HttpMessageHandler

        Private _requestCount As Integer

        Public ReadOnly Property RequestCount As Integer

            Get
                Return Volatile.Read(
                    _requestCount)
            End Get

        End Property

        Protected Overrides Function SendAsync(
            request As HttpRequestMessage,
            cancellationToken As CancellationToken) _
            As Task(Of HttpResponseMessage)

            cancellationToken _
                .ThrowIfCancellationRequested()

            Interlocked.Increment(
                _requestCount)

            Dim failedResponse =
                New HttpResponseMessage(
                    HttpStatusCode.ServiceUnavailable)

            Return Task.FromResult(
                failedResponse)

        End Function

    End Class

End Class