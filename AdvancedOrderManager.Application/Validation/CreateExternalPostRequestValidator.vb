Option Explicit On
Option Strict On
Option Infer On

Imports System.Collections.Generic

Public NotInheritable Class CreateExternalPostRequestValidator
    Implements IInputValidator(
        Of CreateExternalPostRequest)

    Private Const MaximumTitleLength As Integer =
        200

    Private Const MaximumBodyLength As Integer =
        2000

    Public Function Validate(
        value As CreateExternalPostRequest) _
        As InputValidationResult _
        Implements IInputValidator(
            Of CreateExternalPostRequest).Validate

        Dim errors As New List(Of InputValidationError)()

        If value Is Nothing Then

            errors.Add(
                New InputValidationError(
                    "Request",
                    "A post request is required."))

            Return New InputValidationResult(
                errors)

        End If

        ValidateUserId(
            value.UserId,
            errors)

        ValidateTitle(
            value.Title,
            errors)

        ValidateBody(
            value.Body,
            errors)

        Return New InputValidationResult(
            errors)
    End Function

    Private Shared Sub ValidateUserId(
        userId As Integer,
        errors As ICollection(
            Of InputValidationError))

        If userId <= 0 Then

            errors.Add(
                New InputValidationError(
                    NameOf(
                        CreateExternalPostRequest.UserId),
                    "User ID must be greater than zero."))

        End If
    End Sub

    Private Shared Sub ValidateTitle(
        title As String,
        errors As ICollection(
            Of InputValidationError))

        If String.IsNullOrWhiteSpace(
            title) Then

            errors.Add(
                New InputValidationError(
                    NameOf(
                        CreateExternalPostRequest.Title),
                    "A post title is required."))

            Return
        End If

        Dim normalizedTitle As String =
            title.Trim()

        If normalizedTitle.Length >
           MaximumTitleLength Then

            errors.Add(
                New InputValidationError(
                    NameOf(
                        CreateExternalPostRequest.Title),
                    $"The title cannot exceed " &
                    $"{MaximumTitleLength} characters."))

            Return
        End If

        If normalizedTitle.Contains(
            ControlChars.Cr) OrElse
           normalizedTitle.Contains(
            ControlChars.Lf) Then

            errors.Add(
                New InputValidationError(
                    NameOf(
                        CreateExternalPostRequest.Title),
                    "The title must be a single line."))

        End If
    End Sub

    Private Shared Sub ValidateBody(
        body As String,
        errors As ICollection(
            Of InputValidationError))

        If String.IsNullOrWhiteSpace(
            body) Then

            errors.Add(
                New InputValidationError(
                    NameOf(
                        CreateExternalPostRequest.Body),
                    "Post content is required."))

            Return
        End If

        Dim normalizedBody As String =
            body.Trim()

        If normalizedBody.Length >
           MaximumBodyLength Then

            errors.Add(
                New InputValidationError(
                    NameOf(
                        CreateExternalPostRequest.Body),
                    $"Post content cannot exceed " &
                    $"{MaximumBodyLength} characters."))

        End If
    End Sub

End Class

