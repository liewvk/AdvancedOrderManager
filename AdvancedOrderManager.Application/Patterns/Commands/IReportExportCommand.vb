Option Explicit On
Option Strict On
Option Infer On

Imports System.Collections.Generic
Imports System.Threading
Imports System.Threading.Tasks

Public Interface IReportExportCommand

    ReadOnly Property FileFilter As String

    ReadOnly Property DefaultExtension As String

    Function ExecuteAsync(
        records As IReadOnlyList(
            Of OrderReportRecord),
        filePath As String,
        cancellationToken As CancellationToken) _
        As Task

End Interface

