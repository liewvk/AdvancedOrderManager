Option Explicit On
Option Strict On
Option Infer On

Imports System.Linq
Imports AdvancedOrderManager.Application
Imports AdvancedOrderManager.Domain

Namespace Infrastructure

    Public MustInherit Class InMemoryRepository(
        Of TKey,
        TEntity As {Class, IEntity(Of TKey)})

        Implements IRepository(Of TKey, TEntity)

        Private ReadOnly _entities As New Dictionary(Of TKey, TEntity)()

        Private ReadOnly _syncRoot As New Object()

        Protected ReadOnly Property Entities As Dictionary(Of TKey, TEntity)

            Get
                Return _entities
            End Get
        End Property

        Protected ReadOnly Property SyncRoot As Object
            Get
                Return _syncRoot
            End Get
        End Property

        Public ReadOnly Property Count As Integer _
            Implements IRepository(Of TKey, TEntity).Count

            Get
                SyncLock _syncRoot
                    Return _entities.Count
                End SyncLock
            End Get
        End Property

        Public Overridable Sub Add(
            entity As TEntity) _
            Implements IRepository(Of TKey, TEntity).Add

            If entity Is Nothing Then
                Throw New ArgumentNullException(
                    NameOf(entity))
            End If

            SyncLock _syncRoot

                If _entities.ContainsKey(entity.Id) Then
                    Throw New InvalidOperationException(
                        "An entity with the same identifier already exists.")
                End If

                OnAdding(entity)

                _entities.Add(entity.Id, entity)

                OnAdded(entity)
            End SyncLock
        End Sub

        Public Overridable Sub Update(
            entity As TEntity) _
            Implements IRepository(Of TKey, TEntity).Update

            If entity Is Nothing Then
                Throw New ArgumentNullException(
                    NameOf(entity))
            End If

            SyncLock _syncRoot

                Dim existing As TEntity = Nothing

                If Not _entities.TryGetValue(
                    entity.Id,
                    existing) Then

                    Throw New KeyNotFoundException(
                        "The entity was not found.")
                End If

                OnUpdating(existing, entity)

                _entities(entity.Id) = entity

                OnUpdated(existing, entity)
            End SyncLock
        End Sub

        Public Overridable Function Remove(
            id As TKey) As Boolean _
            Implements IRepository(
                Of TKey, TEntity).Remove

            SyncLock _syncRoot

                Dim existing As TEntity = Nothing

                If Not _entities.TryGetValue(
                    id,
                    existing) Then

                    Return False
                End If

                OnRemoving(existing)

                Dim removed As Boolean =
                    _entities.Remove(id)

                If removed Then
                    OnRemoved(existing)
                End If

                Return removed
            End SyncLock
        End Function

        Public Function GetById(
            id As TKey) As TEntity _
            Implements IRepository(
                Of TKey, TEntity).GetById

            SyncLock _syncRoot

                Dim entity As TEntity = Nothing

                If _entities.TryGetValue(
                    id,
                    entity) Then

                    Return entity
                End If
            End SyncLock

            Return Nothing
        End Function

        Public Function GetAll() _
            As IReadOnlyList(Of TEntity) _
            Implements IRepository(
                Of TKey, TEntity).GetAll

            SyncLock _syncRoot
                Return _entities.Values.ToList()
            End SyncLock
        End Function

        Public Function Exists(
            id As TKey) As Boolean _
            Implements IRepository(
                Of TKey, TEntity).Exists

            SyncLock _syncRoot
                Return _entities.ContainsKey(id)
            End SyncLock
        End Function

        Public Function Find(
            predicate As Func(Of TEntity, Boolean)) _
            As IReadOnlyList(Of TEntity) _
            Implements IRepository(
                Of TKey, TEntity).Find

            If predicate Is Nothing Then
                Throw New ArgumentNullException(
                    NameOf(predicate))
            End If

            SyncLock _syncRoot

                Return _entities.Values _
                    .Where(predicate) _
                    .ToList()
            End SyncLock
        End Function

        Protected Overridable Sub OnAdding(
            entity As TEntity)
        End Sub

        Protected Overridable Sub OnAdded(
            entity As TEntity)
        End Sub

        Protected Overridable Sub OnUpdating(
            existing As TEntity,
            replacement As TEntity)
        End Sub

        Protected Overridable Sub OnUpdated(
            previous As TEntity,
            current As TEntity)
        End Sub

        Protected Overridable Sub OnRemoving(
            entity As TEntity)
        End Sub

        Protected Overridable Sub OnRemoved(
            entity As TEntity)
        End Sub

    End Class

End Namespace

