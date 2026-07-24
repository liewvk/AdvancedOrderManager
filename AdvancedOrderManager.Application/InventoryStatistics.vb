Option Explicit On
Option Strict On
Option Infer On

Namespace Application

    Public NotInheritable Class InventoryStatistics

        Public Sub New(
            productCount As Integer,
            activeProductCount As Integer,
            lowStockCount As Integer,
            totalStockUnits As Long,
            totalStockValue As Decimal,
            productsByCategory As IReadOnlyDictionary(Of String, Integer))

            Me.ProductCount = productCount
            Me.ActiveProductCount = activeProductCount
            Me.LowStockCount = lowStockCount
            Me.TotalStockUnits = totalStockUnits
            Me.TotalStockValue = totalStockValue
            Me.ProductsByCategory = productsByCategory
        End Sub

        Public ReadOnly Property ProductCount As Integer

        Public ReadOnly Property ActiveProductCount As Integer

        Public ReadOnly Property LowStockCount As Integer

        Public ReadOnly Property TotalStockUnits As Long

        Public ReadOnly Property TotalStockValue As Decimal

        Public ReadOnly Property ProductsByCategory As IReadOnlyDictionary(Of String, Integer)

    End Class

End Namespace

