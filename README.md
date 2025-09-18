# MasLazu.AspNet.Authentication.Core

A comprehensive authentication and user management system built with ASP.NET Core, following Clean Architecture and Domain-Driven Design principles.

## 🏗️ Architecture Overview

This solution implements a layered architecture with clear separation of concerns:

```
┌─────────────────────────────────────┐
│         Presentation Layer          │
│   MasLazu.AspNet.Authentication.    │
│           Core.Endpoint             │
└─────────────────────────────────────┘
                    │
┌─────────────────────────────────────┐
│       Application Layer             │
│   MasLazu.AspNet.Authentication.    │
│           Core.Base                 │
└─────────────────────────────────────┘
                    │
┌─────────────────────────────────────┐
│       Domain Layer                  │
│   MasLazu.AspNet.Authentication.    │
│           Core.Domain               │
│   MasLazu.AspNet.Authentication.    │
│           Core.Abstraction          │
└─────────────────────────────────────┘
                    │
┌─────────────────────────────────────┐
│       Infrastructure Layer          │
│   MasLazu.AspNet.Authentication.    │
│           Core.EfCore               │
│   MasLazu.AspNet.Authentication.    │
│           Core.Consumer             │
└─────────────────────────────────────┘
```

## 📦 Projects

### Core Projects

#### 🔷 MasLazu.AspNet.Authentication.Core.Abstraction

**Purpose**: Defines contracts and interfaces for the authentication system

- Service interfaces (IUserService, IAuthService, etc.)
- Data transfer objects (DTOs) and request/response models
- Base abstractions for consistent implementation

#### 🔷 MasLazu.AspNet.Authentication.Core.Domain

**Purpose**: Contains domain entities and business rules

- User, LoginMethod, RefreshToken entities
- Reference data entities (Gender, Language, Timezone)
- Domain relationships and constraints

#### 🔷 MasLazu.AspNet.Authentication.Core.Base

**Purpose**: Provides concrete implementations of abstractions

- Service implementations using dependency injection
- JWT token utilities and configuration
- Validation rules and business logic
- Background services for maintenance tasks

### Infrastructure Projects

#### 🔷 MasLazu.AspNet.Authentication.Core.EfCore

**Purpose**: Entity Framework Core data access layer

- DbContext configurations for write and read operations
- Entity type configurations and mappings
- Database schema definitions and relationships
- CQRS support with separate read/write contexts

#### 🔷 MasLazu.AspNet.Authentication.Core.Endpoint

**Purpose**: REST API endpoints using FastEndpoints

- CRUD operations for all entities
- Authentication endpoints (login, verification)
- API versioning and Swagger documentation
- Request validation and response formatting

#### 🔷 MasLazu.AspNet.Authentication.Core.Consumer

**Purpose**: Event-driven processing with MassTransit

- Message consumers for verification events
- Asynchronous email verification handling
- Event-driven architecture integration

## 🚀 Key Features

### Authentication & Authorization

- JWT-based authentication with access and refresh tokens
- Multiple login methods support
- Email and phone number verification
- Role-based access control ready

### User Management

- Complete user profile management
- Multi-language and timezone support
- Gender and other reference data
- Email verification workflows

### API Design

- RESTful API endpoints
- FastEndpoints for high performance
- Comprehensive Swagger documentation
- Standardized error handling

### Data Access

- Entity Framework Core with Code-First approach
- CQRS pattern support
- Optimized queries and indexing
- Migration support

### Event Processing

- MassTransit for message handling
- Asynchronous event processing
- Verification completion handling

## 🛠️ Technology Stack

- **Framework**: .NET 9.0
- **Web Framework**: ASP.NET Core
- **ORM**: Entity Framework Core
- **API Framework**: FastEndpoints
- **Message Bus**: MassTransit
- **Validation**: FluentValidation
- **Authentication**: JWT (JSON Web Tokens)
- **Database**: SQL Server (configurable)

## 📋 Prerequisites

- .NET 9.0 SDK
- SQL Server or compatible database
- Message broker (RabbitMQ, Azure Service Bus, etc.) for MassTransit

## ⚡ Quick Start

1. **Clone and Restore**

   ```bash
   git clone <repository-url>
   cd MasLazu.AspNet.Authentication.Core
   dotnet restore
   ```

2. **Database Setup**

   ```bash
   # Update connection string in appsettings.json
   dotnet ef database update --project src/MasLazu.AspNet.Authentication.Core.EfCore
   ```

3. **Configure Services**

   ```csharp
   // In Program.cs
   builder.Services.AddAuthenticationCoreApplicationServices();
   builder.Services.AddAuthenticationCoreEntityFrameworkCore();
   builder.Services.AddAuthenticationCoreEndpoints();
   builder.Services.AddAuthenticationCoreConsumers();
   ```

4. **Run the Application**
   ```bash
   dotnet run --project YourStartupProject
   ```

## 🔧 Configuration

### JWT Configuration

```json
{
  "Jwt": {
    "Key": "your-256-bit-secret-key",
    "Issuer": "your-issuer",
    "Audience": "your-audience",
    "AccessTokenExpirationMinutes": 120,
    "RefreshTokenExpirationDays": 30
  }
}
```

### Database Configuration

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=AuthCore;Trusted_Connection=True;"
  }
}
```

## 📚 API Documentation

Once running, access Swagger documentation at:

```
https://localhost:5001/swagger
```

## 🧪 Testing

Run tests for all projects:

```bash
dotnet test
```

## 🤝 Contributing

1. Follow the established architecture patterns
2. Add tests for new functionality
3. Update documentation
4. Follow naming conventions
5. Use dependency injection

## 📄 License

Part of the MasLazu ASP.NET framework ecosystem.

## 📞 Support

For questions or issues, please refer to the individual project READMEs for detailed documentation.
