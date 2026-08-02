Option Explicit On
Option Strict On
Option Infer On

Imports AdvancedOrderManager.Application
Imports AdvancedOrderManager.Infrastructure
Imports Microsoft.Extensions.DependencyInjection
Imports Microsoft.Extensions.Logging
Imports Microsoft.Extensions.Logging.Abstractions
Imports Microsoft.Extensions.Options

Public Class OrderProcessingEventForm

    Private _reportStore As OrderReportStore

    Private _pricingService As OrderPricingService

    Private _exporter As IOrderReportExporter

    Private _logger As ILogger(Of OrderProcessingEventForm)

    Private _options As OrderManagerOptions

    Private _processor As OrderProcessor

    Private _serviceProvider As IServiceProvider

    Private ReadOnly _statistics As New OrderProcessingStatistics()

    Private ReadOnly _audit As New OrderAuditSubscriber()

    Private _auditSubscribed As Boolean


    'This constructor is required by the Windows Forms Designer.
    Public Sub New()

        InitializeComponent()

        _serviceProvider = Nothing

        Dim defaultOptions =
            New OrderManagerOptions()

        InitialiseDependencies(
            New OrderReportStore(),
            New OrderPricingService(
                defaultOptions),
            New OrderReportExporter(
                NullLogger(
                    Of OrderReportExporter).Instance),
            NullLogger(
                Of OrderProcessingEventForm).Instance,
            defaultOptions)
    End Sub

    'The dependency-injection container uses this constructor
    'when the application starts through Program.Main.
    Public Sub New(
        reportStore As OrderReportStore,
    pricingService As OrderPricingService,
    exporter As IOrderReportExporter,
    logger As ILogger(
        Of OrderProcessingEventForm),
    options As IOptions(
        Of OrderManagerOptions),
    serviceProvider As IServiceProvider)

        InitializeComponent()

        If options Is Nothing Then

            Throw New ArgumentNullException(
                NameOf(options))
        End If

        _serviceProvider = serviceProvider

        InitialiseDependencies(
            reportStore,
            pricingService,
            exporter,
            logger,
            options.Value)

    End Sub

    Private Sub InitialiseDependencies(
        reportStore As OrderReportStore,
        pricingService As OrderPricingService,
        exporter As IOrderReportExporter,
        logger As ILogger(
            Of OrderProcessingEventForm),
        options As OrderManagerOptions)

        If reportStore Is Nothing Then

            Throw New ArgumentNullException(
                NameOf(reportStore))
        End If

        If pricingService Is Nothing Then

            Throw New ArgumentNullException(
                NameOf(pricingService))
        End If

        If exporter Is Nothing Then

            Throw New ArgumentNullException(
                NameOf(exporter))
        End If

        If logger Is Nothing Then

            Throw New ArgumentNullException(
                NameOf(logger))
        End If

        If options Is Nothing Then

            Throw New ArgumentNullException(
                NameOf(options))
        End If

        _reportStore = reportStore
        _pricingService = pricingService
        _exporter = exporter
        _logger = logger
        _options = options

        _processor =
            New OrderProcessor(
                AddressOf ValidateOrder,
                AddressOf CalculateOrderTotal)

        SubscribeToProcessorEvents()
    End Sub

    Private Sub SubscribeToProcessorEvents()

        AddHandler _processor.OrderProcessed,
            AddressOf _statistics.HandleOrderProcessed

        AddHandler _processor.OrderRejected,
            AddressOf _statistics.HandleOrderRejected

        AddHandler _processor.OrderProcessed,
            AddressOf _reportStore.HandleOrderProcessed

        AddHandler _processor.OrderRejected,
            AddressOf _reportStore.HandleOrderRejected

        AddHandler _processor.OrderProcessed,
            AddressOf HandleOrderProcessed

        AddHandler _processor.OrderRejected,
            AddressOf HandleOrderRejected
    End Sub

    Private Sub UnsubscribeFromProcessorEvents()

        If _processor Is Nothing Then
            Return
        End If

        RemoveHandler _processor.OrderProcessed,
            AddressOf _statistics.HandleOrderProcessed

        RemoveHandler _processor.OrderRejected,
            AddressOf _statistics.HandleOrderRejected

        RemoveHandler _processor.OrderProcessed,
            AddressOf _reportStore.HandleOrderProcessed

        RemoveHandler _processor.OrderRejected,
            AddressOf _reportStore.HandleOrderRejected

        RemoveHandler _processor.OrderProcessed,
            AddressOf HandleOrderProcessed

        RemoveHandler _processor.OrderRejected,
            AddressOf HandleOrderRejected

        If _auditSubscribed Then

            RemoveHandler _processor.OrderProcessed,
                AddressOf _audit.HandleOrderProcessed

            RemoveHandler _processor.OrderRejected,
                AddressOf _audit.HandleOrderRejected

            _auditSubscribed = False
        End If
    End Sub

    Public Sub New(
    reportStore As OrderReportStore,
    pricingService As OrderPricingService,
    exporter As IOrderReportExporter,
    logger As ILogger(
        Of OrderProcessingEventForm),
    options As IOptions(
        Of OrderManagerOptions))

        InitializeComponent()

        If options Is Nothing Then

            Throw New ArgumentNullException(
            NameOf(options))
        End If

        InitialiseDependencies(
        reportStore,
        pricingService,
        exporter,
        logger,
        options.Value)
    End Sub

    Private Sub btnConcurrentProcessing_Click(
    sender As Object,
    e As EventArgs) _
    Handles btnConcurrentProcessing.Click

        If _serviceProvider Is Nothing Then

            Using concurrentForm As New ConcurrentOrderProcessingForm()

                concurrentForm.ShowDialog(Me)
            End Using

            Return
        End If

        Using concurrentForm As ConcurrentOrderProcessingForm =
        _serviceProvider.GetRequiredService(
            Of ConcurrentOrderProcessingForm)()

            concurrentForm.ShowDialog(Me)
        End Using

    End Sub


    Private Sub OrderProcessingEventForm_Load(
        sender As Object,
        e As EventArgs) _
        Handles MyBase.Load

        Me.Text =
            _options.ApplicationTitle

        chkEnableAudit.Checked =
            _options.EnableAuditByDefault

        chkApplyTax.Text =
            $"Apply " &
            $"{_options.DemonstrationTaxRate:P0} " &
            $"Demonstration Tax"

        UpdateAuditSubscription()
        UpdateStatisticsDisplay()

        lblProcessingStatus.Text =
            "Ready to process orders."

        _logger.LogInformation(
            "Order processing form opened.")
        Me.Text =
    _options.ApplicationTitle

        chkEnableAudit.Checked =
    _options.EnableAuditByDefault

        chkApplyTax.Text =
    $"Apply " &
    $"{_options.DemonstrationTaxRate:P0} " &
    $"Demonstration Tax"
        _logger.LogInformation(
    "Order processing form opened.")



    End Sub

    Private Shared Function ValidateOrder(
        order As OrderSubmission) As String

        If order Is Nothing Then
            Return "An order submission is required."
        End If

        If String.IsNullOrWhiteSpace(
            order.OrderNumber) Then

            Return "Order number is required."
        End If

        If String.IsNullOrWhiteSpace(
            order.CustomerName) Then

            Return "Customer name is required."
        End If

        If order.Quantity <= 0 Then
            Return "Quantity must be greater than zero."
        End If

        If order.UnitPrice <= 0D Then
            Return "Unit price must be greater than zero."
        End If

        Return String.Empty
    End Function

    Private Function CalculateOrderTotal(
        order As OrderSubmission) As Decimal

        If order Is Nothing Then

            Throw New ArgumentNullException(
                NameOf(order))
        End If

        Return _pricingService.CalculateTotal(
            order,
            chkApplyTax.Checked)
    End Function

    Private Sub btnProcessOrder_Click(
        sender As Object,
        e As EventArgs) _
        Handles btnProcessOrder.Click

        Dim orderNumber =
            txtOrderNumber.Text.Trim()

        Dim customerName =
            txtCustomerName.Text.Trim()

        Try
            Dim submission =
                New OrderSubmission(
                    orderNumber,
                    customerName,
                    Decimal.ToInt32(
                        nudQuantity.Value),
                    nudUnitPrice.Value,
                    chkPriority.Checked)

            _logger.LogInformation(
                "Processing order {OrderNumber} " &
                "for customer {CustomerName}.",
                submission.OrderNumber,
                submission.CustomerName)

            Dim processed =
                _processor.Process(
                    submission)

            If processed Then

                _logger.LogInformation(
                    "Order {OrderNumber} was " &
                    "processed successfully.",
                    submission.OrderNumber)

                ClearOrderEntry()

            Else
                _logger.LogWarning(
                    "Order {OrderNumber} was rejected.",
                    submission.OrderNumber)
            End If

        Catch ex As Exception

            _logger.LogError(
                ex,
                "An unexpected error occurred while " &
                "processing order {OrderNumber}.",
                orderNumber)

            lblProcessingStatus.Text =
                "The order could not be processed."

            MessageBox.Show(
                Me,
                ex.Message,
                "Order Processing",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub HandleOrderProcessed(
        sender As Object,
        e As OrderProcessedEventArgs)

        If e Is Nothing Then
            Return
        End If

        Dim priorityText =
            If(
                e.IsPriority,
                " Priority",
                String.Empty)

        lstOrderActivity.Items.Insert(
            0,
            $"{e.ProcessedAtUtc.ToLocalTime():HH:mm:ss} " &
            $"Processed {e.OrderNumber} for " &
            $"{e.CustomerName}.{priorityText} " &
            $"Total: {_options.CurrencySymbol}" &
            $"{e.TotalAmount:N2}")

        lblProcessingStatus.Text =
            $"Order {e.OrderNumber} processed successfully."

        UpdateStatisticsDisplay()
    End Sub

    Private Sub HandleOrderRejected(
        sender As Object,
        e As OrderRejectedEventArgs)

        If e Is Nothing Then
            Return
        End If

        lstOrderActivity.Items.Insert(
            0,
            $"{e.RejectedAtUtc.ToLocalTime():HH:mm:ss} " &
            $"Rejected {e.OrderNumber}: {e.Reason}")

        lblProcessingStatus.Text =
            $"Order rejected: {e.Reason}"

        UpdateStatisticsDisplay()
    End Sub

    Private Sub UpdateStatisticsDisplay()

        lblProcessedCount.Text =
            $"Processed: {_statistics.ProcessedCount:N0}"

        lblRejectedCount.Text =
            $"Rejected: {_statistics.RejectedCount:N0}"

        lblTotalRevenue.Text =
            $"Revenue: {_options.CurrencySymbol}" &
            $"{_statistics.TotalRevenue:N2}"
    End Sub

    Private Sub chkEnableAudit_CheckedChanged(
        sender As Object,
        e As EventArgs) _
        Handles chkEnableAudit.CheckedChanged

        UpdateAuditSubscription()
    End Sub

    Private Sub UpdateAuditSubscription()

        If _processor Is Nothing Then
            Return
        End If

        If chkEnableAudit.Checked AndAlso
           Not _auditSubscribed Then

            AddHandler _processor.OrderProcessed,
                AddressOf _audit.HandleOrderProcessed

            AddHandler _processor.OrderRejected,
                AddressOf _audit.HandleOrderRejected

            _auditSubscribed = True

            lblProcessingStatus.Text =
                "Audit monitoring enabled."

        ElseIf Not chkEnableAudit.Checked AndAlso
               _auditSubscribed Then

            RemoveHandler _processor.OrderProcessed,
                AddressOf _audit.HandleOrderProcessed

            RemoveHandler _processor.OrderRejected,
                AddressOf _audit.HandleOrderRejected

            _auditSubscribed = False

            lblProcessingStatus.Text =
                "Audit monitoring disabled."
        End If
    End Sub

    Private Sub btnClearActivity_Click(
        sender As Object,
        e As EventArgs) _
        Handles btnClearActivity.Click

        lstOrderActivity.Items.Clear()

        lblProcessingStatus.Text =
            "Order activity cleared."
    End Sub



    Private Sub ClearOrderEntry()

        txtOrderNumber.Clear()
        txtCustomerName.Clear()

        If nudQuantity.Minimum <= 1D AndAlso
           nudQuantity.Maximum >= 1D Then

            nudQuantity.Value = 1D
        End If

        If nudUnitPrice.Minimum <= 0.01D AndAlso
           nudUnitPrice.Maximum >= 0.01D Then

            nudUnitPrice.Value = 0.01D
        End If

        chkPriority.Checked = False

        txtOrderNumber.Focus()
    End Sub

    Private Sub OrderProcessingEventForm_FormClosed(
        sender As Object,
        e As FormClosedEventArgs) _
        Handles MyBase.FormClosed

        UnsubscribeFromProcessorEvents()

        _logger.LogInformation(
            "Order processing form closed.")


    End Sub
    Private Sub btnOpenReport_Click(
        sender As Object,
        e As EventArgs) _
        Handles btnOpenReport.Click

        If _serviceProvider Is Nothing Then

            Using reportForm As New OrderReportForm(
                _reportStore,
                _exporter,
                NullLogger(
                    Of OrderReportForm).Instance)

                reportForm.ShowDialog(Me)
            End Using

            Return
        End If

        Using reportForm =
            _serviceProvider _
                .GetRequiredService(
                    Of OrderReportForm)()

            reportForm.ShowDialog(Me)
        End Using
    End Sub
End Class