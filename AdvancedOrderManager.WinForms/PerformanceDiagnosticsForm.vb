Option Explicit On
Option Strict On
Option Infer On

Imports System.Diagnostics
Imports AdvancedOrderManager.Application
Imports Microsoft.Extensions.Logging

Public Class PerformanceDiagnosticsForm

    Private _performanceMonitor As IRuntimePerformanceMonitor

    Private _logger As ILogger(
            Of PerformanceDiagnosticsForm)

    Public Sub New()

        InitializeComponent()

    End Sub

    Public Sub New(
        performanceMonitor As IRuntimePerformanceMonitor,
        logger As ILogger(
            Of PerformanceDiagnosticsForm))

        InitializeComponent()

        ArgumentNullException.ThrowIfNull(
            performanceMonitor)

        ArgumentNullException.ThrowIfNull(
            logger)

        _performanceMonitor =
            performanceMonitor

        _logger =
            logger

    End Sub

    Private Sub PerformanceDiagnosticsForm_Load(
        sender As Object,
        e As EventArgs) _
        Handles MyBase.Load

        If Not EnsureServicesAvailable() Then

            Return

        End If

        RefreshSnapshot()

    End Sub

    Private Function EnsureServicesAvailable() _
        As Boolean

        If _performanceMonitor IsNot Nothing Then

            Return True

        End If

        MessageBox.Show(
            Me,
            "Performance monitoring services are unavailable. " &
            "Start the application through Program.Main.",
            "Performance Services Unavailable",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

        Return False

    End Function

    Private Sub btnRefresh_Click(
        sender As Object,
        e As EventArgs) _
        Handles btnRefresh.Click

        If Not EnsureServicesAvailable() Then

            Return

        End If

        RefreshSnapshot()

    End Sub

    Private Sub RefreshSnapshot()

        Dim snapshot =
            _performanceMonitor.CaptureSnapshot()

        lblManagedMemory.Text =
            $"Managed memory: " &
            $"{FormatMegabytes(snapshot.ManagedMemoryBytes):N2} MB"

        lblWorkingSet.Text =
            $"Working set: " &
            $"{FormatMegabytes(snapshot.WorkingSetBytes):N2} MB"

        lblGeneration0.Text =
            $"Generation 0 collections: " &
            $"{snapshot.Generation0Collections}"

        lblGeneration1.Text =
            $"Generation 1 collections: " &
            $"{snapshot.Generation1Collections}"

        lblGeneration2.Text =
            $"Generation 2 collections: " &
            $"{snapshot.Generation2Collections}"

        lblUptime.Text =
            $"Process uptime: " &
            $"{snapshot.ProcessUptime:hh\:mm\:ss}"

        _logger.LogInformation(
            "Performance snapshot captured. " &
            "Managed memory {ManagedMemoryBytes} bytes, " &
            "working set {WorkingSetBytes} bytes.",
            snapshot.ManagedMemoryBytes,
            snapshot.WorkingSetBytes)

    End Sub

    Private Sub btnRunAllocationDemo_Click(
        sender As Object,
        e As EventArgs) _
        Handles btnRunAllocationDemo.Click

        If Not EnsureServicesAvailable() Then

            Return

        End If

        Dim timer =
            Stopwatch.StartNew()

        Dim allocatedBefore As Long =
            GC.GetAllocatedBytesForCurrentThread()

        Dim values As New List(Of String)(
                50000)

        For index As Integer =
            1 To 50000

            values.Add(
                $"Order-{index:000000}")

        Next

        Dim allocatedAfter As Long =
            GC.GetAllocatedBytesForCurrentThread()

        timer.Stop()

        Dim allocatedBytes As Long =
            allocatedAfter -
            allocatedBefore

        _logger.LogInformation(
            "Allocation demonstration created " &
            "{ItemCount} strings in " &
            "{ElapsedMilliseconds} ms and allocated " &
            "approximately {AllocatedBytes} bytes " &
            "on the current thread.",
            values.Count,
            timer.ElapsedMilliseconds,
            allocatedBytes)

        MessageBox.Show(
            Me,
            $"Items created: {values.Count:N0}" &
            Environment.NewLine &
            $"Elapsed: {timer.ElapsedMilliseconds:N0} ms" &
            Environment.NewLine &
            $"Thread allocation: " &
            $"{FormatMegabytes(allocatedBytes):N2} MB",
            "Allocation Demonstration",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information)

        RefreshSnapshot()

    End Sub

    Private Shared Function FormatMegabytes(
        bytes As Long) _
        As Double

        Return bytes /
            1024.0R /
            1024.0R

    End Function

End Class
