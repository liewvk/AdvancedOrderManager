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

        Catch ex As OptionsValidationException

            Dim failureText As String =
        String.Join(
            Environment.NewLine,
            ex.Failures)

            Global.System.Windows.Forms.MessageBox.Show(
        "The application configuration is invalid." &
        Environment.NewLine &
        Environment.NewLine &
        failureText,
        "Configuration Error",
        Global.System.Windows.Forms.MessageBoxButtons.OK,
        Global.System.Windows.Forms.MessageBoxIcon.Error)

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
    .AddOptions(
        Of ExternalApiAuthenticationOptions)() _
    .Bind(
        builder.Configuration.GetSection(
            ExternalApiAuthenticationOptions.SectionName)) _
    .Validate(
        Function(options As ExternalApiAuthenticationOptions)

            Select Case options.Mode

                Case ExternalApiAuthenticationMode.None

                    Return True

                Case ExternalApiAuthenticationMode.ApiKey

                    Return Not String.IsNullOrWhiteSpace(
                            options.ApiKeyHeaderName) AndAlso
                        Not String.IsNullOrWhiteSpace(
                            options.ApiKey)

                Case ExternalApiAuthenticationMode.BearerToken

                    Return Not String.IsNullOrWhiteSpace(
                            options.BearerToken)

                Case Else

                    Return False

            End Select

        End Function,
        "The external API authentication configuration " &
        "is incomplete or invalid.") _
    .ValidateOnStart()

        builder.Services _
    .AddOptions(
        Of OrderManagerOptions)() _
    .Bind(
        builder.Configuration.GetSection(
            OrderManagerOptions.SectionName)) _
    .Validate(
        Function(options As OrderManagerOptions)

            Return Not String.IsNullOrWhiteSpace(
                options.ApplicationTitle)

        End Function,
        "OrderManager:ApplicationTitle is required.") _
    .Validate(
        Function(options As OrderManagerOptions)

            Return options.DemonstrationTaxRate >= 0D AndAlso
                   options.DemonstrationTaxRate <= 1D

        End Function,
        "OrderManager:DemonstrationTaxRate must be " &
        "between 0 and 1.") _
    .Validate(
        Function(options As OrderManagerOptions)

            Return options.MinimumBulkQuantity > 0

        End Function,
        "OrderManager:MinimumBulkQuantity must be " &
        "greater than zero.") _
    .ValidateOnStart()


        '--------------------------------------------------
        ' Chapter 10
        ' Database configuration
        '--------------------------------------------------

        builder.Services _
    .AddOptions(
        Of OrderDatabaseOptions)() _
    .Bind(
        builder.Configuration.GetSection(
            OrderDatabaseOptions.SectionName)) _
    .Validate(
        Function(options As OrderDatabaseOptions)

            Return Not String.IsNullOrWhiteSpace(
                options.ConnectionString)

        End Function,
        "OrderDatabase:ConnectionString is required.") _
    .ValidateOnStart()


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
        ' Chapter 18
        ' Runtime performance diagnostics
        '--------------------------------------------------

        builder.Services _
    .AddSingleton(
        Of IRuntimePerformanceMonitor,
           RuntimePerformanceMonitor)()

        builder.Services _
    .AddTransient(
        Of PerformanceDiagnosticsForm)()

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



    End Sub

End Module