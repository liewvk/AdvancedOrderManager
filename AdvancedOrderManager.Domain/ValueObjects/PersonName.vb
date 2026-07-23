Option Explicit On
Option Strict On
Option Infer On

Namespace Domain.ValueObjects

    Public NotInheritable Class PersonName
        Implements IEquatable(Of PersonName)

        Public Sub New(
            firstName As String,
            lastName As String)

            firstName = ValidateNamePart(
                firstName,
                NameOf(firstName))

            lastName = ValidateNamePart(
                lastName,
                NameOf(lastName))
        End Sub

        Public ReadOnly Property FirstName As String

        Public ReadOnly Property LastName As String

        Public ReadOnly Property FullName As String
            Get
                Return $"{FirstName} {LastName}"
            End Get
        End Property

        Private Shared Function ValidateNamePart(
            value As String,
            parameterName As String) As String

            If String.IsNullOrWhiteSpace(value) Then
                Throw New ArgumentException(
                    "A name value is required.",
                    parameterName)
            End If

            Dim cleaned As String = value.Trim()

            If cleaned.Length < 2 Then
                Throw New ArgumentException(
                    "Each name must contain at least two characters.",
                    parameterName)
            End If

            If cleaned.Any(AddressOf Char.IsDigit) Then
                Throw New ArgumentException(
                    "A name cannot contain numeric digits.",
                    parameterName)
            End If

            Return cleaned
        End Function

        Public Overloads Function Equals(
            other As PersonName) As Boolean _
            Implements IEquatable(Of PersonName).Equals

            If other Is Nothing Then
                Return False
            End If

            Return String.Equals(
                       FirstName,
                       other.FirstName,
                       StringComparison.OrdinalIgnoreCase) AndAlso
                   String.Equals(
                       LastName,
                       other.LastName,
                       StringComparison.OrdinalIgnoreCase)
        End Function

        Public Overrides Function Equals(
            obj As Object) As Boolean

            Return Equals(TryCast(obj, PersonName))
        End Function

        Public Overrides Function GetHashCode() As Integer

            Return HashCode.Combine(
                StringComparer.OrdinalIgnoreCase _
                    .GetHashCode(FirstName),
                StringComparer.OrdinalIgnoreCase _
                    .GetHashCode(LastName))
        End Function

        Public Overrides Function ToString() As String
            Return FullName
        End Function

    End Class

End Namespace

