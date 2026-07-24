Option Explicit On
Option Strict On
Option Infer On

Imports AdvancedOrderManager.Domain
Imports AdvancedOrderManager.Infrastructure
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass>
Public Class GenericRepositoryTests

    Private NotInheritable Class TestEntity
        Implements IEntity(Of Integer)

        Public Sub New(
            id As Integer,
            name As String)

            Me.Id = id
            Me.Name = name
        End Sub

        Public ReadOnly Property Id As Integer _
            Implements IEntity(Of Integer).Id

        Public ReadOnly Property Name As String

    End Class

    Private NotInheritable Class TestRepository

        Inherits InMemoryRepository(
            Of Integer, TestEntity)

    End Class

    <TestMethod>
    Public Sub Add_StoresEntity()

        Dim repository As New TestRepository()

        repository.Add(
            New TestEntity(
                1,
                "First"))

        Assert.AreEqual(1, repository.Count)
        Assert.IsNotNull(repository.GetById(1))
    End Sub

    <TestMethod>
    Public Sub Add_DuplicateIdentifier_ThrowsException()

        Dim repository As New TestRepository()

        repository.Add(
            New TestEntity(
                1,
                "First"))

        Assert.Throws(Of InvalidOperationException)(
            Sub()
                repository.Add(
                    New TestEntity(
                        1,
                        "Duplicate"))
            End Sub)
    End Sub

    <TestMethod>
    Public Sub Update_ReplacesEntity()

        Dim repository As New TestRepository()

        repository.Add(
            New TestEntity(
                1,
                "Original"))

        repository.Update(
            New TestEntity(
                1,
                "Updated"))

        Assert.AreEqual(
            "Updated",
            repository.GetById(1).Name)
    End Sub

    <TestMethod>
    Public Sub Remove_ExistingEntity_ReturnsTrue()

        Dim repository As New TestRepository()

        repository.Add(
            New TestEntity(
                1,
                "First"))

        Dim removed As Boolean =
            repository.Remove(1)

        Assert.IsTrue(removed)
        Assert.AreEqual(0, repository.Count)
    End Sub

    <TestMethod>
    Public Sub Find_ReturnsMatchingEntities()

        Dim repository As New TestRepository()

        repository.Add(
            New TestEntity(1, "Alice"))

        repository.Add(
            New TestEntity(2, "Ben"))

        repository.Add(
            New TestEntity(3, "Albert"))

        Dim results =
            repository.Find(
                Function(entity)
                    Return entity.Name.StartsWith(
                        "Al",
                        StringComparison.OrdinalIgnoreCase)
                End Function)

        Assert.HasCount(2, results)
    End Sub

End Class

