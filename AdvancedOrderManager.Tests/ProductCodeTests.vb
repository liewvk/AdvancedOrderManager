Option Explicit On
Option Strict On
Option Infer On

Imports AdvancedOrderManager.Domain
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass>
Public Class ProductCodeTests

    <TestMethod>
    Public Sub Create_NormalisesCode()

        Dim code =
            ProductCode.Create(
                "  prd-00001  ")

        Assert.AreEqual(
            "PRD-00001",
            code.Value)
    End Sub

    <TestMethod>
    Public Sub EquivalentCodes_AreEqual()

        Dim first =
            ProductCode.Create(
                "prd-00001")

        Dim second =
            ProductCode.Create(
                "PRD-00001")

        Assert.AreEqual(first, second)
    End Sub

    <TestMethod>
    Public Sub InvalidCode_ThrowsException()

        Assert.Throws(Of ArgumentException)(
            Sub()
                ProductCode.Create(
                    "PRODUCT-1")
            End Sub)
    End Sub

End Class

