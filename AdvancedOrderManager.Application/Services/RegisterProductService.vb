Option Explicit On
Option Strict On
Option Infer On

Imports AdvancedOrderManager.Domain

Namespace Application

    Public NotInheritable Class RegisterProductService

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
            request As RegisterProductRequest) _
            As OperationResult(Of Product)

            If request Is Nothing Then
                Throw New ArgumentNullException(
                    NameOf(request))
            End If

            Try
                Dim code As ProductCode =
                    ProductCode.Create(request.Code)

                If _repository.CodeExists(code) Then

                    Return OperationResult(Of Product) _
                        .Failure(
                            "The product code is already registered.")
                End If

                Dim product As New Product(
                    ProductId.NewId(),
                    code,
                    request.Name,
                    request.Category,
                    request.UnitPrice,
                    request.OpeningStock,
                    request.ReorderLevel)

                _repository.Add(product)

                Return OperationResult(Of Product) _
                    .Success(product)

            Catch ex As ArgumentException

                Return OperationResult(Of Product) _
                    .Failure(ex.Message)

            Catch ex As InvalidOperationException

                Return OperationResult(Of Product) _
                    .Failure(ex.Message)
            End Try
        End Function

    End Class

End Namespace

