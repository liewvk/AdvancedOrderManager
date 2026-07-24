Option Explicit On
Option Strict On
Option Infer On

Imports AdvancedOrderManager.Domain

Namespace Application

    Public NotInheritable Class InventoryAdjustmentService

        Private NotInheritable Class UndoEntry

            Public Sub New(
                productId As ProductId,
                reverseChange As Integer,
                reason As String)

                Me.ProductId = productId
                Me.ReverseChange = reverseChange
                Me.Reason = reason
            End Sub

            Public ReadOnly Property ProductId As ProductId

            Public ReadOnly Property ReverseChange As Integer

            Public ReadOnly Property Reason As String

        End Class

        Private ReadOnly _repository As IProductRepository

        Private ReadOnly _undoStack As New Stack(Of UndoEntry)()

        Private ReadOnly _syncRoot As New Object()

        Public Sub New(
            repository As IProductRepository)

            If repository Is Nothing Then
                Throw New ArgumentNullException(
                    NameOf(repository))
            End If

            _repository = repository
        End Sub

        Public ReadOnly Property CanUndo As Boolean
            Get
                SyncLock _syncRoot
                    Return _undoStack.Count > 0
                End SyncLock
            End Get
        End Property

        Public ReadOnly Property UndoCount As Integer
            Get
                SyncLock _syncRoot
                    Return _undoStack.Count
                End SyncLock
            End Get
        End Property

        Public Function Adjust(
            productId As ProductId,
            quantityChange As Integer,
            reason As String) _
            As OperationResult(Of Product)

            If quantityChange = 0 Then

                Return OperationResult(Of Product) _
                    .Failure(
                        "The stock adjustment cannot be zero.")
            End If

            Dim product As Product =
                _repository.GetById(productId)

            If product Is Nothing Then

                Return OperationResult(Of Product) _
                    .Failure(
                        "The selected product was not found.")
            End If

            Try
                product.AdjustStock(
                    quantityChange,
                    reason)

                _repository.Update(product)

                SyncLock _syncRoot

                    _undoStack.Push(
                        New UndoEntry(
                            productId,
                            -quantityChange,
                            $"Undo: {reason}"))
                End SyncLock

                Return OperationResult(Of Product) _
                    .Success(product)

            Catch ex As ArgumentException

                Return OperationResult(Of Product) _
                    .Failure(ex.Message)

            Catch ex As InvalidOperationException

                Return OperationResult(Of Product) _
                    .Failure(ex.Message)

            Catch ex As OverflowException

                Return OperationResult(Of Product) _
                    .Failure(ex.Message)
            End Try
        End Function

        Public Function UndoLast() _
            As OperationResult(Of Product)

            Dim entry As UndoEntry

            SyncLock _syncRoot

                If _undoStack.Count = 0 Then

                    Return OperationResult(Of Product) _
                        .Failure(
                            "There is no stock adjustment to undo.")
                End If

                entry = _undoStack.Pop()
            End SyncLock

            Dim product As Product =
                _repository.GetById(
                    entry.ProductId)

            If product Is Nothing Then

                Return OperationResult(Of Product) _
                    .Failure(
                        "The product for the undo operation no longer exists.")
            End If

            Try
                product.AdjustStock(
                    entry.ReverseChange,
                    entry.Reason)

                _repository.Update(product)

                Return OperationResult(Of Product) _
                    .Success(product)

            Catch ex As Exception

                SyncLock _syncRoot
                    _undoStack.Push(entry)
                End SyncLock

                Return OperationResult(Of Product) _
                    .Failure(
                        $"The adjustment could not be undone: {ex.Message}")
            End Try
        End Function

    End Class

End Namespace
