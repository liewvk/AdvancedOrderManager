Option Explicit On
Option Strict On
Option Infer On

Namespace Application

    Public NotInheritable Class RegisterProductRequest

        Public Sub New(
            code As String,
            name As String,
            category As String,
            unitPrice As Decimal,
            openingStock As Integer,
            reorderLevel As Integer)

            Me.Code = code
            Me.Name = name
            Me.Category = category
            Me.UnitPrice = unitPrice
            Me.OpeningStock = openingStock
            Me.ReorderLevel = reorderLevel
        End Sub

        Public ReadOnly Property Code As String

        Public ReadOnly Property Name As String

        Public ReadOnly Property Category As String

        Public ReadOnly Property UnitPrice As Decimal

        Public ReadOnly Property OpeningStock As Integer

        Public ReadOnly Property ReorderLevel As Integer

    End Class

End Namespace

