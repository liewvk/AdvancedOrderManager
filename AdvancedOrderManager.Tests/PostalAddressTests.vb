Option Explicit On
Option Strict On
Option Infer On

Imports AdvancedOrderManager.Domain
Imports AdvancedOrderManager.Domain.ValueObjects
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass>
Public Class PostalAddressTests

    <TestMethod>
    Public Sub WithCity_ReturnsNewAddress()

        Dim original As New PostalAddress(
            "20 Jalan Ampang",
            "Kuala Lumpur",
            "50450",
            "Malaysia")

        Dim updated =
            original.WithCity(
                "Petaling Jaya",
                "46000")

        Assert.AreEqual(
            "Kuala Lumpur",
            original.City)

        Assert.AreEqual(
            "50450",
            original.PostalCode)

        Assert.AreEqual(
            "Petaling Jaya",
            updated.City)

        Assert.AreEqual(
            "46000",
            updated.PostalCode)

        Assert.AreNotSame(original, updated)
    End Sub

End Class

