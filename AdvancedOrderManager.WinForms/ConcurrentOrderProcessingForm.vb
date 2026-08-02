Option Explicit On
Option Strict On
Option Infer On

Imports System.Threading
Imports AdvancedOrderManager.Application
Imports AdvancedOrderManager.Infrastructure
Imports Microsoft.Extensions.Logging
Imports Microsoft.Extensions.Logging.Abstractions

Public Class ConcurrentOrderProcessingForm

    Private _processingService As IConcurrentOrderProcessingService

    Private _logger As ILogger(
        Of ConcurrentOrderProcessingForm)

    Private _cancellationSource As CancellationTokenSource

    Private _isProcessing As Boolean

    Public Sub New()

        InitializeComponent()

        InitialiseDependencies(
            New ConcurrentOrderProcessingService(
                NullLogger(
                    Of ConcurrentOrderProcessingService).Instance),
            NullLogger(
                Of ConcurrentOrderProcessingForm).Instance)
    End Sub

    Public Sub New(
        processingService As IConcurrentOrderProcessingService,
        logger As ILogger(Of ConcurrentOrderProcessingForm))

        InitializeComponent()

        InitialiseDependencies(
            processingService,
            logger)
    End Sub

    Private Sub InitialiseDependencies(
        processingService As IConcurrentOrderProcessingService,
        logger As ILogger(Of ConcurrentOrderProcessingForm))

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

        Dim orderCount As Integer =
            Decimal.ToInt32(
                nudOrderCount.Value)

        Dim maximumConcurrency As Integer =
            Decimal.ToInt32(
                nudMaximumConcurrency.Value)

        If maximumConcurrency > orderCount Then

            MessageBox.Show(
                Me,
                "Maximum concurrency cannot exceed " &
                "the number of orders.",
                "Invalid Concurrency",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning)

            nudMaximumConcurrency.Focus()
            Return
        End If

        PrepareForProcessing()

        _cancellationSource =
            New CancellationTokenSource()

        Dim progress =
            New Progress(Of ConcurrentBatchProgress)(
                AddressOf DisplayProgress)

        Try
            _logger.LogInformation(
                "The user started concurrent processing " &
                "for {OrderCount} orders with maximum " &
                "concurrency {MaximumConcurrency}.",
                orderCount,
                maximumConcurrency)

            Dim summary As ConcurrentBatchSummary =
                Await _processingService.ProcessBatchAsync(
                    orderCount,
                    maximumConcurrency,
                    progress,
                    _cancellationSource.Token)

            prgProcessing.Value = 100

            lblStatus.Text =
                "Concurrent processing completed."

            lblActiveOperations.Text =
                "Active operations: 0"

            AppendLog(
                $"Completed orders: {summary.CompletedOrders}")

            AppendLog(
                $"Failed orders: {summary.FailedOrders}")

            AppendLog(
                $"Peak concurrency: {summary.PeakConcurrency}")

            AppendLog(
                $"Elapsed time: " &
                $"{summary.ElapsedTime.TotalSeconds:N2} seconds")

            MessageBox.Show(
                Me,
                $"Processed {summary.CompletedOrders} orders." &
                Environment.NewLine &
                $"Peak concurrency: {summary.PeakConcurrency}" &
                Environment.NewLine &
                $"Elapsed time: " &
                $"{summary.ElapsedTime.TotalSeconds:N2} seconds",
                "Concurrent Processing Completed",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information)

        Catch ex As OperationCanceledException

            lblStatus.Text =
                "Concurrent processing was cancelled."

            lblActiveOperations.Text =
                "Active operations: 0"

            AppendLog(
                "The batch was cancelled by the user.")

            _logger.LogWarning(
                "The user cancelled concurrent processing.")

        Catch ex As Exception

            lblStatus.Text =
                "Concurrent processing failed."

            AppendLog(
                $"Error: {ex.Message}")

            _logger.LogError(
                ex,
                "The concurrent processing form " &
                "encountered an error.")

            MessageBox.Show(
                Me,
                ex.Message,
                "Concurrent Processing Error",
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
            "Waiting for active operations to stop.")

        _cancellationSource.Cancel()
    End Sub

    Private Sub DisplayProgress(
        update As ConcurrentBatchProgress)

        prgProcessing.Value =
            Math.Min(
                prgProcessing.Maximum,
                update.Percentage)

        lblStatus.Text =
            update.Message

        lblActiveOperations.Text =
            $"Active operations: {update.ActiveOperations}"

        AppendLog(
            $"{DateTime.Now:T} - " &
            $"{update.Message} " &
            $"Active: {update.ActiveOperations}")
    End Sub

    Private Sub PrepareForProcessing()

        _isProcessing = True

        btnStart.Enabled = False
        btnCancel.Enabled = True

        nudOrderCount.Enabled = False
        nudMaximumConcurrency.Enabled = False

        prgProcessing.Value = 0

        lblStatus.Text =
            "Starting concurrent processing..."

        lblActiveOperations.Text =
            "Active operations: 0"

        txtLog.Clear()

        AppendLog(
            "Concurrent batch processing started.")
    End Sub

    Private Sub FinishProcessing()

        _isProcessing = False

        btnStart.Enabled = True
        btnCancel.Enabled = False

        nudOrderCount.Enabled = True
        nudMaximumConcurrency.Enabled = True

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

    Private Sub ConcurrentOrderProcessingForm_FormClosing(
        sender As Object,
        e As FormClosingEventArgs) _
        Handles MyBase.FormClosing

        If Not _isProcessing Then
            Return
        End If

        Dim result As DialogResult =
            MessageBox.Show(
                Me,
                "Processing is still running. " &
                "Do you want to request cancellation?",
                "Processing in Progress",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)

        If result = DialogResult.Yes Then

            If _cancellationSource IsNot Nothing Then
                _cancellationSource.Cancel()
            End If

            e.Cancel = True

            lblStatus.Text =
                "Cancellation requested. " &
                "Close the form after processing stops."

        Else
            e.Cancel = True
        End If
    End Sub

End Class
