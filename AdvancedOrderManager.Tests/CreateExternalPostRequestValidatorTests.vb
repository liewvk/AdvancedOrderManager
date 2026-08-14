Option Explicit On
Option Strict On
Option Infer On

Imports AdvancedOrderManager.Application
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass>
<TestCategory("Unit")>
Public Class CreateExternalPostRequestValidatorTests

    Private _validator As CreateExternalPostRequestValidator

    <TestInitialize>
    Public Sub Initialise()

        _validator =
            New CreateExternalPostRequestValidator()

    End Sub

    <TestMethod>
    Public Sub Validate_ValidRequest_IsValid()

        Dim request =
            New CreateExternalPostRequest(
                1,
                "Monthly order report",
                "The demonstration post contains valid data.")

        Dim result =
            _validator.Validate(
                request)

        Assert.IsTrue(
            result.IsValid)

        Assert.HasCount(
            0,
            result.Errors)

    End Sub
    <TestMethod>
    Public Sub Validate_MultipleInvalidFields_ReturnsMultipleErrors()

        Dim request =
        New CreateExternalPostRequest(
            0,
            String.Empty,
            String.Empty)

        Dim result =
        _validator.Validate(
            request)

        Assert.IsFalse(
        result.IsValid)

        Assert.HasCount(
        3,
        result.Errors)

    End Sub
    <TestMethod>
    <DataRow(199, True)>
    <DataRow(200, True)>
    <DataRow(201, False)>
    Public Sub Validate_TitleLengthBoundary_ReturnsExpectedResult(
    titleLength As Integer,
    expectedIsValid As Boolean)

        'Arrange

        Dim title As String =
        New String(
            "X"c,
            titleLength)

        Dim request =
        New CreateExternalPostRequest(
            1,
            title,
            "Valid body")

        'Act

        Dim result =
        _validator.Validate(
            request)

        'Assert

        Assert.AreEqual(
        expectedIsValid,
        result.IsValid)

    End Sub
    <TestMethod>
    <DataRow(1999, True)>
    <DataRow(2000, True)>
    <DataRow(2001, False)>
    Public Sub Validate_BodyLengthBoundary_ReturnsExpectedResult(
    bodyLength As Integer,
    expectedIsValid As Boolean)

        'Arrange

        Dim body As String =
        New String(
            "B"c,
            bodyLength)

        Dim request =
        New CreateExternalPostRequest(
            1,
            "Valid title",
            body)

        'Act

        Dim result =
        _validator.Validate(
            request)

        'Assert

        Assert.AreEqual(
        expectedIsValid,
        result.IsValid)

    End Sub


    <TestMethod>
    Public Sub Validate_UserIdIsZero_ReturnsError()

        Dim request =
            New CreateExternalPostRequest(
                0,
                "Valid title",
                "Valid body")

        Dim result =
            _validator.Validate(
                request)

        Assert.IsFalse(
            result.IsValid)

        Assert.IsTrue(
            result.Errors.Any(
                Function(item)
                    Return item.FieldName =
                        NameOf(
                            CreateExternalPostRequest.UserId)
                End Function))

    End Sub

    <TestMethod>
    Public Sub Validate_TitleIsBlank_ReturnsError()

        Dim request =
            New CreateExternalPostRequest(
                1,
                "   ",
                "Valid body")

        Dim result =
            _validator.Validate(
                request)

        Assert.IsFalse(
            result.IsValid)

        Assert.IsTrue(
            result.Errors.Any(
                Function(item)
                    Return item.FieldName =
                        NameOf(
                            CreateExternalPostRequest.Title)
                End Function))

    End Sub

    <TestMethod>
    Public Sub Validate_TitleTooLong_ReturnsError()

        Dim longTitle As String =
            New String(
                "X"c,
                201)

        Dim request =
            New CreateExternalPostRequest(
                1,
                longTitle,
                "Valid body")

        Dim result =
            _validator.Validate(
                request)

        Assert.IsFalse(
            result.IsValid)

        Assert.IsTrue(
            result.Errors.Any(
                Function(item)
                    Return item.FieldName =
                        NameOf(
                            CreateExternalPostRequest.Title)
                End Function))

    End Sub

    <TestMethod>
    Public Sub Validate_TitleContainsNewLine_ReturnsError()

        Dim request =
            New CreateExternalPostRequest(
                1,
                "First line" &
                Environment.NewLine &
                "Second line",
                "Valid body")

        Dim result =
            _validator.Validate(
                request)

        Assert.IsFalse(
            result.IsValid)

        Assert.IsTrue(
            result.Errors.Any(
                Function(item)
                    Return item.FieldName =
                        NameOf(
                            CreateExternalPostRequest.Title)
                End Function))

    End Sub

    <TestMethod>
    Public Sub Validate_BodyIsBlank_ReturnsError()

        Dim request =
            New CreateExternalPostRequest(
                1,
                "Valid title",
                String.Empty)

        Dim result =
            _validator.Validate(
                request)

        Assert.IsFalse(
            result.IsValid)

        Assert.IsTrue(
            result.Errors.Any(
                Function(item)
                    Return item.FieldName =
                        NameOf(
                            CreateExternalPostRequest.Body)
                End Function))

    End Sub

    <TestMethod>
    Public Sub Validate_BodyTooLong_ReturnsError()

        Dim longBody As String =
            New String(
                "B"c,
                2001)

        Dim request =
            New CreateExternalPostRequest(
                1,
                "Valid title",
                longBody)

        Dim result =
            _validator.Validate(
                request)

        Assert.IsFalse(
            result.IsValid)

        Assert.IsTrue(
            result.Errors.Any(
                Function(item)
                    Return item.FieldName =
                        NameOf(
                            CreateExternalPostRequest.Body)
                End Function))

    End Sub

End Class

