Option Explicit On
Option Strict On
Option Infer On

Imports System.Linq
Imports System.Net
Imports System.Net.Http
Imports System.Threading
Imports System.Threading.Tasks
Imports AdvancedOrderManager.Application
Imports AdvancedOrderManager.Infrastructure
Imports Microsoft.Extensions.Logging.Abstractions
Imports Microsoft.Extensions.Options
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass>
<TestCategory("Unit")>
Public Class ExternalApiAuthenticationHandlerTests

    <TestMethod>
    Public Async Function ApiKeyMode_AddsConfiguredHeader() _
        As Task

        Dim authenticationOptions =
            Options.Create(
                New ExternalApiAuthenticationOptions With {
                    .Mode =
                        ExternalApiAuthenticationMode.ApiKey,
                    .ApiKeyHeaderName =
                        "X-API-Key",
                    .ApiKey =
                        "TEST-API-KEY"
                })

        Dim recordingHandler =
            New RecordingHttpMessageHandler()

        Dim authenticationHandler =
            New ExternalApiAuthenticationHandler(
                authenticationOptions,
                NullLogger(
                    Of ExternalApiAuthenticationHandler).Instance)

        authenticationHandler.InnerHandler =
            recordingHandler

        Using client As New HttpClient(
            authenticationHandler)

            Dim response =
                Await client.GetAsync(
                    "https://example.test/orders")

            Assert.AreEqual(
                HttpStatusCode.OK,
                response.StatusCode)

            Assert.AreEqual(
                "TEST-API-KEY",
                recordingHandler.ApiKeyValue)

        End Using
    End Function

    <TestMethod>
    Public Async Function BearerMode_AddsAuthorizationHeader() _
        As Task

        Dim authenticationOptions =
            Options.Create(
                New ExternalApiAuthenticationOptions With {
                    .Mode =
                        ExternalApiAuthenticationMode.BearerToken,
                    .BearerToken =
                        "TEST-BEARER-TOKEN"
                })

        Dim recordingHandler =
            New RecordingHttpMessageHandler()

        Dim authenticationHandler =
            New ExternalApiAuthenticationHandler(
                authenticationOptions,
                NullLogger(
                    Of ExternalApiAuthenticationHandler).Instance)

        authenticationHandler.InnerHandler =
            recordingHandler

        Using client As New HttpClient(
            authenticationHandler)

            Dim response =
                Await client.GetAsync(
                    "https://example.test/orders")

            Assert.AreEqual(
                HttpStatusCode.OK,
                response.StatusCode)

            Assert.AreEqual(
                "Bearer",
                recordingHandler.AuthorizationScheme)

            Assert.AreEqual(
                "TEST-BEARER-TOKEN",
                recordingHandler.AuthorizationParameter)

        End Using
    End Function

    <TestMethod>
    Public Async Function ApiKeyMode_MissingKey_ThrowsException() _
        As Task

        Dim authenticationOptions =
            Options.Create(
                New ExternalApiAuthenticationOptions With {
                    .Mode =
                        ExternalApiAuthenticationMode.ApiKey,
                    .ApiKeyHeaderName =
                        "X-API-Key",
                    .ApiKey =
                        String.Empty
                })

        Dim recordingHandler =
            New RecordingHttpMessageHandler()

        Dim authenticationHandler =
            New ExternalApiAuthenticationHandler(
                authenticationOptions,
                NullLogger(
                    Of ExternalApiAuthenticationHandler).Instance)

        authenticationHandler.InnerHandler =
            recordingHandler

        Using client As New HttpClient(
            authenticationHandler)

            Dim authenticationFailureObserved As Boolean =
                False

            Try
                Await client.GetAsync(
                    "https://example.test/orders")

            Catch ex As ExternalApiAuthenticationException

                authenticationFailureObserved =
                    True

            End Try

            Assert.IsTrue(
                authenticationFailureObserved,
                "A missing required API key should " &
                "produce an authentication exception.")

            Assert.AreEqual(
                0,
                recordingHandler.RequestCount)

        End Using
    End Function

    Private NotInheritable Class RecordingHttpMessageHandler
        Inherits HttpMessageHandler

        Private _requestCount As Integer

        Public Property ApiKeyValue As String =
            String.Empty

        Public Property AuthorizationScheme As String =
            String.Empty

        Public Property AuthorizationParameter As String =
            String.Empty

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

            Interlocked.Increment(
                _requestCount)

            If request.Headers.Contains(
                "X-API-Key") Then

                ApiKeyValue =
                    request.Headers _
                        .GetValues(
                            "X-API-Key") _
                        .FirstOrDefault()

            End If

            If request.Headers.Authorization IsNot Nothing Then

                AuthorizationScheme =
                    request.Headers.Authorization.Scheme

                AuthorizationParameter =
                    If(
                        request.Headers.Authorization.Parameter,
                        String.Empty)

            End If

            Return Task.FromResult(
                New HttpResponseMessage(
                    HttpStatusCode.OK))
        End Function

    End Class

End Class

