# Ticket System API Architecture

This project is structured following the Clean Architecture pattern, ensuring separation of concerns and scalability. Here is an overview of each layer and its responsibilities:

## Layers

### 1. **TicketSystem.Api**
- **Purpose**: Acts as the entry point for the application, handling HTTP requests and responses.
- **Responsibilities**:
  - Define API endpoints using controllers.
  - Handle user input and pass it to the application layer.
  - Return appropriate responses to the client.

### 2. **TicketSystem.Application**
- **Purpose**: Contains the core business logic of the application.
- **Responsibilities**:
  - Implement commands and queries using MediatR for CQRS pattern.
  - Define business rules and validation logic.
  - Handle use cases for creating and handling tickets.

### 3. **TicketSystem.Domain**
- **Purpose**: Represents the core of the application with business entities and enums.
- **Responsibilities**:
  - Define domain models (e.g., `Ticket`).
  - Enumerate domain-specific concepts (e.g., `TicketStatus`).
  - Maintain business rules and invariants within domain entities.

### 4. **TicketSystem.Infrastructure**
- **Purpose**: Handles data access and external service integration.
- **Responsibilities**:
  - Implement repository interfaces for data persistence.
  - Configure Entity Framework Core for database operations.
  - Manage database migrations and context setup.

## Dependency Flow

The dependency flow follows the direction of outer layers depending on inner layers, ensuring that:
- The **Api** layer depends on the **Application** layer.
- The **Application** layer depends on the **Domain** layer.
- The **Infrastructure** layer depends on the **Domain** layer but not vice versa.

This structure ensures that the core business logic and domain models remain independent of external frameworks and technologies.

## Example Flow

1. **Client Request**: A client sends a request to the API to create a new ticket.
2. **Api Layer**: The `TicketsCommandController` receives the request and maps it to a `CreateTicketCommand`.
3. **Application Layer**: The `CreateTicketCommandHandler` processes the command, applying business logic to create a new ticket.
4. **Domain Layer**: The `Ticket` entity is instantiated with the provided details.
5. **Infrastructure Layer**: The `TicketRepository` saves the new ticket to the database using Entity Framework Core.
6. **Api Layer**: The controller returns the created ticket as a response to the client.

## Advantages

- **Separation of Concerns**: Each layer has a distinct responsibility, making the codebase more maintainable and testable.
- **Scalability**: The architecture supports scaling each part of the application independently.
- **Testability**: Business logic is decoupled from the framework and can be tested in isolation.

## Conclusion

This clean architecture ensures that the Ticket System API is robust, scalable, and maintainable. It adheres to best practices, making it easier to manage and extend in the future.
