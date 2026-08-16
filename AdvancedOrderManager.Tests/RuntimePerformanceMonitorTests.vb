Option Explicit On
Option Strict On
Option Infer On

Imports AdvancedOrderManager.Infrastructure
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass>
<TestCategory("Unit")>
Public Class RuntimePerformanceMonitorTests

    <TestMethod>
    Public Sub CaptureSnapshot_ReturnsNonNegativeValues()

        'Arrange

        Dim monitor =
            New RuntimePerformanceMonitor()

        'Act

        Dim snapshot =
            monitor.CaptureSnapshot()

        'Assert

        Assert.IsGreaterThanOrEqualTo(
            snapshot.ManagedMemoryBytes,
            0L)

        Assert.IsGreaterThanOrEqualTo(
            snapshot.WorkingSetBytes,
            0L)

        Assert.IsGreaterThanOrEqualTo(
            snapshot.Generation0Collections,
            0)

        Assert.IsGreaterThanOrEqualTo(
            snapshot.Generation1Collections,
            0)

        Assert.IsGreaterThanOrEqualTo(
            snapshot.Generation2Collections,
            0)

        Assert.IsGreaterThanOrEqualTo(
            snapshot.ProcessUptime,
            TimeSpan.Zero)

    End Sub

End Class

