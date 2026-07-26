# AdvancedOrderManager

AdvancedOrderManager is a Visual Basic .NET application for managing orders, inventory and related workflows. This repository contains the source code, configuration, and supporting files for building and running the project.

> Note: This README provides general guidance for building and running the project. Implementation details (database schema, exact configuration keys, and environment-specific setup) are in the repository structure and project files.

## Key Features

- Order entry and tracking
- Inventory management
- Basic reporting and export capabilities
- Configurable data storage (database connection strings in configuration)
- Designed for extensibility and maintainability using Visual Basic .NET and .NET tooling

## Language and Platform

- Language: Visual Basic .NET
- Target framework: .NET (see project files for exact target framework/version)
- Recommended IDE: Visual Studio (Windows) or other editors that support VB.NET and MSBuild

## Prerequisites

- .NET SDK matching the project's target framework (check project files)
- Visual Studio or another IDE with VB.NET support
- Optional: SQL Server, SQLite, or other supported database depending on configuration

## Quick Start (Development)

1. Clone the repository:

   ```
   git clone https://github.com/liewvk/AdvancedOrderManager.git
   cd AdvancedOrderManager
   ```

2. Open the solution or project in Visual Studio or your preferred editor.

3. Restore NuGet packages (Visual Studio will usually restore automatically). From the command line:

   ```
   dotnet restore
   ```

4. Build the project:

   ```
   dotnet build
   ```

5. Configure the application settings (connection strings, logging, etc.). Look for configuration files such as `app.config`, `web.config`, or `appsettings.json` in the project folders and update them accordingly.

6. Run the application from the IDE or via command line:

   ```
   dotnet run --project <path-to-project-file>
   ```

   Replace `<path-to-project-file>` with the path to the VB.NET project file if running from the command line.

## Database and Migrations

This repository may use a database for persisting orders and inventory. Check the solution for any scripts, EF migrations, or SQL files. Typical steps:

- Create the database specified by the connection string in configuration.
- Run migration or schema scripts (if present). If Entity Framework is used, use the EF tools to apply migrations.

## Tests

If the repository includes tests, run them using your test runner (Visual Studio Test Explorer or `dotnet test`).

## Configuration

- Search the repository for files named `app.config`, `web.config`, `appsettings.json`, or `*.config` for environment-specific settings.
- Store sensitive values (passwords, keys) securely — do not commit secrets to the repository.

## Chapter 04: New Classes and Event-Driven Architecture

Chapter 04 introduces new domain classes, event-driven patterns, and enhanced inventory management capabilities:

### New Domain Classes

- **`StockMovement`** — Represents a change in product inventory with validation for quantity changes and reasons. Tracks when movements occur.
- **`Product`** — Enhanced entity class implementing `IEntity(Of ProductId)` with support for stock movement tracking and inventory management.
- **`Order` & `OrderLine`** — Domain models for managing customer orders with line items and validation.
- **`PersonName`** — Value object for managing person names with validation (minimum 2 characters per part).
- **`CustomerId`** — Value object (struct) implementing `IEquatable(Of CustomerId)` for strongly-typed customer identification.
- **`PostalAddress`** — Value object for managing addresses with support for address composition and equality comparison.

### Application Layer Classes

- **`OrderSubmission`** — Data transfer object (DTO) for submitting order information with automatic subtotal calculation.
- **`OrderLineRequest`** — Request object for individual order line entries.
- **`RegisterProductRequest`** — Request object for product registration with inventory details.
- **`OrderProcessor`** — Event-driven processor that validates and processes orders using custom validator and calculator functions.
- **`OrderAuditSubscriber`** — Subscriber that logs order events (processed/rejected) to an in-memory audit trail.
- **`OrderProcessingStatistics`** — Tracks order processing metrics and event handling.

### Architecture Highlights

- **Event-driven pattern** — `OrderProcessor` raises `OrderProcessed` and `OrderRejected` events for loose coupling.
- **Value Objects** — Strong typing for domain concepts (PersonName, CustomerId, PostalAddress).
- **Validation** — Constructor-level validation across domain and application layers.
- **Services** — `CreateOrderService`, `RegisterCustomerService`, `RegisterProductService`, and other specialized services manage business logic.
- **Form Integration** — `OrderProcessingEventForm`, `CustomerForm`, `ProductForm` provide UI bindings for event subscribers.

### Project Structure

- **`AdvancedOrderManager.Domain`** — Domain entities, value objects, and business rules.
- **`AdvancedOrderManager.Application`** — Application services, requests, and event handling.
- **`AdvancedOrderManager.WinForms`** — WinForms UI with form classes and bootstrapper for dependency injection.
- **`AdvancedOrderManager.Infrastructure`** — Repositories and data access implementations (in-memory and database).
- **`AdvancedOrderManager.Tests`** — Unit tests for value objects and domain logic.

## Contributing

Contributions are welcome. Please follow these guidelines:

1. Open an issue to propose significant changes or discuss bugs/feature requests.
2. Create a topic branch for your work.
3. Make small, focused commits with clear messages.
4. Open a pull request with a description of what you changed and why.

If there are existing contribution guidelines or a code of conduct in the repository, follow those.

## Troubleshooting

- If builds fail, check the target .NET SDK version and installed workloads in Visual Studio.
- For missing dependencies, run `dotnet restore` or use the NuGet package manager in Visual Studio.
- Check configuration files for correct connection strings and paths.

## License

If the repository includes a LICENSE file, that governs usage. If not, add a license or contact the repository owner to clarify terms.

## Contact

For questions about this repository, open an issue on GitHub. If you are the repository owner, update this README with project-specific details (database schema, exact prerequisites, environment variables, etc.).
