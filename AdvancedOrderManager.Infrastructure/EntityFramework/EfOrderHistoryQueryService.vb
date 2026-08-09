Option Explicit On
Option Strict On
Option Infer On

Imports System.Collections.Generic
Imports System.Linq
Imports System.Threading
Imports System.Threading.Tasks
Imports AdvancedOrderManager.Application
Imports Microsoft.EntityFrameworkCore
Imports Microsoft.Extensions.Logging

Public NotInheritable Class EfOrderHistoryQueryService
    Implements IOrderHistoryQueryService

    Private ReadOnly _contextFactory As IDbContextFactory(
        Of OrderDbContext)

    Private ReadOnly _logger As ILogger(
        Of EfOrderHistoryQueryService)

    Public Sub New(
        contextFactory As IDbContextFactory(Of OrderDbContext),
        logger As ILogger(Of EfOrderHistoryQueryService))

        If contextFactory Is Nothing Then
            Throw New ArgumentNullException(
                NameOf(contextFactory))
        End If

        If logger Is Nothing Then
            Throw New ArgumentNullException(
                NameOf(logger))
        End If

        _contextFactory = contextFactory
        _logger = logger
    End Sub

    Public Async Function SearchAsync(
        criteria As OrderHistorySearchCriteria,
        cancellationToken As CancellationToken) _
        As Task(Of IReadOnlyList(Of StoredOrderRecord)) _
        Implements IOrderHistoryQueryService.SearchAsync

        If criteria Is Nothing Then
            Throw New ArgumentNullException(
                NameOf(criteria))
        End If

        Using context As OrderDbContext =
            Await _contextFactory.CreateDbContextAsync(
                cancellationToken)

            Dim query As IQueryable(
                Of ProcessedOrderEntity) =
                context.ProcessedOrders _
                    .AsNoTracking()

            If criteria.CustomerName.Length > 0 Then

                Dim customerFilter As String =
                    criteria.CustomerName

                query =
                    query.Where(Function(order) order.CustomerName.Contains(customerFilter))
            End If

            If criteria.Status.Length > 0 Then

                Dim statusFilter As String =
                    criteria.Status

                query =
                    query.Where(Function(order) order.Status = statusFilter)
            End If

            If criteria.PriorityOnly Then

                query =
                    query.Where(Function(order) order.IsPriority)
            End If

            Dim entities =
                Await query _
                    .OrderByDescending(Function(order) order.ProcessedAt) _
                    .ThenBy(Function(order) order.OrderId) _
                    .ToListAsync(
                        cancellationToken)

            Dim records =
                entities _
                    .Select(Function(entity) ToRecord(entity)) _
                    .ToList()

            _logger.LogInformation(
                "EF Core search returned {RecordCount} records.",
                records.Count)

            Return records.AsReadOnly()
        End Using
    End Function

    Public Async Function GetStatisticsAsync(
        cancellationToken As CancellationToken) _
        As Task(Of OrderHistoryStatistics) _
        Implements IOrderHistoryQueryService.GetStatisticsAsync

        Using context As OrderDbContext =
            Await _contextFactory.CreateDbContextAsync(
                cancellationToken)

            Dim query =
                context.ProcessedOrders _
                    .AsNoTracking()

            Dim totalOrders As Integer =
                Await query.CountAsync(
                    cancellationToken)

            If totalOrders = 0 Then

                Return New OrderHistoryStatistics(
                    0,
                    0,
                    0D,
                    0D)
            End If

            Dim priorityOrders As Integer =
                Await query.CountAsync(Function(order) order.IsPriority, cancellationToken)

            Dim totalAmount As Decimal =
                Await query.SumAsync(Function(order) order.TotalAmount, cancellationToken)

            Dim averageAmount As Decimal =
                Await query.AverageAsync(Function(order) order.TotalAmount, cancellationToken)

            Return New OrderHistoryStatistics(
                totalOrders,
                priorityOrders,
                totalAmount,
                averageAmount)
        End Using
    End Function

    Private Shared Function ToRecord(
        entity As ProcessedOrderEntity) _
        As StoredOrderRecord

        Return New StoredOrderRecord(
            entity.OrderId,
            entity.CustomerName,
            entity.Quantity,
            entity.UnitPrice,
            entity.IsPriority,
            entity.TotalAmount,
            entity.Status,
            entity.ProcessedAt)
    End Function

End Class

