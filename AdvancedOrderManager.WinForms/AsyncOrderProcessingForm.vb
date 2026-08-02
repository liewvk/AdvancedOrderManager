Option Explicit On
Option Strict On
Option Infer On

Imports System.Threading
Imports AdvancedOrderManager.Application
Imports AdvancedOrderManager.Infrastructure
Imports Microsoft.Extensions.DependencyInjection
Imports Microsoft.Extensions.Logging
Imports Microsoft.Extensions.Logging.Abstractions

Public Class AsyncOrderProcessingForm

    Private _processingService As IAsyncOrderProcessingService
    Private _serviceProvider As IServiceProvider

    Private _logger As ILogger(
        Of AsyncOrderProcessingForm)

    Private _cancellationSource As CancellationTokenSource

    Public Sub New()

        InitializeComponent()

        InitialiseDependencies(
            New SimulatedOrderProcessingService(
                NullLogger(
                    Of SimulatedOrderProcessingService).Instance),
            NullLogger(
                Of AsyncOrderProcessingForm).Instance)
    End Sub

    Public Sub New(
        processingService As IAsyncOrderProcessingService,
        logger As ILogger(
            Of AsyncOrderProcessingForm))

        InitializeComponent()

        InitialiseDependencies(
            processingService,
            logger)
    End Sub

    Private Sub InitialiseDependencies(
        processingService As IAsyncOrderProcessingService,
        logger As ILogger(
            Of AsyncOrderProcessingForm))

        If processingService Is Nothing Then
            Throw New ArgumentNullException(
                NameOf(processingService))
        End If

        If logger Is Nothing Then
            Throw New ArgumentNullException(
                NameOf(logger))
        End If

        _processingService = processingService
        _logger = logger
    End Sub

    Private Async Sub btnStart_Click(
        sender As Object,
        e As EventArgs) _
        Handles btnStart.Click

        Dim orderCount =
            Decimal.ToInt32(
                nudOrderCount.Value)

        PrepareForProcessing()

        _cancellationSource =
            New CancellationTokenSource()

        Dim progress =
            New Progress(
                Of OrderProcessingProgress)(
                    Sub(update)
                        DisplayProgress(update)
                    End Sub)

        Try
            _logger.LogInformation(
                "The user started asynchronous " &
                "processing for {OrderCount} orders.",
                orderCount)

            Dim summary =
                Await _processingService.ProcessAsync(
                    orderCount,
                    progress,
                    _cancellationSource.Token)

            prgProcessing.Value = 100

            lblStatus.Text =
                "Processing completed successfully."

            AppendLog(
                $"Completed {summary.ProcessedOrders} " &
                $"orders in " &
                $"{summary.ElapsedTime.TotalSeconds:N2} seconds.")

            MessageBox.Show(
                Me,
                "All orders were processed successfully.",
                "Processing Completed",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information)

        Catch ex As OperationCanceledException

            lblStatus.Text =
                "Processing was cancelled."

            AppendLog(
                "The operation was cancelled by the user.")

            _logger.LogWarning(
                "The user cancelled asynchronous processing.")

        Catch ex As Exception

            lblStatus.Text =
                "Processing failed."

            AppendLog(
                $"Error: {ex.Message}")

            _logger.LogError(
                ex,
                "The asynchronous processing form " &
                "encountered an error.")

            MessageBox.Show(
                Me,
                ex.Message,
                "Processing Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)

        Finally
            FinishProcessing()
        End Try
    End Sub

    Private Sub btnCancel_Click(
        sender As Object,
        e As EventArgs) _
        Handles btnCancel.Click

        If _cancellationSource Is Nothing Then
            Return
        End If

        btnCancel.Enabled = False

        lblStatus.Text =
            "Cancellation requested..."

        AppendLog(
            "Waiting for the current operation to stop.")

        _cancellationSource.Cancel()
    End Sub

    Private Sub DisplayProgress(
        update As OrderProcessingProgress)

        prgProcessing.Value =
            Math.Min(
                prgProcessing.Maximum,
                update.Percentage)

        lblStatus.Text =
            update.Message

        AppendLog(
            $"{DateTime.Now:T} - {update.Message}")
    End Sub

    Private Sub PrepareForProcessing()

        btnStart.Enabled = False
        btnCancel.Enabled = True
        nudOrderCount.Enabled = False

        prgProcessing.Value = 0
        lblStatus.Text = "Starting..."

        txtLog.Clear()

        AppendLog(
            "Asynchronous order processing started.")
    End Sub

    Private Sub FinishProcessing()

        btnStart.Enabled = True
        btnCancel.Enabled = False
        nudOrderCount.Enabled = True

        If _cancellationSource IsNot Nothing Then
            _cancellationSource.Dispose()
            _cancellationSource = Nothing
        End If
    End Sub

    Private Sub AppendLog(
        message As String)

        txtLog.AppendText(
            message &
            Environment.NewLine)
    End Sub

    Private Sub AsyncOrderProcessingForm_FormClosing(
        sender As Object,
        e As FormClosingEventArgs) _
        Handles MyBase.FormClosing

        If _cancellationSource IsNot Nothing Then
            _cancellationSource.Cancel()
        End If
    End Sub

    Private Sub btnAsyncProcessing_Click(sender As Object, e As EventArgs) Handles btnAsyncProcessing.Click
        If _serviceProvider Is Nothing Then

            Using asyncForm As New AsyncOrderProcessingForm()

                asyncForm.ShowDialog(Me)
            End Using

            Return
        End If

        Using asyncForm = _serviceProvider.GetRequiredService(
            Of AsyncOrderProcessingForm)()

            asyncForm.ShowDialog(Me)
        End Using

    End Sub
End Class
