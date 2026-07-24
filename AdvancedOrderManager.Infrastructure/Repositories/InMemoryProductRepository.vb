Option Explicit On
Option Strict On
Option Infer On

Imports System.Linq
Imports AdvancedOrderManager.Application
Imports AdvancedOrderManager.Domain

Namespace Infrastructure

    Public NotInheritable Class InMemoryProductRepository

        Inherits InMemoryRepository(
            Of ProductId, Product)

        Implements IProductRepository

        Private ReadOnly _productIdsByCode As New Dictionary(
                Of ProductCode, ProductId)()

        Private ReadOnly _categories As New HashSet(Of String)(
                StringComparer.OrdinalIgnoreCase)

        Protected Overrides Sub OnAdding(
            entity As Product)

            If _productIdsByCode.ContainsKey(
                entity.Code) Then

                Throw New InvalidOperationException(
                    "The product code is already registered.")
            End If
        End Sub

        Protected Overrides Sub OnAdded(
            entity As Product)

            _productIdsByCode.Add(
                entity.Code,
                entity.ProductId)

            _categories.Add(entity.Category)
        End Sub

        Protected Overrides Sub OnUpdated(
            previous As Product,
            current As Product)

            RebuildCategories()
        End Sub

        Protected Overrides Sub OnRemoved(
            entity As Product)

            _productIdsByCode.Remove(entity.Code)
            RebuildCategories()
        End Sub

        Public Function CodeExists(
            code As ProductCode) As Boolean _
            Implements IProductRepository.CodeExists

            If code Is Nothing Then
                Throw New ArgumentNullException(
                    NameOf(code))
            End If

            SyncLock SyncRoot
                Return _productIdsByCode.ContainsKey(code)
            End SyncLock
        End Function

        Public Function GetByCode(
            code As ProductCode) As Product _
            Implements IProductRepository.GetByCode

            If code Is Nothing Then
                Throw New ArgumentNullException(
                    NameOf(code))
            End If

            SyncLock SyncRoot

                Dim productId As ProductId

                If Not _productIdsByCode.TryGetValue(
                    code,
                    productId) Then

                    Return Nothing
                End If

                Return Entities(productId)
            End SyncLock
        End Function

        Public Function GetCategories() _
            As IReadOnlyList(Of String) _
            Implements IProductRepository.GetCategories

            SyncLock SyncRoot

                Return _categories _
                    .OrderBy(
                        Function(category)
                            Return category
                        End Function,
                        StringComparer.OrdinalIgnoreCase) _
                    .ToList()
            End SyncLock
        End Function

        Private Sub RebuildCategories()

            _categories.Clear()

            For Each product In Entities.Values
                _categories.Add(product.Category)
            Next
        End Sub

    End Class

End Namespace

