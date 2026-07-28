Option Explicit On
Option Strict On
Option Infer On

Imports AdvancedOrderManager.Application
Imports AdvancedOrderManager.Infrastructure

Public Class OrderProcessingEventForm

    Private ReadOnly _processor As OrderProcessor

    Private ReadOnly _statistics As New OrderProcessingStatistics()

    Private ReadOnly _audit As New OrderAuditSubscriber()
    Private ReadOnly _reportStore As New OrderReportStore()
    Private ReadOnly _pricingService As New OrderPricingService()

    Private _auditSubscribed As Boolean

    Public Sub New()

        InitializeComponent()

        Dim validator As Func(Of OrderSubmission, String) =
                AddressOf ValidateOrder

        Dim totalCalculator As Func(Of OrderSubmission, Decimal) =
                AddressOf CalculateOrderTotal

        _processor =
            New OrderProcessor(
                validator,
                totalCalculator)

        AddHandler _processor.OrderProcessed,
    AddressOf _reportStore.HandleOrderProcessed

        AddHandler _processor.OrderRejected,
    AddressOf _reportStore.HandleOrderRejected

        AddHandler _processor.OrderProcessed,
            AddressOf _statistics.HandleOrderProcessed

        AddHandler _processor.OrderRejected,
            AddressOf _statistics.HandleOrderRejected

        AddHandler _processor.OrderProcessed,
            AddressOf HandleOrderProcessed

        AddHandler _processor.OrderRejected,
            AddressOf HandleOrderRejected
    End Sub

    Private Sub OrderProcessingEventForm_Load(
        sender As Object,
        e As EventArgs) _
        Handles MyBase.Load

        lblProcessingStatus.Text = "Ready"

        lblProcessingStatus.ForeColor =
            SystemColors.ControlText

        chkEnableAudit.Checked = False

        UpdateStatistics()

        txtOrderNumber.Focus()
    End Sub

    Private Function ValidateOrder(
        order As OrderSubmission) As String

        If order Is Nothing Then

            Throw New ArgumentNullException(
                NameOf(order))
        End If

        If String.IsNullOrWhiteSpace(
            order.OrderNumber) Then

            Return "Enter an order number."
        End If

        If order.OrderNumber.Length > 30 Then

            Return "The order number cannot exceed " &
                   "30 characters."
        End If

        If String.IsNullOrWhiteSpace(
            order.CustomerName) Then

            Return "Enter the customer name."
        End If

        If order.CustomerName.Length > 100 Then

            Return "The customer name cannot exceed " &
                   "100 characters."
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

        Return _pricingService.CalculateTotal(
        order,
        chkApplyTax.Checked)
    End Function

    Private Sub btnProcessOrder_Click(
        sender As Object,
        e As EventArgs) _
        Handles btnProcessOrder.Click

        Try
            Dim order =
                New OrderSubmission(
                    txtOrderNumber.Text,
                    txtCustomerName.Text,
                    Decimal.ToInt32(
                        nudQuantity.Value),
                    nudUnitPrice.Value,
                    chkPriority.Checked)

            Dim succeeded =
                _processor.Process(
                    order)

            UpdateStatistics()

            If succeeded Then
                ClearOrderEntry()
            End If

        Catch ex As Exception

            lblProcessingStatus.Text =
                "The order could not be processed."

            lblProcessingStatus.ForeColor =
                Color.DarkRed

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
                "Priority",
                "Standard")

        lstOrderActivity.Items.Insert(
            0,
            $"Processed {e.OrderNumber} | " &
            $"{e.CustomerName} | " &
            $"{priorityText} | " &
            $"RM{e.TotalAmount:N2}")

        lblProcessingStatus.Text =
            $"Order {e.OrderNumber} was processed."

        lblProcessingStatus.ForeColor =
            Color.DarkGreen
    End Sub

    Private Sub HandleOrderRejected(
        sender As Object,
        e As OrderRejectedEventArgs)

        If e Is Nothing Then
            Return
        End If

        Dim displayedOrderNumber =
            If(
                String.IsNullOrWhiteSpace(
                    e.OrderNumber),
                "(no order number)",
                e.OrderNumber)

        lstOrderActivity.Items.Insert(
            0,
            $"Rejected {displayedOrderNumber} | " &
            $"{e.Reason}")

        lblProcessingStatus.Text =
            e.Reason

        lblProcessingStatus.ForeColor =
            Color.DarkRed
    End Sub

    Private Sub UpdateStatistics()

        lblProcessedCount.Text =
            $"Processed: {_statistics.ProcessedCount:N0}"

        lblRejectedCount.Text =
            $"Rejected: {_statistics.RejectedCount:N0}"

        lblTotalRevenue.Text =
            $"Revenue: RM{_statistics.TotalRevenue:N2}"
    End Sub

    Private Sub chkEnableAudit_CheckedChanged(
        sender As Object,
        e As EventArgs) _
        Handles chkEnableAudit.CheckedChanged

        If chkEnableAudit.Checked AndAlso
           Not _auditSubscribed Then

            AddHandler _processor.OrderProcessed,
                AddressOf _audit.HandleOrderProcessed

            AddHandler _processor.OrderRejected,
                AddressOf _audit.HandleOrderRejected

            _auditSubscribed = True

            lblProcessingStatus.Text =
                "Audit subscriber enabled."

            lblProcessingStatus.ForeColor =
                Color.DarkBlue

        ElseIf Not chkEnableAudit.Checked AndAlso
               _auditSubscribed Then

            RemoveHandler _processor.OrderProcessed,
                AddressOf _audit.HandleOrderProcessed

            RemoveHandler _processor.OrderRejected,
                AddressOf _audit.HandleOrderRejected

            _auditSubscribed = False

            lblProcessingStatus.Text =
                "Audit subscriber disabled."

            lblProcessingStatus.ForeColor =
                SystemColors.ControlText
        End If
    End Sub

    Private Sub ClearOrderEntry()

        txtOrderNumber.Clear()
        txtCustomerName.Clear()

        nudQuantity.Value =
            nudQuantity.Minimum

        nudUnitPrice.Value =
            nudUnitPrice.Minimum

        chkPriority.Checked = False

        txtOrderNumber.Focus()
    End Sub

    Private Sub btnClearActivity_Click(
        sender As Object,
        e As EventArgs) _
        Handles btnClearActivity.Click

        lstOrderActivity.Items.Clear()

        lblProcessingStatus.Text =
            "Activity display cleared."

        lblProcessingStatus.ForeColor =
            SystemColors.ControlText
    End Sub

    Private Sub OrderProcessingEventForm_FormClosed(
        sender As Object,
        e As FormClosedEventArgs) _
        Handles MyBase.FormClosed

        RemoveHandler _processor.OrderProcessed,
            AddressOf _statistics.HandleOrderProcessed

        RemoveHandler _processor.OrderRejected,
            AddressOf _statistics.HandleOrderRejected

        RemoveHandler _processor.OrderProcessed,
            AddressOf HandleOrderProcessed

        RemoveHandler _processor.OrderRejected,
            AddressOf HandleOrderRejected
        RemoveHandler _processor.OrderProcessed,
    AddressOf _reportStore.HandleOrderProcessed

        RemoveHandler _processor.OrderRejected,
    AddressOf _reportStore.HandleOrderRejected


        If _auditSubscribed Then

            RemoveHandler _processor.OrderProcessed,
                AddressOf _audit.HandleOrderProcessed

            RemoveHandler _processor.OrderRejected,
                AddressOf _audit.HandleOrderRejected

            _auditSubscribed = False
        End If
    End Sub
    Private Sub btnOpenReport_Click(
    sender As Object,
    e As EventArgs) _
    Handles btnOpenReport.Click

        Using reportForm As New OrderReportForm(
            _reportStore,
            New OrderReportExporter())

            reportForm.ShowDialog(Me)
        End Using
    End Sub

End Class