Option Explicit On
Option Strict On
Option Infer On

Imports AdvancedOrderManager.Domain
Imports AdvancedOrderManager.Domain.ValueObjects
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass>
Public Class EmailAddressTests

    <TestMethod>
    Public Sub Create_NormalisesEmailAddress()

        Dim email =
            EmailAddress.Create(
                "  ALICE@EXAMPLE.COM  ")

        Assert.AreEqual(
            "alice@example.com",
            email.Value)
    End Sub

    <TestMethod>
    Public Sub EquivalentAddresses_AreEqual()

        Dim first =
            EmailAddress.Create(
                "Alice@Example.com")

        Dim second =
            EmailAddress.Create(
                "alice@example.com")

        Assert.AreEqual(first, second)
        Assert.IsTrue(first = second)
    End Sub

    <TestMethod>
    Public Sub InvalidAddress_ThrowsException()

        Assert.ThrowsExactly(Of ArgumentException)(
            Sub()
                EmailAddress.Create(
                    "not-an-email-address")
            End Sub)
    End Sub

    <TestMethod>
    Public Sub Domain_ReturnsValueAfterAtSymbol()

        Dim email =
            EmailAddress.Create(
                "alice@example.com")

        Assert.AreEqual(
            "example.com",
            email.Domain)
    End Sub

End Class

