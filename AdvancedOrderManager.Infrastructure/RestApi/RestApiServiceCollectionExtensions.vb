Option Explicit On
Option Strict On
Option Infer On

Imports System.Runtime.CompilerServices
Imports AdvancedOrderManager.Application
Imports Microsoft.Extensions.DependencyInjection
Imports Microsoft.Extensions.Http.Resilience

Public Module RestApiServiceCollectionExtensions

    <Extension>
    Public Function AddExternalRestApi(
        services As IServiceCollection,
        baseAddress As String,
        resilienceOptions As ExternalApiResilienceOptions) _
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

        If resilienceOptions Is Nothing Then
            Throw New ArgumentNullException(
                NameOf(resilienceOptions))
        End If

        ValidateResilienceOptions(
            resilienceOptions)

        Dim apiUri As Uri = Nothing

        If Not Uri.TryCreate(
            baseAddress,
            UriKind.Absolute,
            apiUri) Then

            Throw New ArgumentException(
                "The API base address is invalid.",
                NameOf(baseAddress))
        End If

        services.AddTransient(
    Of ExternalApiAuthenticationHandler)()

        Dim httpClientBuilder =
            services.AddHttpClient(
                Of IExternalPostService,
                   JsonPlaceholderPostService)(
                    Sub(client)

                        client.BaseAddress =
                            apiUri

                    End Sub)

        httpClientBuilder _
    .AddHttpMessageHandler(
        Of ExternalApiAuthenticationHandler)()

        httpClientBuilder _
            .AddStandardResilienceHandler(
                Sub(options)

                    options.Retry.MaxRetryAttempts =
                        resilienceOptions.MaxRetryAttempts

                    options.Retry.Delay =
                        TimeSpan.FromSeconds(
                            resilienceOptions.RetryDelaySeconds)

                    options.Retry _
                        .DisableForUnsafeHttpMethods()

                    options.AttemptTimeout.Timeout =
                        TimeSpan.FromSeconds(
                            resilienceOptions.AttemptTimeoutSeconds)

                    options.TotalRequestTimeout.Timeout =
                        TimeSpan.FromSeconds(
                            resilienceOptions.TotalTimeoutSeconds)

                End Sub)

        Return services
    End Function

    Private Sub ValidateResilienceOptions(
        options As ExternalApiResilienceOptions)

        If options.MaxRetryAttempts < 0 Then

            Throw New ArgumentOutOfRangeException(
                NameOf(options.MaxRetryAttempts),
                "Maximum retry attempts cannot be negative.")
        End If

        If options.RetryDelaySeconds < 0 Then

            Throw New ArgumentOutOfRangeException(
                NameOf(options.RetryDelaySeconds),
                "The retry delay cannot be negative.")
        End If

        If options.AttemptTimeoutSeconds <= 0 Then

            Throw New ArgumentOutOfRangeException(
                NameOf(options.AttemptTimeoutSeconds),
                "The attempt timeout must be greater than zero.")
        End If

        If options.TotalTimeoutSeconds <= 0 Then

            Throw New ArgumentOutOfRangeException(
                NameOf(options.TotalTimeoutSeconds),
                "The total timeout must be greater than zero.")
        End If

        If options.TotalTimeoutSeconds <
           options.AttemptTimeoutSeconds Then

            Throw New ArgumentException(
                "The total timeout cannot be shorter than " &
                "the timeout for one request attempt.")
        End If
    End Sub

End Module
