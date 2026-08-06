Option Explicit On
Option Strict On
Option Infer On

Imports System.Collections.Generic
Imports System.Data
Imports System.Threading
Imports System.Threading.Tasks
Imports AdvancedOrderManager.Application
Imports Microsoft.Data.SqlClient
Imports Microsoft.Extensions.Logging
Imports Microsoft.Extensions.Options

Public NotInheritable Class SqlOrderDataRepository
    Implements IOrderDataRepository

    Private ReadOnly _connectionString As String

    Private ReadOnly _logger As ILogger(
        Of SqlOrderDataRepository)

    Public Sub New(
        options As IOptions(Of OrderDatabaseOptions),
        logger As ILogger(Of SqlOrderDataRepository))

        If options Is Nothing Then
            Throw New ArgumentNullException(
                NameOf(options))
        End If

        If logger Is Nothing Then
            Throw New ArgumentNullException(
                NameOf(logger))
        End If

        If String.IsNullOrWhiteSpace(
            options.Value.ConnectionString) Then

            Throw New InvalidOperationException(
                "The order database connection string " &
                "has not been configured.")
        End If

        _connectionString =
            options.Value.ConnectionString

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

        Const sql As String =
            "INSERT INTO dbo.ProcessedOrders " &
            "(" &
            "OrderId, CustomerName, Quantity, UnitPrice, " &
            "IsPriority, TotalAmount, Status, ProcessedAt" &
            ") " &
            "VALUES " &
            "(" &
            "@OrderId, @CustomerName, @Quantity, @UnitPrice, " &
            "@IsPriority, @TotalAmount, @Status, @ProcessedAt" &
            ");"

        Using connection As SqlConnection =
            CreateConnection()

            Await connection.OpenAsync(
                cancellationToken)

            Using command As New SqlCommand(
                sql,
                connection)

                AddRecordParameters(
                    command,
                    record)

                Await command.ExecuteNonQueryAsync(
                    cancellationToken)
            End Using
        End Using

        _logger.LogInformation(
            "Order {OrderId} was added to the database.",
            record.OrderId)
    End Function

    Public Async Function GetAllAsync(
        cancellationToken As CancellationToken) _
        As Task(Of IReadOnlyList(Of StoredOrderRecord)) _
        Implements IOrderDataRepository.GetAllAsync

        Const sql As String =
            "SELECT " &
            "OrderId, CustomerName, Quantity, UnitPrice, " &
            "IsPriority, TotalAmount, Status, ProcessedAt " &
            "FROM dbo.ProcessedOrders " &
            "ORDER BY ProcessedAt DESC, Id DESC;"

        Dim records As New List(Of StoredOrderRecord)()

        Using connection As SqlConnection =
            CreateConnection()

            Await connection.OpenAsync(
                cancellationToken)

            Using command As New SqlCommand(
                sql,
                connection)

                Using reader As SqlDataReader =
                    Await command.ExecuteReaderAsync(
                        cancellationToken)

                    While Await reader.ReadAsync(
                        cancellationToken)

                        records.Add(
                            ReadRecord(reader))
                    End While
                End Using
            End Using
        End Using

        _logger.LogInformation(
            "{RecordCount} database order records were loaded.",
            records.Count)

        Return records.AsReadOnly()
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

        Const sql As String =
            "SELECT " &
            "OrderId, CustomerName, Quantity, UnitPrice, " &
            "IsPriority, TotalAmount, Status, ProcessedAt " &
            "FROM dbo.ProcessedOrders " &
            "WHERE OrderId = @OrderId;"

        Using connection As SqlConnection =
            CreateConnection()

            Await connection.OpenAsync(
                cancellationToken)

            Using command As New SqlCommand(
                sql,
                connection)

                command.Parameters.Add(
                    "@OrderId",
                    SqlDbType.NVarChar,
                    50).Value = orderId.Trim()

                Using reader As SqlDataReader =
                    Await command.ExecuteReaderAsync(
                        cancellationToken)

                    If Await reader.ReadAsync(
                        cancellationToken) Then

                        Return ReadRecord(reader)
                    End If
                End Using
            End Using
        End Using

        Return Nothing
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

        Const sql As String =
            "DELETE FROM dbo.ProcessedOrders " &
            "WHERE OrderId = @OrderId;"

        Dim affectedRows As Integer

        Using connection As SqlConnection =
            CreateConnection()

            Await connection.OpenAsync(
                cancellationToken)

            Using command As New SqlCommand(
                sql,
                connection)

                command.Parameters.Add(
                    "@OrderId",
                    SqlDbType.NVarChar,
                    50).Value = orderId.Trim()

                affectedRows =
                    Await command.ExecuteNonQueryAsync(
                        cancellationToken)
            End Using
        End Using

        If affectedRows > 0 Then

            _logger.LogInformation(
                "Order {OrderId} was deleted " &
                "from the database.",
                orderId)
        End If

        Return affectedRows > 0
    End Function

    Public Async Function DeleteAllAsync(
        cancellationToken As CancellationToken) _
        As Task(Of Integer) _
        Implements IOrderDataRepository.DeleteAllAsync

        Const sql As String =
            "DELETE FROM dbo.ProcessedOrders;"

        Dim affectedRows As Integer

        Using connection As SqlConnection =
            CreateConnection()

            Await connection.OpenAsync(
                cancellationToken)

            Using command As New SqlCommand(
                sql,
                connection)

                affectedRows =
                    Await command.ExecuteNonQueryAsync(
                        cancellationToken)
            End Using
        End Using

        _logger.LogWarning(
            "{DeletedCount} order records were deleted.",
            affectedRows)

        Return affectedRows
    End Function

    Private Function CreateConnection() As SqlConnection

        Return New SqlConnection(
            _connectionString)
    End Function

    Private Shared Sub AddRecordParameters(
        command As SqlCommand,
        record As StoredOrderRecord)

        command.Parameters.Add(
            "@OrderId",
            SqlDbType.NVarChar,
            50).Value = record.OrderId

        command.Parameters.Add(
            "@CustomerName",
            SqlDbType.NVarChar,
            120).Value = record.CustomerName

        command.Parameters.Add(
            "@Quantity",
            SqlDbType.Int).Value = record.Quantity

        Dim unitPriceParameter =
            command.Parameters.Add(
                "@UnitPrice",
                SqlDbType.Decimal)

        unitPriceParameter.Precision = 18
        unitPriceParameter.Scale = 2
        unitPriceParameter.Value = record.UnitPrice

        command.Parameters.Add(
            "@IsPriority",
            SqlDbType.Bit).Value = record.IsPriority

        Dim totalAmountParameter =
            command.Parameters.Add(
                "@TotalAmount",
                SqlDbType.Decimal)

        totalAmountParameter.Precision = 18
        totalAmountParameter.Scale = 2
        totalAmountParameter.Value = record.TotalAmount

        command.Parameters.Add(
            "@Status",
            SqlDbType.NVarChar,
            30).Value = record.Status

        command.Parameters.Add(
            "@ProcessedAt",
            SqlDbType.DateTimeOffset).Value =
            record.ProcessedAt
    End Sub

    Private Shared Function ReadRecord(
        reader As SqlDataReader) As StoredOrderRecord

        Dim processedAtValue As DateTimeOffset =
            CType(
                reader.GetValue(7),
                DateTimeOffset)

        Return New StoredOrderRecord(
            reader.GetString(0),
            reader.GetString(1),
            reader.GetInt32(2),
            reader.GetDecimal(3),
            reader.GetBoolean(4),
            reader.GetDecimal(5),
            reader.GetString(6),
            processedAtValue)
    End Function

End Class

