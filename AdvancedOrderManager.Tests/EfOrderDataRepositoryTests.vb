Option Explicit On
Option Strict On
Option Infer On

Imports System.Threading
Imports System.Threading.Tasks
Imports AdvancedOrderManager.Application
Imports AdvancedOrderManager.Infrastructure
Imports Microsoft.Extensions.DependencyInjection
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass>
<TestCategory("Integration")>
Public Class EfOrderDataRepositoryTests

    Private Const TestConnectionString As String =
        "Server=(localdb)\MSSQLLocalDB;" &
        "Database=AdvancedOrderManagerDb;" &
        "Trusted_Connection=True;" &
        "TrustServerCertificate=True"

    <TestMethod>
    Public Async Function AddAndFindAsync_ValidRecord_RoundTrips() _
        As Task

        Dim services As New ServiceCollection()

        services.AddLogging()

        services.AddOrderEntityFramework(
            TestConnectionString)

        Using provider =
            services.BuildServiceProvider()

            Dim repository =
                provider.GetRequiredService(
                    Of IOrderDataRepository)()

            Dim orderId As String =
                $"EF-TEST-{Guid.NewGuid():N}"

            Dim record As New StoredOrderRecord(
                orderId,
                "EF Core Integration Customer",
                4,
                20D,
                True,
                80D,
                "Processed",
                DateTimeOffset.UtcNow)

            Dim caughtEx As Exception = Nothing

            Try
                Await repository.AddAsync(
                    record,
                    CancellationToken.None)

                Dim loadedRecord =
                    Await repository.FindByOrderIdAsync(
                        orderId,
                        CancellationToken.None)

                Assert.IsNotNull(
                    loadedRecord)

                Assert.AreEqual(
                    record.OrderId,
                    loadedRecord.OrderId)

                Assert.AreEqual(
                    record.CustomerName,
                    loadedRecord.CustomerName)

                Assert.AreEqual(
                    record.Quantity,
                    loadedRecord.Quantity)

                Assert.AreEqual(
                    record.UnitPrice,
                    loadedRecord.UnitPrice)

                Assert.AreEqual(
                    record.IsPriority,
                    loadedRecord.IsPriority)

                Assert.AreEqual(
                    record.TotalAmount,
                    loadedRecord.TotalAmount)

            Catch ex As Exception
                caughtEx = ex
            End Try

            ' Always attempt async cleanup (await allowed here)
            Try
                Await repository.DeleteByOrderIdAsync(
                    orderId,
                    CancellationToken.None)
            Catch
                ' Optional: ignore/delete-failed exceptions or log them
            End Try

            If caughtEx IsNot Nothing Then
                Throw caughtEx
            End If
        End Using
    End Function

End Class

