Option Explicit On
Option Strict On
Option Infer On

Imports System.Linq
Imports AdvancedOrderManager.Application
Imports AdvancedOrderManager.Domain
Imports AdvancedOrderManager.Domain.Entities
Imports AdvancedOrderManager.Domain.ValueObjects

Namespace Infrastructure

    Public NotInheritable Class InMemoryCustomerRepository
        Implements ICustomerRepository

        Private ReadOnly _customersById As New Dictionary(
                Of CustomerId, CustomerProfile)()

        Private ReadOnly _customerIdsByEmail As New Dictionary(
                Of EmailAddress, CustomerId)()

        Private ReadOnly _syncRoot As New Object()

        Public Sub Add(
            customer As CustomerProfile) _
            Implements ICustomerRepository.Add

            If customer Is Nothing Then
                Throw New ArgumentNullException(
                    NameOf(customer))
            End If

            SyncLock _syncRoot

                If _customersById.ContainsKey(
                    customer.CustomerId) Then

                    Throw New InvalidOperationException(
                        "The customer already exists.")
                End If

                If _customerIdsByEmail.ContainsKey(
                    customer.Email) Then

                    Throw New InvalidOperationException(
                        "The email address is already registered.")
                End If

                _customersById.Add(
                    customer.CustomerId,
                    customer)

                _customerIdsByEmail.Add(
                    customer.Email,
                    customer.CustomerId)
            End SyncLock
        End Sub

        Public Function GetAll() _
            As IReadOnlyList(Of CustomerProfile) _
            Implements ICustomerRepository.GetAll

            SyncLock _syncRoot

                Return _customersById.Values _
                    .OrderBy(
                        Function(customer)
                            Return customer.Name.LastName
                        End Function) _
                    .ThenBy(
                        Function(customer)
                            Return customer.Name.FirstName
                        End Function) _
                    .ToList()
            End SyncLock
        End Function

        Public Function GetById(
            customerId As CustomerId) _
            As CustomerProfile _
            Implements ICustomerRepository.GetById

            SyncLock _syncRoot

                Dim customer As CustomerProfile = Nothing

                If _customersById.TryGetValue(
                    customerId,
                    customer) Then

                    Return customer
                End If
            End SyncLock

            Return Nothing
        End Function

        Public Function GetByEmail(
            email As EmailAddress) _
            As CustomerProfile _
            Implements ICustomerRepository.GetByEmail

            If email Is Nothing Then
                Throw New ArgumentNullException(
                    NameOf(email))
            End If

            SyncLock _syncRoot

                Dim customerId As CustomerId

                If Not _customerIdsByEmail.TryGetValue(
                    email,
                    customerId) Then

                    Return Nothing
                End If

                Return _customersById(customerId)
            End SyncLock
        End Function

        Public Function EmailExists(
            email As EmailAddress) As Boolean _
            Implements ICustomerRepository.EmailExists

            If email Is Nothing Then
                Throw New ArgumentNullException(
                    NameOf(email))
            End If

            SyncLock _syncRoot
                Return _customerIdsByEmail.ContainsKey(email)
            End SyncLock
        End Function

    End Class

End Namespace

