Option Explicit On
Option Strict On
Option Infer On

Imports AdvancedOrderManager.Domain
Imports AdvancedOrderManager.Domain.Entities
Imports AdvancedOrderManager.Domain.ValueObjects
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass>
Public Class CustomerProfileTests

    Private Shared Function CreateAddress() _
        As PostalAddress

        Return New PostalAddress(
            "20 Jalan Ampang",
            "Kuala Lumpur",
            "50450",
            "Malaysia")
    End Function

    <TestMethod>
    Public Sub SameCustomerId_EntitiesAreEqual()

        Dim id As CustomerId =
            CustomerId.NewId()

        Dim first As New CustomerProfile(
            id,
            New PersonName("Alice", "Tan"),
            EmailAddress.Create(
                "alice@example.com"),
            CreateAddress())

        Dim second As New CustomerProfile(
            id,
            New PersonName("Alice", "Lim"),
            EmailAddress.Create(
                "alice.new@example.com"),
            CreateAddress())

        Assert.AreEqual(first, second)
    End Sub

    <TestMethod>
    Public Sub ChangeEmail_ReplacesEmailValue()

        Dim customer As New CustomerProfile(
            CustomerId.NewId(),
            New PersonName("Alice", "Tan"),
            EmailAddress.Create(
                "alice@example.com"),
            CreateAddress())

        customer.ChangeEmail(
            EmailAddress.Create(
                "new@example.com"))

        Assert.AreEqual(
            "new@example.com",
            customer.Email.Value)
    End Sub

    <TestMethod>
    Public Sub Deactivate_ChangesCustomerStatus()

        Dim customer As New CustomerProfile(
            CustomerId.NewId(),
            New PersonName("Alice", "Tan"),
            EmailAddress.Create(
                "alice@example.com"),
            CreateAddress())

        customer.Deactivate()

        Assert.IsFalse(customer.IsActive)
    End Sub

End Class

