Option Explicit On
Option Strict On
Option Infer On

Imports AdvancedOrderManager.Domain

Namespace Application

    Public NotInheritable Class InventoryStatisticsService

        Private ReadOnly _repository As IProductRepository

        Public Sub New(
            repository As IProductRepository)

            If repository Is Nothing Then
                Throw New ArgumentNullException(
                    NameOf(repository))
            End If

            _repository = repository
        End Sub

        Public Function Execute() As InventoryStatistics

            Dim products =
                _repository.GetAll()

            Dim activeCount As Integer = 0
            Dim lowStockCount As Integer = 0
            Dim totalUnits As Long = 0
            Dim totalValue As Decimal = 0D

            Dim productsByCategory As New SortedDictionary(Of String, Integer)(
                    StringComparer.OrdinalIgnoreCase)

            For Each product As Product In products

                If product.IsActive Then
                    activeCount += 1
                End If

                If product.NeedsRestock Then
                    lowStockCount += 1
                End If

                totalUnits += product.QuantityInStock
                totalValue += product.StockValue

                If productsByCategory.ContainsKey(
                    product.Category) Then

                    productsByCategory(product.Category) += 1
                Else
                    productsByCategory.Add(
                        product.Category,
                        1)
                End If
            Next

            Return New InventoryStatistics(
                products.Count,
                activeCount,
                lowStockCount,
                totalUnits,
                totalValue,
                productsByCategory)
        End Function

    End Class

End Namespace

