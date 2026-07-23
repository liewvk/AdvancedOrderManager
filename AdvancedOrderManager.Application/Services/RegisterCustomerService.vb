Option Explicit On
Option Strict On
Option Infer On

Imports AdvancedOrderManager.Domain
Imports AdvancedOrderManager.Domain.Entities
Imports AdvancedOrderManager.Domain.ValueObjects

Namespace Application

    Public NotInheritable Class RegisterCustomerService

        Private ReadOnly _repository As ICustomerRepository

        Public Sub New(
            repository As ICustomerRepository)

            If repository Is Nothing Then
                Throw New ArgumentNullException(
                    NameOf(repository))
            End If

            _repository = repository
        End Sub

        Public Function Execute(
            request As RegisterCustomerRequest) _
            As OperationResult(Of CustomerProfile)

            If request Is Nothing Then
                Throw New ArgumentNullException(
                    NameOf(request))
            End If

            Try
                Dim name As New PersonName(
                    request.FirstName,
                    request.LastName)

                Dim email As EmailAddress =
                    EmailAddress.Create(
                        request.Email)

                If _repository.EmailExists(email) Then

                    Return OperationResult(
                        Of CustomerProfile) _
                        .Failure(
                            "The email address is already registered.")
                End If

                Dim address As New PostalAddress(
                    request.AddressLine,
                    request.City,
                    request.PostalCode,
                    request.Country)

                Dim customer As New CustomerProfile(
                    CustomerId.NewId(),
                    name,
                    email,
                    address)

                _repository.Add(customer)

                Return OperationResult(
                    Of CustomerProfile) _
                    .Success(customer)

            Catch ex As ArgumentException

                Return OperationResult(
                    Of CustomerProfile) _
                    .Failure(ex.Message)
            End Try
        End Function

    End Class

End Namespace

