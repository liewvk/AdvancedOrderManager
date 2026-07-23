Option Explicit On
Option Strict On
Option Infer On

Namespace Application

    Public NotInheritable Class OperationResult(Of T)

        Private Sub New(
            isSuccess As Boolean,
            value As T,
            errors As IReadOnlyList(Of String))

            Me.IsSuccess = isSuccess
            Me.Value = value
            Me.Errors = errors
        End Sub

        Public ReadOnly Property IsSuccess As Boolean

        Public ReadOnly Property Value As T

        Public ReadOnly Property Errors As IReadOnlyList(Of String)

        Public ReadOnly Property ErrorMessage As String
            Get
                Return String.Join(
                    Environment.NewLine,
                    Errors)
            End Get
        End Property

        Public Shared Function Success(
            value As T) As OperationResult(Of T)

            Return New OperationResult(Of T)(
                True,
                value,
                Array.Empty(Of String)())
        End Function

        Public Shared Function Failure(
            ParamArray errors() As String) _
            As OperationResult(Of T)

            If errors Is Nothing OrElse errors.Length = 0 Then
                Throw New ArgumentException(
                    "At least one error is required.",
                    NameOf(errors))
            End If

            Return New OperationResult(Of T)(
                False,
                Nothing,
                Array.AsReadOnly(errors))
        End Function

    End Class

End Namespace

