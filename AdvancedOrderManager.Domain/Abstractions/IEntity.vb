Option Explicit On
Option Strict On
Option Infer On

Namespace Domain

    Public Interface IEntity(Of TKey)

        ReadOnly Property Id As TKey

    End Interface

End Namespace

