Option Explicit On
Option Strict On
Option Infer On

Imports AdvancedOrderManager.Domain
Imports AdvancedOrderManager.Domain.Entities


Public NotInheritable Class CustomerGridRow

    Public Sub New(customer As CustomerProfile)

        If customer Is Nothing Then
            Throw New ArgumentNullException(
                NameOf(customer))
        End If

        CustomerId = customer.CustomerId.ToString()
        FullName = customer.Name.FullName
        Email = customer.Email.Value
        City = customer.PostalAddress.City
        Country = customer.PostalAddress.Country
        Status = If(
            customer.IsActive,
            "Active",
            "Inactive")
    End Sub

    Public ReadOnly Property CustomerId As String

    Public ReadOnly Property FullName As String

    Public ReadOnly Property Email As String

    Public ReadOnly Property City As String

    Public ReadOnly Property Country As String

    Public ReadOnly Property Status As String
    Private _isActive As Boolean = True

    Public ReadOnly Property IsActive As Boolean
        Get
            Return _isActive
        End Get
    End Property

    Public Sub Activate()
        _isActive = True
    End Sub

    Public Sub Deactivate()
        _isActive = False
    End Sub
End Class


