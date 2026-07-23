Option Explicit On
Option Strict On
Option Infer On

Namespace Domain.ValueObjects

    Public Structure CustomerId
        Implements IEquatable(Of CustomerId)

        Private ReadOnly _value As Guid

        Public Sub New(value As Guid)

            If value = Guid.Empty Then
                Throw New ArgumentException(
                    "A customer identifier cannot be empty.",
                    NameOf(value))
            End If

            _value = value
        End Sub

        Public Shared Function NewId() As CustomerId
            Return New CustomerId(Guid.NewGuid())
        End Function

        Public ReadOnly Property Value As Guid
            Get
                Return _value
            End Get
        End Property

        Public Overloads Function Equals(
            other As CustomerId) As Boolean _
            Implements IEquatable(Of CustomerId).Equals

            Return _value.Equals(other._value)
        End Function

        Public Overrides Function Equals(
            obj As Object) As Boolean

            If Not TypeOf obj Is CustomerId Then
                Return False
            End If

            Return Equals(DirectCast(obj, CustomerId))
        End Function

        Public Overrides Function GetHashCode() As Integer
            Return _value.GetHashCode()
        End Function

        Public Overrides Function ToString() As String
            Return _value.ToString()
        End Function

        Public Shared Operator =(
            left As CustomerId,
            right As CustomerId) As Boolean

            Return left.Equals(right)
        End Operator

        Public Shared Operator <>(
            left As CustomerId,
            right As CustomerId) As Boolean

            Return Not left.Equals(right)
        End Operator

    End Structure

End Namespace

