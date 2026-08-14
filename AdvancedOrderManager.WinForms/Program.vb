Option Explicit On
Option Strict On
Option Infer On

Imports AdvancedOrderManager.Application
Imports AdvancedOrderManager.Infrastructure
Imports Microsoft.Extensions.Configuration
Imports Microsoft.Extensions.DependencyInjection
Imports Microsoft.Extensions.Hosting
Imports Microsoft.Extensions.Logging
Imports Microsoft.Extensions.Options

Friend Module Program

    <STAThread>
    Public Sub Main()

        Global.System.Windows.Forms.Application.SetHighDpiMode(
            Global.System.Windows.Forms.HighDpiMode.SystemAware)

        Global.System.Windows.Forms.Application.EnableVisualStyles()

        Global.System.Windows.Forms.Application _
            .SetCompatibleTextRenderingDefault(False)

        Try
            RunApplication(
                Environment.GetCommandLineArgs())

        Catch ex As Exception

            Global.System.Windows.Forms.MessageBox.Show(
                "The application could not start." &
                Environment.NewLine &
                Environment.NewLine &
                ex.Message,
                "Application Startup Error",
                Global.System.Windows.Forms.MessageBoxButtons.OK,
                Global.System.Windows.Forms.MessageBoxIcon.Error)

        End Try

    End Sub

    Private Sub RunApplication(
        args As String())

        Dim builder As HostApplicationBuilder =
            Host.CreateApplicationBuilder(args)

        ConfigureServices(
            builder)

        Using host As IHost =
            builder.Build()

            host.Start()

            Dim mainForm As OrderProcessingEventForm =
                host.Services.GetRequiredService(
                    Of OrderProcessingEventForm)()

            Global.System.Windows.Forms.Application.Run(
                mainForm)

            host.StopAsync() _
                .GetAwaiter() _
                .GetResult()

        End Using

    End Sub

    Private Sub ConfigureServices(
        builder As HostApplicationBuilder)

        '--------------------------------------------------
        ' Chapter 7
        ' Main application configuration
        '--------------------------------------------------

        builder.Services _
            .Configure(
                Of OrderManagerOptions)(
                    builder.Configuration _
                        .GetSection(
                            OrderManagerOptions.SectionName))

        builder.Services _
            .AddSingleton(
                Function(provider)

                    Return provider _
                        .GetRequiredService(
                            Of IOptions(
                                Of OrderManagerOptions))() _
                        .Value

                End Function)

        '--------------------------------------------------
        ' Chapter 10
        ' Database configuration
        '--------------------------------------------------

        builder.Services _
            .Configure(
                Of OrderDatabaseOptions)(
                    builder.Configuration _
                        .GetSection(
                            OrderDatabaseOptions.SectionName))

        Dim databaseSection =
            builder.Configuration _
                .GetSection(
                    OrderDatabaseOptions.SectionName)

        Dim databaseConnectionString As String =
            databaseSection("ConnectionString")

        If String.IsNullOrWhiteSpace(
            databaseConnectionString) Then

            Throw New InvalidOperationException(
                "The database connection string " &
                "has not been configured.")

        End If

        '--------------------------------------------------
        ' Chapters 12 and 13
        ' REST API configuration
        '--------------------------------------------------

        builder.Services _
            .Configure(
                Of ExternalApiOptions)(
                    builder.Configuration _
                        .GetSection(
                            ExternalApiOptions.SectionName))

        builder.Services _
            .Configure(
                Of ExternalApiResilienceOptions)(
                    builder.Configuration _
                        .GetSection(
                            ExternalApiResilienceOptions.SectionName))

        builder.Services _
    .Configure(
        Of ExternalApiAuthenticationOptions)(
            builder.Configuration _
                .GetSection(
                    ExternalApiAuthenticationOptions.SectionName))

        Dim externalApiSection =
            builder.Configuration _
                .GetSection(
                    ExternalApiOptions.SectionName)

        Dim externalApiBaseAddress As String =
            externalApiSection("BaseAddress")

        If String.IsNullOrWhiteSpace(
            externalApiBaseAddress) Then

            Throw New InvalidOperationException(
                "The external API base address " &
                "has not been configured.")

        End If

        Dim resilienceSection =
            builder.Configuration _
                .GetSection(
                    ExternalApiResilienceOptions.SectionName)

        Dim resilienceOptions As New ExternalApiResilienceOptions()

        resilienceSection.Bind(
            resilienceOptions)
        '--------------------------------------------------
        ' Chapter 15
        ' Application input validation
        '--------------------------------------------------

        builder.Services _
    .AddSingleton(
        Of IInputValidator(
            Of CreateExternalPostRequest),
           CreateExternalPostRequestValidator)()
        builder.Services _
    .AddTransient(
        Of IExternalPostApplicationService,
           ExternalPostApplicationService)()

        '--------------------------------------------------
        ' Chapter 5-7
        ' Reporting and pricing services
        '--------------------------------------------------

        builder.Services _
            .AddSingleton(
                Of OrderReportStore)()

        builder.Services _
            .AddSingleton(
                Of OrderPricingService)()

        builder.Services _
            .AddSingleton(
                Of IOrderReportExporter,
                   OrderReportExporter)()

        '--------------------------------------------------
        ' Chapter 8
        ' Asynchronous processing
        '--------------------------------------------------

        builder.Services _
            .AddSingleton(
                Of IAsyncOrderProcessingService,
                   SimulatedOrderProcessingService)()

        '--------------------------------------------------
        ' Chapter 9
        ' Concurrent processing
        '--------------------------------------------------

        builder.Services _
            .AddSingleton(
                Of IConcurrentOrderProcessingService,
                   ConcurrentOrderProcessingService)()

        '--------------------------------------------------
        ' Chapter 11
        ' Entity Framework Core
        '--------------------------------------------------

        builder.Services _
            .AddOrderEntityFramework(
                databaseConnectionString)

        'Do not register SqlOrderDataRepository here.
        'AddOrderEntityFramework now registers:
        '
        'IOrderDataRepository
        '    -> EfOrderDataRepository
        '
        'IOrderHistoryQueryService
        '    -> EfOrderHistoryQueryService

        '--------------------------------------------------
        ' Chapters 12 and 13
        ' REST API and resilience
        '--------------------------------------------------

        builder.Services _
            .AddExternalRestApi(
                externalApiBaseAddress,
                resilienceOptions)

        'AddExternalRestApi registers:
        '
        'IExternalPostService
        '    -> JsonPlaceholderPostService
        '
        'together with the configured HttpClient and
        'Chapter 13 resilience pipeline.

        '--------------------------------------------------
        ' Windows Forms
        '--------------------------------------------------

        builder.Services _
            .AddTransient(
                Of OrderProcessingEventForm)()

        builder.Services _
            .AddTransient(
                Of OrderReportForm)()

        builder.Services _
            .AddTransient(
                Of AsyncOrderProcessingForm)()

        builder.Services _
            .AddTransient(
                Of ConcurrentOrderProcessingForm)()

        builder.Services _
            .AddTransient(
                Of OrderDatabaseForm)()

        builder.Services _
            .AddTransient(
                Of EntityFrameworkQueryForm)()

        builder.Services _
            .AddTransient(
                Of RestApiForm)()

        '--------------------------------------------------
        ' Hosted services and logging
        '--------------------------------------------------

        builder.Services _
            .AddHostedService(
                Of ApplicationStartupReporter)()

        builder.Logging _
            .SetMinimumLevel(
                LogLevel.Information)

    End Sub

End Module