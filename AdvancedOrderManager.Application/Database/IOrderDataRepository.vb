Option Explicit On
Option Strict On
Option Infer On

Imports System.Collections.Generic
Imports System.Threading
Imports System.Threading.Tasks

Public Interface IOrderDataRepository

    Function AddAsync(
        record As StoredOrderRecord,
        cancellationToken As CancellationToken) As Task

    Function GetAllAsync(
        cancellationToken As CancellationToken) _
        As Task(Of IReadOnlyList(Of StoredOrderRecord))

    Function FindByOrderIdAsync(
        orderId As String,
        cancellationToken As CancellationToken) _
        As Task(Of StoredOrderRecord)

    Function DeleteByOrderIdAsync(
        orderId As String,
        cancellationToken As CancellationToken) _
        As Task(Of Boolean)

    Function DeleteAllAsync(
        cancellationToken As CancellationToken) _
        As Task(Of Integer)

End Interface

