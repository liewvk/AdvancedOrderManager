Option Explicit On
Option Strict On
Option Infer On

Imports System.Threading
Imports System.Threading.Tasks

Public Interface IAsyncOrderProcessingService

    Function ProcessAsync(
        orderCount As Integer,
        progress As IProgress(
            Of OrderProcessingProgress),
        cancellationToken As CancellationToken) _
        As Task(Of OrderProcessingSummary)

End Interface

