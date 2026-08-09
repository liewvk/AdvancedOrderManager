Option Explicit On
Option Strict On
Option Infer On

Imports Microsoft.EntityFrameworkCore

Public NotInheritable Class OrderDbContext
    Inherits DbContext

    Public Sub New(
        options As DbContextOptions(Of OrderDbContext))

        MyBase.New(options)
    End Sub

    Public Property ProcessedOrders As DbSet(
        Of ProcessedOrderEntity)

    Protected Overrides Sub OnModelCreating(
        modelBuilder As ModelBuilder)

        MyBase.OnModelCreating(modelBuilder)

        ConfigureProcessedOrder(
            modelBuilder)
    End Sub

    Private Shared Sub ConfigureProcessedOrder(
        modelBuilder As ModelBuilder)

        Dim entity =
            modelBuilder.Entity(
                Of ProcessedOrderEntity)()

        entity.ToTable(
            "ProcessedOrders",
            "dbo")

        entity.HasKey(
            Function(order) order.Id)

        entity.Property(
            Function(order) order.Id) _
            .ValueGeneratedOnAdd()

        entity.Property(
            Function(order) order.OrderId) _
            .HasMaxLength(50) _
            .IsRequired()

        entity.HasIndex(
            Function(order) order.OrderId) _
            .IsUnique()

        entity.Property(
            Function(order) order.CustomerName) _
            .HasMaxLength(120) _
            .IsRequired()

        entity.Property(
            Function(order) order.Quantity) _
            .IsRequired()

        entity.Property(
            Function(order) order.UnitPrice) _
            .HasPrecision(18, 2) _
            .IsRequired()

        entity.Property(
            Function(order) order.IsPriority) _
            .IsRequired()

        entity.Property(
            Function(order) order.TotalAmount) _
            .HasPrecision(18, 2) _
            .IsRequired()

        entity.Property(
            Function(order) order.Status) _
            .HasMaxLength(30) _
            .IsRequired()

        entity.Property(
            Function(order) order.ProcessedAt) _
            .IsRequired()
    End Sub

End Class

