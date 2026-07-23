Option Explicit On
Option Strict On
Option Infer On

Imports AdvancedOrderManager.Domain
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass>
Public Class OrderTests

    <TestMethod>
    Public Sub AddLine_IncreasesOrderTotal()

        Dim order As New Order("Alice Tan")

        order.AddLine(
            New OrderLine(
                "Wireless Keyboard",
                2,
                89.9D))

        order.AddLine(
            New OrderLine(
                "USB-C Mouse",
                1,
                45.5D))

        Assert.AreEqual(225.3D, order.Total)
        Assert.HasCount(2, order.Lines)
    End Sub

    <TestMethod>
    Public Sub Constructor_BlankCustomerName_ThrowsException()

        Assert.ThrowsExactly(Of ArgumentException)(
            Sub()
                Dim ignored As New Order(" ")
            End Sub)
    End Sub

    <TestMethod>
    Public Sub AddLine_Nothing_ThrowsException()

        Dim order As New Order("Alice Tan")

        Assert.ThrowsExactly(Of ArgumentNullException)(
            Sub()
                order.AddLine(Nothing)
            End Sub)
    End Sub

End Class

