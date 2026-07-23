Option Explicit On
Option Strict On
Option Infer On

Imports AdvancedOrderManager.Domain
Imports AdvancedOrderManager.Domain.ValueObjects
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass>
Public Class CustomerIdTests

    <TestMethod>
    Public Sub SameGuid_ProducesEqualCustomerIds()

        Dim value As Guid = Guid.NewGuid()

        Dim first As New CustomerId(value)
        Dim second As New CustomerId(value)

        Assert.AreEqual(first, second)
        Assert.IsTrue(first = second)
        Assert.AreEqual(
            first.GetHashCode(),
            second.GetHashCode())
    End Sub

    <TestMethod>
    Public Sub DifferentGuids_ProduceDifferentCustomerIds()

        Dim first As CustomerId =
            CustomerId.NewId()

        Dim second As CustomerId =
            CustomerId.NewId()

        Assert.AreNotEqual(first, second)
        Assert.IsTrue(first <> second)
    End Sub


    <TestMethod>
    Public Sub EmptyGuid_ThrowsException()

        Assert.ThrowsExactly(Of ArgumentException)(
        Sub()
            Dim ignored As New CustomerId(Guid.Empty)
        End Sub)

    End Sub

End Class
