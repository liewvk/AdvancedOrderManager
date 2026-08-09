Option Explicit On
Option Strict On
Option Infer On

Imports System.Runtime.CompilerServices
Imports AdvancedOrderManager.Application
Imports Microsoft.EntityFrameworkCore
Imports Microsoft.Extensions.DependencyInjection

Public Module EntityFrameworkServiceCollectionExtensions

    <Extension>
    Public Function AddOrderEntityFramework(
        services As IServiceCollection,
        connectionString As String) As IServiceCollection

        If services Is Nothing Then
            Throw New ArgumentNullException(NameOf(services))
        End If

        If String.IsNullOrWhiteSpace(connectionString) Then
            Throw New ArgumentException(
                "A database connection string is required.",
                NameOf(connectionString))
        End If

        services.AddDbContextFactory(Of OrderDbContext)(
            Sub(options)
                options.UseSqlServer(connectionString)
            End Sub)

        services.AddSingleton(
            Of IOrderDataRepository,
               EfOrderDataRepository)()

        services.AddSingleton(
            Of IOrderHistoryQueryService,
               EfOrderHistoryQueryService)()

        Return services
    End Function

End Module