Option Explicit On
Option Strict On
Option Infer On

Imports System
Imports System.Collections.Generic
Imports AdvancedOrderManager.Application.Contracts
Imports AdvancedOrderManager.Domain

Namespace Infrastructure

    Public NotInheritable Class InMemoryOrderRepository
        Implements IOrderRepository

        Private ReadOnly _orders As New Dictionary(Of Guid, Order)()
        Private ReadOnly _syncRoot As New Object()

        Public Sub Add(order As Order) _
            Implements IOrderRepository.Add

            If order Is Nothing Then
                Throw New ArgumentNullException(NameOf(order))
            End If

            SyncLock _syncRoot

                If _orders.ContainsKey(order.OrderId) Then
                    Throw New InvalidOperationException(
                        "The order already exists.")
                End If

                _orders.Add(order.OrderId, order)

            End SyncLock

        End Sub

        Public Function GetAll() As IReadOnlyCollection(Of Order) _
            Implements IOrderRepository.GetAll

            SyncLock _syncRoot
                Dim orders As New List(Of Order)(_orders.Values)
                Return orders.AsReadOnly()
            End SyncLock

        End Function

    End Class

End Namespace