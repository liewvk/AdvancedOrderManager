Option Explicit On
Option Strict On
Option Infer On

Public Interface IRuntimePerformanceMonitor

    Function CaptureSnapshot() _
        As RuntimePerformanceSnapshot

End Interface

