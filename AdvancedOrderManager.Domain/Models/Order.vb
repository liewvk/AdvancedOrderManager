Option Explicit On
Option Strict On
Option Infer On

Imports System.Linq

Namespace Domain

    Public NotInheritable Class Order

        Private ReadOnly _lines As New List(Of OrderLine)()

        Public Sub New(customerName As String)

            If String.IsNullOrWhiteSpace(customerName) Then
                Throw New ArgumentException(
                    "A customer name is required.",
                    NameOf(customerName))
            End If

            OrderId = Guid.NewGuid()
            customerName = customerName.Trim()
            CreatedAt = DateTimeOffset.Now
        End Sub

        Public ReadOnly Property OrderId As Guid

        Public ReadOnly Property CustomerName As String

        Public ReadOnly Property CreatedAt As DateTimeOffset

        Public ReadOnly Property Lines As IReadOnlyList(Of OrderLine)
            Get
                Return _lines.AsReadOnly()
            End Get
        End Property

        Public ReadOnly Property Total As Decimal
            Get
                Return _lines.Sum(
                    Function(item) item.LineTotal)
            End Get
        End Property

        Public Sub AddLine(line As OrderLine)

            If line Is Nothing Then
                Throw New ArgumentNullException(NameOf(line))
            End If

            _lines.Add(line)
        End Sub

    End Class

End Namespace

