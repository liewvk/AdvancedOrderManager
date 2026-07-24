Option Explicit On
Option Strict On
Option Infer On

Imports AdvancedOrderManager.Domain

Public NotInheritable Class ProductGridRow

    Public Sub New(product As Product)

        If product Is Nothing Then
            Throw New ArgumentNullException(
                NameOf(product))
        End If

        ProductId = product.ProductId
        Code = product.Code.Value
        ProductName = product.Name
        Category = product.Category
        UnitPrice = product.UnitPrice
        QuantityInStock = product.QuantityInStock
        ReorderLevel = product.ReorderLevel
        StockStatus = product.StockStatus.ToString()
        StockValue = product.StockValue
        Active = product.IsActive
    End Sub

    Public ReadOnly Property ProductId As ProductId

    Public ReadOnly Property Code As String

    Public ReadOnly Property ProductName As String

    Public ReadOnly Property Category As String

    Public ReadOnly Property UnitPrice As Decimal

    Public ReadOnly Property QuantityInStock As Integer

    Public ReadOnly Property ReorderLevel As Integer

    Public ReadOnly Property StockStatus As String

    Public ReadOnly Property StockValue As Decimal

    Public ReadOnly Property Active As Boolean

End Class

