Option Explicit On
Option Strict On
Option Infer On

Imports AdvancedOrderManager.Domain

Namespace Application

    Public Interface IRepository(
        Of TKey,
        TEntity As {Class, IEntity(Of TKey)})

        Sub Add(entity As TEntity)

        Sub Update(entity As TEntity)

        Function Remove(id As TKey) As Boolean

        Function GetById(id As TKey) As TEntity

        Function GetAll() As IReadOnlyList(Of TEntity)

        Function Exists(id As TKey) As Boolean

        Function Find(
            predicate As Func(Of TEntity, Boolean)) _
            As IReadOnlyList(Of TEntity)

        ReadOnly Property Count As Integer

    End Interface

End Namespace

