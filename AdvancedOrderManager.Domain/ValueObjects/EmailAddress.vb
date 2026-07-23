Option Explicit On
Option Strict On
Option Infer On

Imports System.Net.Mail

Namespace Domain.ValueObjects

    Public NotInheritable Class EmailAddress
        Implements IEquatable(Of EmailAddress)

        Private ReadOnly _value As String

        Private Sub New(value As String)
            _value = value
        End Sub

        Public Shared Function Create(
            input As String) As EmailAddress

            If String.IsNullOrWhiteSpace(input) Then
                Throw New ArgumentException(
                    "An email address is required.",
                    NameOf(input))
            End If

            Dim normalised As String =
                input.Trim().ToLowerInvariant()

            Dim parsedAddress As MailAddress

            Try
                parsedAddress = New MailAddress(normalised)
            Catch ex As FormatException
                Throw New ArgumentException(
                    "The email address format is invalid.",
                    NameOf(input),
                    ex)
            End Try

            If Not String.Equals(
                parsedAddress.Address,
                normalised,
                StringComparison.OrdinalIgnoreCase) Then

                Throw New ArgumentException(
                    "The email address format is invalid.",
                    NameOf(input))
            End If

            Return New EmailAddress(normalised)
        End Function

        Public ReadOnly Property Value As String
            Get
                Return _value
            End Get
        End Property

        Public ReadOnly Property Domain As String
            Get
                Dim separatorIndex As Integer =
                    _value.LastIndexOf("@"c)

                Return _value.Substring(separatorIndex + 1)
            End Get
        End Property

        Public Overloads Function Equals(
            other As EmailAddress) As Boolean _
            Implements IEquatable(Of EmailAddress).Equals

            If other Is Nothing Then
                Return False
            End If

            Return String.Equals(
                _value,
                other._value,
                StringComparison.OrdinalIgnoreCase)
        End Function

        Public Overrides Function Equals(
            obj As Object) As Boolean

            Return Equals(TryCast(obj, EmailAddress))
        End Function

        Public Overrides Function GetHashCode() As Integer

            Return StringComparer.OrdinalIgnoreCase _
                .GetHashCode(_value)
        End Function

        Public Overrides Function ToString() As String
            Return _value
        End Function

        Public Shared Operator =(
            left As EmailAddress,
            right As EmailAddress) As Boolean

            If ReferenceEquals(left, right) Then
                Return True
            End If

            If left Is Nothing OrElse right Is Nothing Then
                Return False
            End If

            Return left.Equals(right)
        End Operator

        Public Shared Operator <>(
            left As EmailAddress,
            right As EmailAddress) As Boolean

            Return Not left = right
        End Operator

    End Class

End Namespace

