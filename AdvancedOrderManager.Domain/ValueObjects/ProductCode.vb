Option Explicit On
Option Strict On
Option Infer On

Imports System.Text.RegularExpressions

Namespace Domain

    Public NotInheritable Class ProductCode
        Implements IEquatable(Of ProductCode)

        Private Shared ReadOnly CodePattern As New Regex(
                "^[A-Z]{3}-\d{5}$",
                RegexOptions.Compiled Or
                RegexOptions.CultureInvariant)

        Private ReadOnly _value As String

        Private Sub New(value As String)
            _value = value
        End Sub

        Public Shared Function Create(
            input As String) As ProductCode

            If String.IsNullOrWhiteSpace(input) Then
                Throw New ArgumentException(
                    "A product code is required.",
                    NameOf(input))
            End If

            Dim normalised As String =
                input.Trim().ToUpperInvariant()

            If Not CodePattern.IsMatch(normalised) Then
                Throw New ArgumentException(
                    "The product code must use the format ABC-12345.",
                    NameOf(input))
            End If

            Return New ProductCode(normalised)
        End Function

        Public ReadOnly Property Value As String
            Get
                Return _value
            End Get
        End Property

        Public Overloads Function Equals(
            other As ProductCode) As Boolean _
            Implements IEquatable(Of ProductCode).Equals

            If other Is Nothing Then
                Return False
            End If

            Return String.Equals(
                _value,
                other._value,
                StringComparison.Ordinal)
        End Function

        Public Overrides Function Equals(
            obj As Object) As Boolean

            Return Equals(
                TryCast(obj, ProductCode))
        End Function

        Public Overrides Function GetHashCode() As Integer

            Return StringComparer.Ordinal _
                .GetHashCode(_value)
        End Function

        Public Overrides Function ToString() As String
            Return _value
        End Function

        Public Shared Operator =(
            left As ProductCode,
            right As ProductCode) As Boolean

            If ReferenceEquals(left, right) Then
                Return True
            End If

            If left Is Nothing OrElse right Is Nothing Then
                Return False
            End If

            Return left.Equals(right)
        End Operator

        Public Shared Operator <>(
            left As ProductCode,
            right As ProductCode) As Boolean

            Return Not left = right
        End Operator

    End Class

End Namespace

