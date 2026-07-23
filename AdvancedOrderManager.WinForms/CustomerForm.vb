Option Explicit On
Option Strict On
Option Infer On

Imports System.ComponentModel
Imports System.Linq
Imports AdvancedOrderManager.Application
Imports AdvancedOrderManager.Domain
Imports AdvancedOrderManager.Domain.Entities

Public Class CustomerForm

    Private ReadOnly _registerCustomerService As RegisterCustomerService

    Private ReadOnly _repository As ICustomerRepository

    Private ReadOnly _rows As New BindingList(Of CustomerGridRow)()

    Private ReadOnly _source As New BindingSource()

    Public Sub New(
        registerCustomerService As RegisterCustomerService,
        repository As ICustomerRepository)

        InitializeComponent()

        If registerCustomerService Is Nothing Then
            Throw New ArgumentNullException(
                NameOf(registerCustomerService))
        End If

        If repository Is Nothing Then
            Throw New ArgumentNullException(
                NameOf(repository))
        End If

        _registerCustomerService =
            registerCustomerService

        _repository = repository
    End Sub

    Private Sub CustomerForm_Load(
        sender As Object,
        e As EventArgs) Handles MyBase.Load

        ConfigureCustomerGrid()

        _source.DataSource = _rows
        dgvCustomers.DataSource = _source

        RefreshCustomerGrid()

        txtFirstName.Focus()
    End Sub

    Private Sub ConfigureCustomerGrid()

        dgvCustomers.Columns.Clear()

        AddTextColumn(
            "CustomerIdColumn",
            "Customer ID",
            NameOf(CustomerGridRow.CustomerId),
            25)

        AddTextColumn(
            "FullNameColumn",
            "Customer Name",
            NameOf(CustomerGridRow.FullName),
            25)

        AddTextColumn(
            "EmailColumn",
            "Email",
            NameOf(CustomerGridRow.Email),
            30)

        AddTextColumn(
            "CityColumn",
            "City",
            NameOf(CustomerGridRow.City),
            15)

        AddTextColumn(
            "CountryColumn",
            "Country",
            NameOf(CustomerGridRow.Country),
            15)

        AddTextColumn(
            "StatusColumn",
            "Status",
            NameOf(CustomerGridRow.Status),
            10)
    End Sub

    Private Sub AddTextColumn(
        columnName As String,
        headerText As String,
        propertyName As String,
        fillWeight As Single)

        Dim column As New DataGridViewTextBoxColumn() With {
            .Name = columnName,
            .HeaderText = headerText,
            .DataPropertyName = propertyName,
            .FillWeight = fillWeight,
            .ReadOnly = True
        }

        dgvCustomers.Columns.Add(column)
    End Sub

    Private Sub btnRegister_Click(
        sender As Object,
        e As EventArgs) Handles btnRegister.Click

        Try
            Dim request As New RegisterCustomerRequest(
                txtFirstName.Text,
                txtLastName.Text,
                txtEmail.Text,
                txtAddressLine.Text,
                txtCity.Text,
                txtPostalCode.Text,
                txtCountry.Text)

            Dim result =
                _registerCustomerService.Execute(request)

            If Not result.IsSuccess Then

                MessageBox.Show(
                    result.ErrorMessage,
                    "Customer Cannot Be Registered",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning)

                lblStatus.Text =
                    "Customer registration failed."

                Return
            End If

            Dim customer As CustomerProfile =
                result.Value

            MessageBox.Show(
                $"Customer registered successfully." &
                Environment.NewLine &
                $"Customer ID: {customer.CustomerId}" &
                Environment.NewLine &
                $"Name: {customer.Name.FullName}",
                "Customer Registered",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information)

            ClearInputs()
            RefreshCustomerGrid()

            lblStatus.Text =
                $"Registered {customer.Name.FullName}."

        Catch ex As Exception

            lblStatus.Text =
                "An unexpected error occurred."

            MessageBox.Show(
                "The customer could not be registered." &
                Environment.NewLine &
                Environment.NewLine &
                ex.Message,
                "Application Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClear_Click(
        sender As Object,
        e As EventArgs) Handles btnClear.Click

        ClearInputs()
        lblStatus.Text = "Input cleared."
    End Sub

    Private Sub txtSearch_TextChanged(
        sender As Object,
        e As EventArgs) Handles txtSearch.TextChanged

        RefreshCustomerGrid()
    End Sub

    Private Sub RefreshCustomerGrid()

        Dim searchText As String =
            txtSearch.Text.Trim()

        Dim customers =
            _repository.GetAll().AsEnumerable()

        If searchText.Length > 0 Then

            customers =
                customers.Where(
                    Function(customer)

                        Return customer.Name.FullName _
                                   .Contains(
                                       searchText,
                                       StringComparison.OrdinalIgnoreCase) OrElse
                               customer.Email.Value _
                                   .Contains(
                                       searchText,
                                       StringComparison.OrdinalIgnoreCase) OrElse
                               customer.PostalAddress.City _
                                   .Contains(
                                       searchText,
                                       StringComparison.OrdinalIgnoreCase)
                    End Function)
        End If

        _rows.Clear()

        For Each customer In customers
            _rows.Add(
                New CustomerGridRow(customer))
        Next

        lblCustomerCount.Text =
            $"Customers: {_rows.Count}"
    End Sub

    Private Sub ClearInputs()

        txtFirstName.Clear()
        txtLastName.Clear()
        txtEmail.Clear()
        txtAddressLine.Clear()
        txtCity.Clear()
        txtPostalCode.Clear()
        txtCountry.Text = "Malaysia"

        txtFirstName.Focus()
    End Sub

    Private Sub btnCustomers_Click(sender As Object, e As EventArgs) Handles btnCustomers.Click
        Using customerForm As CustomerForm = Bootstrapper.CreateCustomerForm()

            customerForm.ShowDialog(Me)
        End Using

    End Sub
End Class
