Option Explicit On
Option Strict On
Option Infer On

Imports System.Collections.Generic
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Text
Imports System.Text.Encodings.Web
Imports System.Text.Json
Imports System.Threading
Imports System.Threading.Tasks
Imports AdvancedOrderManager.Application

Public NotInheritable Class OrderReportExporter
    Implements IOrderReportExporter

    Private Shared ReadOnly JsonOptions As New JsonSerializerOptions() With {
            .WriteIndented = True
        }

    Public Async Function ExportCsvAsync(
        records As IReadOnlyList(Of OrderReportRecord),
        filePath As String,
        cancellationToken As CancellationToken) _
        As Task _
        Implements IOrderReportExporter.ExportCsvAsync

        ValidateArguments(
            records,
            filePath)

        Dim encoding =
            New UTF8Encoding(
                encoderShouldEmitUTF8Identifier:=True)

        Using writer As New StreamWriter(
                filePath,
                append:=False,
                encoding:=encoding)

            Await writer.WriteLineAsync(
                "OrderNumber,CustomerName,Status," &
                "TotalAmount,Priority,Message,OccurredAtUtc")

            For Each record In records

                cancellationToken _
                    .ThrowIfCancellationRequested()

                Dim values =
                    New String() {
                        record.OrderNumber,
                        record.CustomerName,
                        record.Status.ToString(),
                        record.TotalAmount.ToString(
                            "0.00",
                            CultureInfo.InvariantCulture),
                        record.IsPriority.ToString(),
                        record.Message,
                        record.OccurredAtUtc.ToString(
                            "O",
                            CultureInfo.InvariantCulture)
                    }

                Dim escapedValues =
                    values.Select(
                        Function(value)

                            Return EscapeCsv(value)
                        End Function)

                Await writer.WriteLineAsync(
                    String.Join(
                        ",",
                        escapedValues))
            Next
        End Using
    End Function

    Private Shared Function EscapeCsv(
        value As String) As String

        Dim safeValue =
            If(value, String.Empty)

        Dim requiresQuotes =
            safeValue.Contains(","c) OrElse
            safeValue.Contains(""""c) OrElse
            safeValue.Contains(ControlChars.Cr) OrElse
            safeValue.Contains(ControlChars.Lf)

        If Not requiresQuotes Then
            Return safeValue
        End If

        Dim quote As Char =
            """"c

        Dim doubledQuote =
            New String(
                quote,
                2)

        Dim escaped =
            safeValue.Replace(
                quote.ToString(),
                doubledQuote)

        Return quote.ToString() &
               escaped &
               quote.ToString()
    End Function
    Public Async Function ExportJsonAsync(
        records As IReadOnlyList(Of OrderReportRecord),
        filePath As String,
        cancellationToken As CancellationToken) _
        As Task _
        Implements IOrderReportExporter.ExportJsonAsync

        ValidateArguments(
            records,
            filePath)

        Using stream As New FileStream(
                filePath,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None,
                bufferSize:=4096,
                useAsync:=True)

            Await JsonSerializer.SerializeAsync(
                Of IReadOnlyList(
                    Of OrderReportRecord))(
                        stream,
                        records,
                        JsonOptions,
                        cancellationToken)
        End Using
    End Function
    Public Async Function ExportHtmlAsync(
        records As IReadOnlyList(Of OrderReportRecord),
        filePath As String,
        cancellationToken As CancellationToken) _
        As Task _
        Implements IOrderReportExporter.ExportHtmlAsync

        ValidateArguments(
            records,
            filePath)

        Dim summary =
            OrderReportSummary.Create(
                records)

        Dim encoder =
            HtmlEncoder.Default

        Dim html As New StringBuilder()

        html.AppendLine(
            "<!DOCTYPE html>")

        html.AppendLine(
            "<html lang=""en"">")

        html.AppendLine(
            "<head>")

        html.AppendLine(
            "<meta charset=""utf-8"">")

        html.AppendLine(
            "<meta name=""viewport"" " &
            "content=""width=device-width, initial-scale=1"">")

        html.AppendLine(
            "<title>Order Processing Report</title>")
        html.AppendLine(
            "<style>")

        html.AppendLine(
            "body{font-family:Arial,sans-serif;" &
            "margin:32px;color:#222;}")

        html.AppendLine(
            "h1{margin-bottom:4px;}")

        html.AppendLine(
            ".generated{color:#666;margin-top:0;}")

        html.AppendLine(
            ".summary{display:flex;gap:16px;" &
            "flex-wrap:wrap;margin:24px 0;}")

        html.AppendLine(
            ".card{border:1px solid #ccc;" &
            "border-radius:6px;padding:12px 18px;" &
            "min-width:150px;}")

        html.AppendLine(
            "table{border-collapse:collapse;width:100%;}")

        html.AppendLine(
            "th,td{border:1px solid #ccc;" &
            "padding:8px;text-align:left;}")

        html.AppendLine(
            "th{background:#eee;}")

        html.AppendLine(
            ".number{text-align:right;}")

        html.AppendLine(
            "</style>")

        html.AppendLine(
            "</head>")

        html.AppendLine(
            "<body>")

        html.AppendLine(
            "<h1>Order Processing Report</h1>")

        html.AppendLine(
            $"<p class=""generated"">Generated: " &
            $"{DateTimeOffset.Now:dd MMM yyyy HH:mm:ss}</p>")
        html.AppendLine(
            "<div class=""summary"">")

        AppendSummaryCard(
            html,
            "Records",
            summary.TotalRecords.ToString("N0"))

        AppendSummaryCard(
            html,
            "Processed",
            summary.ProcessedCount.ToString("N0"))

        AppendSummaryCard(
            html,
            "Rejected",
            summary.RejectedCount.ToString("N0"))

        AppendSummaryCard(
            html,
            "Revenue",
            $"RM{summary.TotalRevenue:N2}")

        html.AppendLine(
            "</div>")
        html.AppendLine(
            "<table>")

        html.AppendLine(
            "<thead><tr>" &
            "<th>Date</th>" &
            "<th>Order</th>" &
            "<th>Customer</th>" &
            "<th>Status</th>" &
            "<th>Priority</th>" &
            "<th>Total</th>" &
            "<th>Message</th>" &
            "</tr></thead>")

        html.AppendLine(
            "<tbody>")

        For Each record In records

            cancellationToken _
                .ThrowIfCancellationRequested()

            html.AppendLine(
                "<tr>")

            AppendTableCell(
                html,
                encoder.Encode(
                    record.OccurredAtUtc _
                        .ToLocalTime() _
                        .ToString(
                            "dd MMM yyyy HH:mm:ss")),
                cssClass:=String.Empty)

            AppendTableCell(
                html,
                encoder.Encode(
                    record.OrderNumber),
                cssClass:=String.Empty)

            AppendTableCell(
                html,
                encoder.Encode(
                    record.CustomerName),
                cssClass:=String.Empty)

            AppendTableCell(
                html,
                encoder.Encode(
                    record.Status.ToString()),
                cssClass:=String.Empty)

            AppendTableCell(
                html,
                If(
                    record.IsPriority,
                    "Yes",
                    "No"),
                cssClass:=String.Empty)

            AppendTableCell(
                html,
                If(
                    record.Status =
                    OrderReportStatus.Processed,
                    $"RM{record.TotalAmount:N2}",
                    String.Empty),
                cssClass:="number")

            AppendTableCell(
                html,
                encoder.Encode(
                    record.Message),
                cssClass:=String.Empty)

            html.AppendLine(
                "</tr>")
        Next

        html.AppendLine(
            "</tbody></table>")

        html.AppendLine(
            "</body></html>")

        Await File.WriteAllTextAsync(
            filePath,
            html.ToString(),
            Encoding.UTF8,
            cancellationToken)
    End Function
    Private Shared Sub AppendSummaryCard(
        builder As StringBuilder,
        title As String,
        value As String)

        builder.AppendLine(
            "<div class=""card"">")

        builder.AppendLine(
            $"<strong>{title}</strong><br>")

        builder.AppendLine(
            value)

        builder.AppendLine(
            "</div>")
    End Sub

    Private Shared Sub AppendTableCell(
        builder As StringBuilder,
        value As String,
        cssClass As String)

        Dim classAttribute =
            If(
                String.IsNullOrWhiteSpace(
                    cssClass),
                String.Empty,
                $" class=""{cssClass}""")

        builder.AppendLine(
            $"<td{classAttribute}>{value}</td>")
    End Sub

    Private Shared Sub ValidateArguments(
        records As IReadOnlyList(Of OrderReportRecord),
        filePath As String)

        If records Is Nothing Then

            Throw New ArgumentNullException(
                NameOf(records))
        End If

        If String.IsNullOrWhiteSpace(
            filePath) Then

            Throw New ArgumentException(
                "An export file path is required.",
                NameOf(filePath))
        End If
    End Sub

End Class
