Option Explicit On
Option Strict On
Option Infer On

Public NotInheritable Class ExternalPostSubmissionResult

    Private Sub New(
        validationResult As InputValidationResult,
        createdPost As ExternalPost)

        ArgumentNullException.ThrowIfNull(
            validationResult)

        Me.ValidationResult =
            validationResult

        Me.CreatedPost =
            createdPost
    End Sub

    Public ReadOnly Property ValidationResult As InputValidationResult

    Public ReadOnly Property CreatedPost As ExternalPost

    Public ReadOnly Property WasSuccessful As Boolean

        Get
            Return ValidationResult.IsValid AndAlso
                   CreatedPost IsNot Nothing
        End Get

    End Property

    Public Shared Function ValidationFailed(
        validationResult As InputValidationResult) _
        As ExternalPostSubmissionResult

        ArgumentNullException.ThrowIfNull(
            validationResult)

        If validationResult.IsValid Then

            Throw New ArgumentException(
                "A failed submission requires an " &
                "invalid validation result.",
                NameOf(validationResult))

        End If

        Return New ExternalPostSubmissionResult(
            validationResult,
            Nothing)
    End Function

    Public Shared Function Success(
        createdPost As ExternalPost) _
        As ExternalPostSubmissionResult

        ArgumentNullException.ThrowIfNull(
            createdPost)

        Return New ExternalPostSubmissionResult(
            InputValidationResult.Success(),
            createdPost)
    End Function

End Class

