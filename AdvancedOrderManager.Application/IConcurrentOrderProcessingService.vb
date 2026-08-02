Option Explicit On
Option Strict On
Option Infer On

Imports System.Threading
Imports System.Threading.Tasks

Public Interface IConcurrentOrderProcessingService

    Function ProcessBatchAsync(
        orderCount As Integer,
        maximumConcurrency As Integer,
        progress As IProgress(Of ConcurrentBatchProgress),
        cancellationToken As CancellationToken) _
        As Task(Of ConcurrentBatchSummary)

End Interface

