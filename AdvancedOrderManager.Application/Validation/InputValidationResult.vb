Option Explicit On
Option Strict On
Option Infer On

Imports System.Collections.Generic
Imports System.Linq

Public NotInheritable Class InputValidationResult

    Private ReadOnly _errors As IReadOnlyList(Of InputValidationError)

    Public Sub New(
        errors As IEnumerable(
            Of InputValidationError))

        ArgumentNullException.ThrowIfNull(
            errors)

        _errors =
            errors _
                .ToList() _
                .AsReadOnly()
    End Sub

    Public ReadOnly Property IsValid As Boolean

        Get
            Return _errors.Count = 0
        End Get

    End Property

    Public ReadOnly Property Errors As IReadOnlyList(Of InputValidationError)

        Get
            Return _errors
        End Get

    End Property

    Public Shared Function Success() _
        As InputValidationResult

        Return New InputValidationResult(
            Array.Empty(
                Of InputValidationError)())
    End Function

End Class

