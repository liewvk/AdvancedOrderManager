Option Explicit On
Option Strict On
Option Infer On

Namespace Application

    Public NotInheritable Class RegisterCustomerRequest

        Public Sub New(
            firstName As String,
            lastName As String,
            email As String,
            addressLine As String,
            city As String,
            postalCode As String,
            country As String)

            Me.FirstName = firstName
            Me.LastName = lastName
            Me.Email = email
            Me.AddressLine = addressLine
            Me.City = city
            Me.PostalCode = postalCode
            Me.Country = country
        End Sub

        Public ReadOnly Property FirstName As String

        Public ReadOnly Property LastName As String

        Public ReadOnly Property Email As String

        Public ReadOnly Property AddressLine As String

        Public ReadOnly Property City As String

        Public ReadOnly Property PostalCode As String

        Public ReadOnly Property Country As String

    End Class

End Namespace

