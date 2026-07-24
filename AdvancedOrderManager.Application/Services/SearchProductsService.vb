Option Explicit On
Option Strict On
Option Infer On

Imports System.Linq
Imports AdvancedOrderManager.Domain

Namespace Application

    Public NotInheritable Class SearchProductsService

        Private ReadOnly _repository As IProductRepository

        Public Sub New(
            repository As IProductRepository)

            If repository Is Nothing Then
                Throw New ArgumentNullException(
                    NameOf(repository))
            End If

            _repository = repository
        End Sub

        Public Function Execute(
            searchText As String,
            category As String,
            includeInactive As Boolean) _
            As IReadOnlyList(Of Product)

            Dim normalisedSearch As String =
                If(searchText, String.Empty).Trim()

            Dim normalisedCategory As String =
                If(category, String.Empty).Trim()

            Return _repository.Find(
                Function(product)

                    If Not includeInactive AndAlso
                       Not product.IsActive Then

                        Return False
                    End If

                    If normalisedCategory.Length > 0 AndAlso
                       Not String.Equals(
                           product.Category,
                           normalisedCategory,
                           StringComparison.OrdinalIgnoreCase) Then

                        Return False
                    End If

                    If normalisedSearch.Length = 0 Then
                        Return True
                    End If

                    Return product.Code.Value.Contains(
                               normalisedSearch,
                               StringComparison.OrdinalIgnoreCase) OrElse
                           product.Name.Contains(
                               normalisedSearch,
                               StringComparison.OrdinalIgnoreCase) OrElse
                           product.Category.Contains(
                               normalisedSearch,
                               StringComparison.OrdinalIgnoreCase)
                End Function) _
                .OrderBy(
                    Function(product)
                        Return product.Name
                    End Function) _
                .ToList()
        End Function

    End Class

End Namespace

