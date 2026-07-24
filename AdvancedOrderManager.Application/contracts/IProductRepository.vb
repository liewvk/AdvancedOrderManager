Option Explicit On
Option Strict On
Option Infer On

Imports AdvancedOrderManager.Domain

Namespace Application

    Public Interface IProductRepository
        Inherits IRepository(Of ProductId, Product)

        Function CodeExists(
            code As ProductCode) As Boolean

        Function GetByCode(
            code As ProductCode) As Product

        Function GetCategories() As IReadOnlyList(Of String)

    End Interface

End Namespace

