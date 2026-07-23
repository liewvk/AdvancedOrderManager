Option Explicit On
Option Strict On
Option Infer On

Namespace Domain.ValueObjects

    Public NotInheritable Class PostalAddress
        Implements IEquatable(Of PostalAddress)

        Public Sub New(
            addressLine As String,
            city As String,
            postalCode As String,
            country As String)

            Me.AddressLine =
                RequireText(
                    addressLine,
                    NameOf(addressLine))

            Me.City =
                RequireText(
                    city,
                    NameOf(city))

            Me.PostalCode =
                RequireText(
                    postalCode,
                    NameOf(postalCode))

            Me.Country =
                RequireText(
                    country,
                    NameOf(country))
        End Sub

        Public ReadOnly Property AddressLine As String

        Public ReadOnly Property City As String

        Public ReadOnly Property PostalCode As String

        Public ReadOnly Property Country As String

        Public ReadOnly Property FormattedAddress As String
            Get
                Return String.Join(
                    Environment.NewLine,
                    AddressLine,
                    $"{PostalCode} {City}",
                    Country)
            End Get
        End Property

        Public Function WithCity(
            newCity As String,
            newPostalCode As String) As PostalAddress

            Return New PostalAddress(
                AddressLine,
                newCity,
                newPostalCode,
                Country)
        End Function

        Public Function WithAddressLine(
            newAddressLine As String) As PostalAddress

            Return New PostalAddress(
                newAddressLine,
                City,
                PostalCode,
                Country)
        End Function

        Private Shared Function RequireText(
            value As String,
            parameterName As String) As String

            If String.IsNullOrWhiteSpace(value) Then
                Throw New ArgumentException(
                    "A value is required.",
                    parameterName)
            End If

            Return value.Trim()
        End Function

        Public Overloads Function Equals(
            other As PostalAddress) As Boolean _
            Implements IEquatable(Of PostalAddress).Equals

            If other Is Nothing Then
                Return False
            End If

            Return String.Equals(
                       AddressLine,
                       other.AddressLine,
                       StringComparison.OrdinalIgnoreCase) AndAlso
                   String.Equals(
                       City,
                       other.City,
                       StringComparison.OrdinalIgnoreCase) AndAlso
                   String.Equals(
                       PostalCode,
                       other.PostalCode,
                       StringComparison.OrdinalIgnoreCase) AndAlso
                   String.Equals(
                       Country,
                       other.Country,
                       StringComparison.OrdinalIgnoreCase)
        End Function

        Public Overrides Function Equals(
            obj As Object) As Boolean

            Return Equals(TryCast(obj, PostalAddress))
        End Function

        Public Overrides Function GetHashCode() As Integer

            Return HashCode.Combine(
                AddressLine.ToUpperInvariant(),
                City.ToUpperInvariant(),
                PostalCode.ToUpperInvariant(),
                Country.ToUpperInvariant())
        End Function

        Public Overrides Function ToString() As String
            Return FormattedAddress
        End Function

    End Class

End Namespace

