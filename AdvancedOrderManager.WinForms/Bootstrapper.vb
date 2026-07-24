Option Explicit On
Option Strict On
Option Infer On

Imports AdvancedOrderManager.Application
Imports AdvancedOrderManager.Application.Contracts
Imports AdvancedOrderManager.Infrastructure

Friend NotInheritable Class Bootstrapper

    Private Shared ReadOnly _orderRepository As IOrderRepository =
        New InMemoryOrderRepository()

    Private Shared ReadOnly _customerRepository As ICustomerRepository =
        New InMemoryCustomerRepository()
    Private Shared ReadOnly _productRepository As IProductRepository =
        New InMemoryProductRepository()


    Private Sub New()
    End Sub

    Public Shared Function CreateMainForm() As MainForm

        Dim createOrderService As New CreateOrderService(
            _orderRepository)

        Return New MainForm(
            createOrderService,
            _orderRepository)

    End Function

    Public Shared Function CreateCustomerForm() As CustomerForm

        Dim registerCustomerService As New RegisterCustomerService(
            _customerRepository)

        Return New CustomerForm(
            registerCustomerService,
            _customerRepository)

    End Function
    Public Shared Function CreateProductForm() _
    As ProductForm

        Dim registerProductService As New RegisterProductService(_productRepository)

        Dim searchProductsService As New SearchProductsService(_productRepository)

        Dim adjustmentService As New InventoryAdjustmentService(_productRepository)

        Dim restockQueueService As New RestockQueueService(_productRepository)

        Dim statisticsService As New InventoryStatisticsService(_productRepository)

        Return New ProductForm(
        _productRepository,
        registerProductService,
        searchProductsService,
        adjustmentService,
        restockQueueService,
        statisticsService)
    End Function

End Class