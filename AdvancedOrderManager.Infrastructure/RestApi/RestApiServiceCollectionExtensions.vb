Option Explicit On
Option Strict On
Option Infer On

Imports System.Runtime.CompilerServices
Imports AdvancedOrderManager.Application
Imports Microsoft.Extensions.DependencyInjection

Public Module RestApiServiceCollectionExtensions

    <Extension>
    Public Function AddExternalRestApi(
        services As IServiceCollection,
        baseAddress As String,
        timeoutSeconds As Integer) _
        As IServiceCollection

        If services Is Nothing Then
            Throw New ArgumentNullException(
                NameOf(services))
        End If

        If String.IsNullOrWhiteSpace(baseAddress) Then
            Throw New ArgumentException(
                "An API base address is required.",
                NameOf(baseAddress))
        End If

        If timeoutSeconds <= 0 Then
            Throw New ArgumentOutOfRangeException(
                NameOf(timeoutSeconds))
        End If

        Dim apiUri As Uri = Nothing

        If Not Uri.TryCreate(
            baseAddress,
            UriKind.Absolute,
            apiUri) Then

            Throw New ArgumentException(
                "The API base address is invalid.",
                NameOf(baseAddress))
        End If

        services.AddHttpClient(
            Of IExternalPostService,
               JsonPlaceholderPostService)(
                Sub(client)

                    client.BaseAddress =
                        apiUri

                    client.Timeout =
                        TimeSpan.FromSeconds(
                            timeoutSeconds)

                End Sub)

        Return services
    End Function

End Module

