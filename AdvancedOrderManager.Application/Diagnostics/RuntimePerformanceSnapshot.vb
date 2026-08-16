Option Explicit On
Option Strict On
Option Infer On

Public NotInheritable Class RuntimePerformanceSnapshot

    Public Sub New(
        managedMemoryBytes As Long,
        workingSetBytes As Long,
        generation0Collections As Integer,
        generation1Collections As Integer,
        generation2Collections As Integer,
        processUptime As TimeSpan)

        If managedMemoryBytes < 0 Then

            Throw New ArgumentOutOfRangeException(
                NameOf(managedMemoryBytes))

        End If

        If workingSetBytes < 0 Then

            Throw New ArgumentOutOfRangeException(
                NameOf(workingSetBytes))

        End If

        Me.ManagedMemoryBytes =
            managedMemoryBytes

        Me.WorkingSetBytes =
            workingSetBytes

        Me.Generation0Collections =
            generation0Collections

        Me.Generation1Collections =
            generation1Collections

        Me.Generation2Collections =
            generation2Collections

        Me.ProcessUptime =
            processUptime

    End Sub

    Public ReadOnly Property ManagedMemoryBytes As Long

    Public ReadOnly Property WorkingSetBytes As Long

    Public ReadOnly Property Generation0Collections As Integer

    Public ReadOnly Property Generation1Collections As Integer

    Public ReadOnly Property Generation2Collections As Integer

    Public ReadOnly Property ProcessUptime As TimeSpan

End Class


