Option Explicit On
Option Strict On
Option Infer On

Imports AdvancedOrderManager.Domain

Namespace Application

    Public NotInheritable Class RestockQueueService

        Private ReadOnly _repository As IProductRepository

        Private ReadOnly _queue As New Queue(Of ProductId)()

        Private ReadOnly _queuedIds As New HashSet(Of ProductId)()

        Private ReadOnly _syncRoot As New Object()

        Public Sub New(
            repository As IProductRepository)

            If repository Is Nothing Then
                Throw New ArgumentNullException(
                    NameOf(repository))
            End If

            _repository = repository
        End Sub

        Public ReadOnly Property PendingCount As Integer
            Get
                SyncLock _syncRoot
                    Return _queue.Count
                End SyncLock
            End Get
        End Property

        Public Function Enqueue(
            productId As ProductId) _
            As OperationResult(Of Product)

            Dim product As Product =
                _repository.GetById(productId)

            If product Is Nothing Then

                Return OperationResult(Of Product) _
                    .Failure(
                        "The selected product was not found.")
            End If

            If Not product.NeedsRestock Then

                Return OperationResult(Of Product) _
                    .Failure(
                        "The selected product does not currently require restocking.")
            End If

            SyncLock _syncRoot

                If Not _queuedIds.Add(productId) Then

                    Return OperationResult(Of Product) _
                        .Failure(
                            "The product is already in the restock queue.")
                End If

                _queue.Enqueue(productId)
            End SyncLock

            Return OperationResult(Of Product) _
                .Success(product)
        End Function

        Public Function TryProcessNext() _
            As OperationResult(Of Product)

            Dim productId As ProductId

            SyncLock _syncRoot

                If _queue.Count = 0 Then

                    Return OperationResult(Of Product) _
                        .Failure(
                            "The restock queue is empty.")
                End If

                productId = _queue.Dequeue()
                _queuedIds.Remove(productId)
            End SyncLock

            Dim product As Product =
                _repository.GetById(productId)

            If product Is Nothing Then

                Return OperationResult(Of Product) _
                    .Failure(
                        "The queued product no longer exists.")
            End If

            Return OperationResult(Of Product) _
                .Success(product)
        End Function

        Public Function Contains(
            productId As ProductId) As Boolean

            SyncLock _syncRoot
                Return _queuedIds.Contains(productId)
            End SyncLock
        End Function

    End Class

End Namespace

