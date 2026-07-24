Option Explicit On
Option Strict On
Option Infer On

Imports System

Imports AdvancedOrderManager.Domain.ValueObjects

Namespace Domain.Entities

    Public NotInheritable Class CustomerProfile
        Implements IEquatable(Of CustomerProfile)
        Implements IEntity(Of CustomerId)
        Public ReadOnly Property Id As CustomerId _
    Implements IEntity(Of CustomerId).Id

            Get
                Return CustomerId
            End Get
        End Property

        Private _name As PersonName
        Private _email As EmailAddress
        Private _postalAddress As PostalAddress
        Private _isActive As Boolean = True

        Public Sub New(
            customerId As CustomerId,
            name As PersonName,
            email As EmailAddress,
            postalAddress As PostalAddress)

            If customerId.Value = Guid.Empty Then
                Throw New ArgumentException(
                    "Customer ID cannot be empty.",
                    NameOf(customerId))
            End If

            If name Is Nothing Then
                Throw New ArgumentNullException(NameOf(name))
            End If

            If email Is Nothing Then
                Throw New ArgumentNullException(NameOf(email))
            End If

            If postalAddress Is Nothing Then
                Throw New ArgumentNullException(NameOf(postalAddress))
            End If

            Me.CustomerId = customerId
            Me.Name = name
            Me.Email = email
            Me.PostalAddress = postalAddress

        End Sub

        Public ReadOnly Property CustomerId As CustomerId

        Public Property Name As PersonName
            Get
                Return _name
            End Get

            Private Set(value As PersonName)

                If value Is Nothing Then
                    Throw New ArgumentNullException(NameOf(value))
                End If

                _name = value

            End Set
        End Property

        Public Property Email As EmailAddress
            Get
                Return _email
            End Get

            Private Set(value As EmailAddress)

                If value Is Nothing Then
                    Throw New ArgumentNullException(NameOf(value))
                End If

                _email = value

            End Set
        End Property

        Public Property PostalAddress As PostalAddress
            Get
                Return _postalAddress
            End Get

            Private Set(value As PostalAddress)

                If value Is Nothing Then
                    Throw New ArgumentNullException(NameOf(value))
                End If

                _postalAddress = value

            End Set
        End Property

        Public ReadOnly Property IsActive As Boolean
            Get
                Return _isActive
            End Get
        End Property

        Public Sub ChangeName(newName As PersonName)

            If newName Is Nothing Then
                Throw New ArgumentNullException(NameOf(newName))
            End If

            Name = newName

        End Sub

        Public Sub ChangeEmail(newEmail As EmailAddress)

            If newEmail Is Nothing Then
                Throw New ArgumentNullException(NameOf(newEmail))
            End If

            Email = newEmail

        End Sub

        Public Sub ChangePostalAddress(
            newAddress As PostalAddress)

            If newAddress Is Nothing Then
                Throw New ArgumentNullException(NameOf(newAddress))
            End If

            PostalAddress = newAddress

        End Sub

        Public Sub Activate()
            _isActive = True
        End Sub

        Public Sub Deactivate()
            _isActive = False
        End Sub

        Public Overloads Function Equals(
            other As CustomerProfile) As Boolean _
            Implements IEquatable(Of CustomerProfile).Equals

            If other Is Nothing Then
                Return False
            End If

            Return CustomerId.Equals(other.CustomerId)

        End Function

        Public Overrides Function Equals(
            obj As Object) As Boolean

            If Not TypeOf obj Is CustomerProfile Then
                Return False
            End If

            Return Equals(
                DirectCast(obj, CustomerProfile))

        End Function

        Public Overrides Function GetHashCode() As Integer
            Return CustomerId.GetHashCode()
        End Function

        Public Shared Operator =(
            left As CustomerProfile,
            right As CustomerProfile) As Boolean

            If ReferenceEquals(left, right) Then
                Return True
            End If

            If left Is Nothing OrElse right Is Nothing Then
                Return False
            End If

            Return left.Equals(right)

        End Operator

        Public Shared Operator <>(
            left As CustomerProfile,
            right As CustomerProfile) As Boolean

            Return Not (left = right)

        End Operator

    End Class

End Namespace