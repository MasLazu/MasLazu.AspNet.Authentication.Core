# MasLazu.AspNet.Authentication.Core.Consumer

This project contains the event consumers for the MasLazu ASP.NET Authentication Core library.

## Overview

The `MasLazu.AspNet.Authentication.Core.Consumer` project provides:

- **Event Consumers**: MassTransit consumers for handling authentication-related events
- **Event-Driven Processing**: Asynchronous processing of verification and authentication events
- **Extensions**: Dependency injection and MassTransit registration extensions

## Key Consumers

- `VerificationCompletedConsumer`: Handles `VerificationCompletedEvent` to verify user emails upon successful verification

## Key Components

- `ServiceCollectionExtensions`: Extensions for registering consumers in DI and MassTransit

## Project Configuration

- **Target Framework**: .NET 9.0
- **Implicit Usings**: Enabled
- **Nullable Reference Types**: Enabled

## Dependencies

- **Package References**:

  - `MasLazu.AspNet.Verification.Abstraction` - Verification event models
  - `MassTransit` - Message bus for event handling
  - `Microsoft.Extensions.DependencyInjection` - DI container

- **Project References**:
  - `MasLazu.AspNet.Authentication.Core.Abstraction` - Service interfaces

## Usage

Register the consumers in your application's DI container and MassTransit configuration:

```csharp
// Register consumers in DI
services.AddAuthenticationCoreConsumers();

// Configure MassTransit
busConfigurator.AddAuthenticationCoreConsumers();
```

The consumers will automatically handle events published to the message bus.

## Event Flow

1. Verification service publishes `VerificationCompletedEvent`
2. `VerificationCompletedConsumer` receives the event
3. If verification is successful, updates user email verification status
4. User email is marked as verified in the database

🛠️ Development Guidelines

### Naming Conventions

- **Consumers**: Suffix with Consumer (e.g., VerificationCompletedConsumer)
- **Extensions**: Suffix with Extensions (e.g., ServiceCollectionExtensions)
- **Event Handlers**: Follow MassTransit IConsumer<TEvent> pattern

### Code Structure

```
src/MasLazu.AspNet.Authentication.Core.Consumer/
├── Consumers/             # MassTransit event consumers
├── Extensions/            # DI and MassTransit extensions
├── MasLazu.AspNet.Authentication.Core.Consumer.csproj
└── README.md
```

### Best Practices

- Implement `IConsumer<TEvent>` for event handling
- Use dependency injection for service dependencies
- Handle exceptions appropriately in consumer logic
- Follow async/await patterns for database operations
- Include proper logging for event processing

🤝 Contributing

- **New Consumers**: Implement IConsumer<TEvent> for new event types
- **Event Handling**: Add business logic for processing events
- **Registration**: Update extensions to register new consumers
- **Testing**: Add integration tests for consumer behavior
- **Error Handling**: Implement retry and dead letter queue patterns

📄 License

Part of the MasLazu ASP.NET framework ecosystem.
