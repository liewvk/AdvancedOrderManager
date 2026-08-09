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

Public NotInheritable Class EfOrderDataRepository
    Implements IOrderDataRepository

    Private ReadOnly _contextFactory As IDbContextFactory(
        Of OrderDbContext)

    Private ReadOnly _logger As ILogger(
        Of EfOrderDataRepository)

    Public Sub New(
        contextFactory As IDbContextFactory(Of OrderDbContext),
        logger As ILogger(Of EfOrderDataRepository))

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

    Public Async Function AddAsync(
        record As StoredOrderRecord,
        cancellationToken As CancellationToken) As Task _
        Implements IOrderDataRepository.AddAsync

        If record Is Nothing Then
            Throw New ArgumentNullException(
                NameOf(record))
        End If

        Using context As OrderDbContext =
            Await _contextFactory.CreateDbContextAsync(
                cancellationToken)

            Dim entity As ProcessedOrderEntity =
                ToEntity(record)

            context.ProcessedOrders.Add(entity)

            Await context.SaveChangesAsync(
                cancellationToken)
        End Using

        _logger.LogInformation(
            "Order {OrderId} was added using EF Core.",
            record.OrderId)
    End Function

    Public Async Function GetAllAsync(
        cancellationToken As CancellationToken) _
        As Task(Of IReadOnlyList(Of StoredOrderRecord)) _
        Implements IOrderDataRepository.GetAllAsync

        Using context As OrderDbContext =
            Await _contextFactory.CreateDbContextAsync(
                cancellationToken)

            Dim entities =
                Await context.ProcessedOrders _
                    .AsNoTracking() _
                    .OrderByDescending(
                        Function(order) order.ProcessedAt) _
                    .ThenByDescending(
                        Function(order) order.Id) _
                    .ToListAsync(
                        cancellationToken)

            Dim records As List(Of StoredOrderRecord) =
                entities _
                    .Select(
                        Function(entity)
                            Return ToRecord(entity)
                        End Function) _
                    .ToList()

            _logger.LogInformation(
                "{RecordCount} records were loaded using EF Core.",
                records.Count)

            Return records.AsReadOnly()
        End Using
    End Function

    Public Async Function FindByOrderIdAsync(
        orderId As String,
        cancellationToken As CancellationToken) _
        As Task(Of StoredOrderRecord) _
        Implements IOrderDataRepository.FindByOrderIdAsync

        If String.IsNullOrWhiteSpace(orderId) Then
            Throw New ArgumentException(
                "An order ID is required.",
                NameOf(orderId))
        End If

        Dim normalisedOrderId As String =
            orderId.Trim()

        Using context As OrderDbContext =
            Await _contextFactory.CreateDbContextAsync(
                cancellationToken)

            Dim entity =
                Await context.ProcessedOrders _
                    .AsNoTracking() _
                    .SingleOrDefaultAsync(
                        Function(order) order.OrderId = normalisedOrderId,
                        cancellationToken)

            If entity Is Nothing Then
                Return Nothing
            End If

            Return ToRecord(entity)
        End Using
    End Function

    Public Async Function DeleteByOrderIdAsync(
        orderId As String,
        cancellationToken As CancellationToken) _
        As Task(Of Boolean) _
        Implements IOrderDataRepository.DeleteByOrderIdAsync

        If String.IsNullOrWhiteSpace(orderId) Then
            Throw New ArgumentException(
                "An order ID is required.",
                NameOf(orderId))
        End If

        Dim normalisedOrderId As String =
            orderId.Trim()

        Dim affectedRows As Integer

        Using context As OrderDbContext =
            Await _contextFactory.CreateDbContextAsync(
                cancellationToken)

            affectedRows =
                Await context.ProcessedOrders _
                    .Where(
                        Function(order) order.OrderId = normalisedOrderId) _
                    .ExecuteDeleteAsync(
                        cancellationToken)
        End Using

        If affectedRows > 0 Then

            _logger.LogInformation(
                "Order {OrderId} was deleted using EF Core.",
                normalisedOrderId)
        End If

        Return affectedRows > 0
    End Function

    Public Async Function DeleteAllAsync(
        cancellationToken As CancellationToken) _
        As Task(Of Integer) _
        Implements IOrderDataRepository.DeleteAllAsync

        Dim affectedRows As Integer

        Using context As OrderDbContext =
            Await _contextFactory.CreateDbContextAsync(
                cancellationToken)

            affectedRows =
                Await context.ProcessedOrders _
                    .ExecuteDeleteAsync(
                        cancellationToken)
        End Using

        _logger.LogWarning(
            "{DeletedCount} records were deleted using EF Core.",
            affectedRows)

        Return affectedRows
    End Function

    Private Shared Function ToEntity(
        record As StoredOrderRecord) _
        As ProcessedOrderEntity

        Return New ProcessedOrderEntity With {
            .OrderId = record.OrderId,
            .CustomerName = record.CustomerName,
            .Quantity = record.Quantity,
            .UnitPrice = record.UnitPrice,
            .IsPriority = record.IsPriority,
            .TotalAmount = record.TotalAmount,
            .Status = record.Status,
            .ProcessedAt = record.ProcessedAt
        }
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

