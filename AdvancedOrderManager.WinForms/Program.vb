Option Explicit On
Option Strict On
Option Infer On

Imports AdvancedOrderManager.Application
Imports AdvancedOrderManager.Infrastructure
Imports Microsoft.Extensions.DependencyInjection
Imports Microsoft.Extensions.Hosting
Imports Microsoft.Extensions.Logging
Imports Microsoft.Extensions.Options

Friend Module Program

    <STAThread>
    Public Sub Main(
        args As String())

        Global.System.Windows.Forms.Application.SetHighDpiMode(
            Global.System.Windows.Forms.HighDpiMode.SystemAware)

        Global.System.Windows.Forms.Application.EnableVisualStyles()

        Global.System.Windows.Forms.Application _
            .SetCompatibleTextRenderingDefault(
                False)

        Try
            RunApplication(
                args)

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

        Dim builder =
            Host.CreateApplicationBuilder(
                args)

        ConfigureServices(
            builder)

        Using host =
            builder.Build()

            host.Start()

            Dim mainForm =
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

        builder.Services _
            .Configure(
                Of OrderManagerOptions)(
                    builder.Configuration _
                        .GetSection(
                            OrderManagerOptions.SectionName))

        Dim databaseSection =
    builder.Configuration _
        .GetSection(
            OrderDatabaseOptions.SectionName)

        Dim databaseConnectionString As String =
    databaseSection("ConnectionString")

        builder.Services _
    .AddOrderEntityFramework(
        databaseConnectionString)

        builder.Services _
            .AddSingleton(
                Function(provider)

                    Return provider _
                        .GetRequiredService(
                            Of IOptions(
                                Of OrderManagerOptions))() _
                        .Value
                End Function)

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

        builder.Services _
            .AddTransient(
                Of OrderProcessingEventForm)()

        builder.Services _
            .AddTransient(
                Of OrderReportForm)()

        builder.Services _
            .AddHostedService(
                Of ApplicationStartupReporter)()

        builder.Logging.SetMinimumLevel(
            LogLevel.Information)
        builder.Services _
    .AddSingleton(
        Of IAsyncOrderProcessingService,
           SimulatedOrderProcessingService)()

        builder.Services _
    .AddTransient(
        Of AsyncOrderProcessingForm)()

        builder.Services _
    .AddSingleton(
        Of IConcurrentOrderProcessingService,
           ConcurrentOrderProcessingService)()

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
    .Configure(
        Of ExternalApiOptions)(
            builder.Configuration _
                .GetSection(
                    ExternalApiOptions.SectionName))

        Dim externalApiSection =
    builder.Configuration _
        .GetSection(
            ExternalApiOptions.SectionName)

        Dim externalApiBaseAddress As String =
    externalApiSection("BaseAddress")

        Dim timeoutText As String =
    externalApiSection("TimeoutSeconds")

        Dim externalApiTimeoutSeconds As Integer

        If Not Integer.TryParse(
    timeoutText,
    externalApiTimeoutSeconds) Then

            externalApiTimeoutSeconds = 15
        End If
        builder.Services _
    .AddExternalRestApi(
        externalApiBaseAddress,
        externalApiTimeoutSeconds)
        builder.Services _
    .AddTransient(
        Of RestApiForm)()


    End Sub

End Module

