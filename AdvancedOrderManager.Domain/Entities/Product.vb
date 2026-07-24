Option Explicit On
Option Strict On
Option Infer On

Namespace Domain

    Public NotInheritable Class Product
        Implements IEntity(Of ProductId)

        Private ReadOnly _stockmovements As New List(Of StockMovement)()

        Private _quantityInStock As Integer

        ' Add backing fields for properties with private setters
        Private _name As String
        Private _category As String
        Private _unitPrice As Decimal
        Private _reorderLevel As Integer
        Private _isActive As Boolean

        Public Sub New(
            productId As ProductId,
            code As ProductCode,
            name As String,
            category As String,
            unitPrice As Decimal,
            openingStock As Integer,
            reorderLevel As Integer)

            If code Is Nothing Then
                Throw New ArgumentNullException(
                    NameOf(code))
            End If

            Me.Name = ValidateText(
                name,
                NameOf(name))

            Me.Category = ValidateText(
                category,
                NameOf(category))

            If unitPrice < 0D Then
                Throw New ArgumentOutOfRangeException(
                    NameOf(unitPrice),
                    "The unit price cannot be negative.")
            End If

            If openingStock < 0 Then
                Throw New ArgumentOutOfRangeException(
                    NameOf(openingStock),
                    "Opening stock cannot be negative.")
            End If

            If reorderLevel < 0 Then
                Throw New ArgumentOutOfRangeException(
                    NameOf(reorderLevel),
                    "The reorder level cannot be negative.")
            End If

            Me.ProductId = productId
            Me.Code = code
            Me.UnitPrice = unitPrice
            Me.ReorderLevel = reorderLevel

            _quantityInStock = openingStock
            IsActive = True

            If openingStock > 0 Then

                _stockmovements.Add(
                    New StockMovement(
                        openingStock,
                        openingStock,
                        "Opening stock"))
            End If
        End Sub

        Public ReadOnly Property ProductId As ProductId

        Public ReadOnly Property Id As ProductId _
            Implements IEntity(Of ProductId).Id

            Get
                Return ProductId
            End Get
        End Property

        Public ReadOnly Property Code As ProductCode

        Public Property Name As String
            Get
                Return _name
            End Get
            Private Set(value As String)
                _name = value
            End Set
        End Property

        Public Property Category As String
            Get
                Return _category
            End Get
            Private Set(value As String)
                _category = value
            End Set
        End Property

        Public Property UnitPrice As Decimal
            Get
                Return _unitPrice
            End Get
            Private Set(value As Decimal)
                _unitPrice = value
            End Set
        End Property

        Public Property ReorderLevel As Integer
            Get
                Return _reorderLevel
            End Get
            Private Set(value As Integer)
                _reorderLevel = value
            End Set
        End Property

        Public Property IsActive As Boolean
            Get
                Return _isActive
            End Get
            Private Set(value As Boolean)
                _isActive = value
            End Set
        End Property

        Public ReadOnly Property QuantityInStock As Integer
            Get
                Return _quantityInStock
            End Get
        End Property

        Public ReadOnly Property StockValue As Decimal
            Get
                Return UnitPrice * _quantityInStock
            End Get
        End Property

        Public ReadOnly Property StockStatus As StockLevelStatus
            Get
                If _quantityInStock = 0 Then
                    Return StockLevelStatus.OutOfStock
                End If

                If _quantityInStock <= ReorderLevel Then
                    Return StockLevelStatus.LowStock
                End If

                Return StockLevelStatus.InStock
            End Get
        End Property

        Public ReadOnly Property NeedsRestock As Boolean
            Get
                Return StockStatus =
                       StockLevelStatus.OutOfStock OrElse
                       StockStatus =
                       StockLevelStatus.LowStock
            End Get
        End Property

        Public ReadOnly Property Movements As IReadOnlyList(Of StockMovement)
            Get
                Return _stockmovements.AsReadOnly()
            End Get
        End Property

        Public Sub Rename(newName As String)

            Name = ValidateText(
                newName,
                NameOf(newName))
        End Sub

        Public Sub ChangeCategory(
            newCategory As String)

            Category = ValidateText(
                newCategory,
                NameOf(newCategory))
        End Sub

        Public Sub ChangePrice(
            newUnitPrice As Decimal)

            If newUnitPrice < 0D Then
                Throw New ArgumentOutOfRangeException(
                    NameOf(newUnitPrice),
                    "The unit price cannot be negative.")
            End If

            UnitPrice = newUnitPrice
        End Sub

        Public Sub ChangeReorderLevel(
            newReorderLevel As Integer)

            If newReorderLevel < 0 Then
                Throw New ArgumentOutOfRangeException(
                    NameOf(newReorderLevel),
                    "The reorder level cannot be negative.")
            End If

            ReorderLevel = newReorderLevel
        End Sub

        Public Sub AdjustStock(
            quantityChange As Integer,
            reason As String)

            If quantityChange = 0 Then
                Throw New ArgumentException(
                    "The stock adjustment cannot be zero.",
                    NameOf(quantityChange))
            End If

            If String.IsNullOrWhiteSpace(reason) Then
                Throw New ArgumentException(
                    "A reason is required.",
                    NameOf(reason))
            End If

            Dim candidateQuantity As Long =
                CLng(_quantityInStock) +
                CLng(quantityChange)

            If candidateQuantity < 0 Then
                Throw New InvalidOperationException(
                    "The adjustment would make stock negative.")
            End If

            If candidateQuantity > Integer.MaxValue Then
                Throw New OverflowException(
                    "The resulting stock quantity is too large.")
            End If

            _quantityInStock =
                CInt(candidateQuantity)

            _stockmovements.Add(
                New StockMovement(
                    quantityChange,
                    _quantityInStock,
                    reason))
        End Sub

        Public Sub Deactivate()
            IsActive = False
        End Sub

        Public Sub Reactivate()
            IsActive = True
        End Sub

        Private Shared Function ValidateText(
            value As String,
            parameterName As String) As String

            If String.IsNullOrWhiteSpace(value) Then
                Throw New ArgumentException(
                    "A value is required.",
                    parameterName)
            End If

            Return value.Trim()
        End Function

        Public Overrides Function Equals(
            obj As Object) As Boolean

            Dim other As Product =
                TryCast(obj, Product)

            If other Is Nothing Then
                Return False
            End If

            Return ProductId = other.ProductId
        End Function

        Public Overrides Function GetHashCode() As Integer
            Return ProductId.GetHashCode()
        End Function

        Public Overrides Function ToString() As String

            Return $"{Code} - {Name}"
        End Function

    End Class

End Namespace