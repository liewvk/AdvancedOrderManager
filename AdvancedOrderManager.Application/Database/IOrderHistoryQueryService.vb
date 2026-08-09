Option Explicit On
Option Strict On
Option Infer On

Imports System.Collections.Generic
Imports System.Threading
Imports System.Threading.Tasks

Public Interface IOrderHistoryQueryService

    Function SearchAsync(
        criteria As OrderHistorySearchCriteria,
        cancellationToken As CancellationToken) _
        As Task(Of IReadOnlyList(Of StoredOrderRecord))

    Function GetStatisticsAsync(
        cancellationToken As CancellationToken) _
        As Task(Of OrderHistoryStatistics)

End Interface

