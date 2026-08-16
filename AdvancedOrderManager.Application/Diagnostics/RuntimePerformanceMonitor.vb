Option Explicit On
Option Strict On
Option Infer On

Imports System.Diagnostics
Imports AdvancedOrderManager.Application

Public NotInheritable Class RuntimePerformanceMonitor
    Implements IRuntimePerformanceMonitor

    Public Function CaptureSnapshot() _
        As RuntimePerformanceSnapshot _
        Implements IRuntimePerformanceMonitor.CaptureSnapshot

        Dim managedMemoryBytes As Long =
            GC.GetTotalMemory(
                False)

        Dim generation0Collections As Integer =
            GC.CollectionCount(
                0)

        Dim generation1Collections As Integer =
            GC.CollectionCount(
                1)

        Dim generation2Collections As Integer =
            GC.CollectionCount(
                2)

        Using currentProcess As Process =
            Process.GetCurrentProcess()

            Dim workingSetBytes As Long =
                currentProcess.WorkingSet64

            Dim processUptime As TimeSpan =
                DateTime.Now -
                currentProcess.StartTime

            Return New RuntimePerformanceSnapshot(
                managedMemoryBytes,
                workingSetBytes,
                generation0Collections,
                generation1Collections,
                generation2Collections,
                processUptime)

        End Using

    End Function

End Class

