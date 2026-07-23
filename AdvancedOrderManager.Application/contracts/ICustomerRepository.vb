Option Explicit On
Option Strict On
Option Infer On

Imports AdvancedOrderManager.Domain
Imports AdvancedOrderManager.Domain.Entities
Imports AdvancedOrderManager.Domain.ValueObjects

Namespace Application

    Public Interface ICustomerRepository

        Sub Add(customer As CustomerProfile)

        Function GetAll() As IReadOnlyList(Of CustomerProfile)

        Function GetById(
            customerId As CustomerId) As CustomerProfile

        Function GetByEmail(
            email As EmailAddress) As CustomerProfile

        Function EmailExists(
            email As EmailAddress) As Boolean

    End Interface

End Namespace

