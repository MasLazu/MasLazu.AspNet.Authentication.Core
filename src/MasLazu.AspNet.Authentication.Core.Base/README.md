# MasLazu.AspNet.Authentication.Core.Base

This project contains the base implementations for the MasLazu ASP.NET Authentication Core library.

## Overview

The `MasLazu.AspNet.Authentication.Core.Base` project provides:

- **Service Implementations**: Concrete implementations of authentication and user management interfaces
- **JWT Utilities**: Token generation, validation, and management utilities
- **Validation**: FluentValidation validators for request models
- **Extensions**: Dependency injection and application setup extensions
- **Configuration**: JWT and authentication configuration classes
- **Background Services**: Database seeding and maintenance services

## Key Services

- `AuthService`: Implements `IAuthService` for user authentication and JWT token management
- `UserService`: Implements `IUserService` for user CRUD operations and email verification
- `GenderService`, `LanguageService`, `TimezoneService`: CRUD services for reference data
- `LoginMethodService`: Implements `ILoginMethodService` for login method management
- `UserLoginMethodService`: Implements `IUserLoginMethodService` for user-login method associations

## Key Components

- `JwtUtil`: Utility class for JWT access and refresh token generation and validation
- `JwtConfiguration`: Configuration class for JWT settings
- `AuthenticationCoreApplicationServiceExtension`: Extension for registering services in DI
- `AuthenticationCoreDatabaseSeedBackgroundService`: Background service for database seeding

## Project Configuration

- **Target Framework**: .NET 9.0
- **Implicit Usings**: Enabled
- **Nullable Reference Types**: Enabled

## Dependencies

- **Package References**:

  - `MasLazu.AspNet.Framework.Application` - Base application framework
  - `MasLazu.AspNet.Framework.Domain` - Domain framework
  - `MasLazu.AspNet.Verification.Abstraction` - Verification abstractions
  - `Microsoft.Extensions.DependencyInjection` - DI container
  - `Microsoft.Extensions.Hosting` - Hosting abstractions
  - `System.IdentityModel.Tokens.Jwt` - JWT token handling
  - `Microsoft.IdentityModel.Tokens` - Token validation

- **Project References**:
  - `MasLazu.AspNet.Authentication.Core.Domain` - Domain entities
  - `MasLazu.AspNet.Authentication.Core.Abstraction` - Service interfaces

## Usage

Register the services in your application's DI container:

```csharp
services.AddAuthenticationCoreApplicationServices();
```

Configure JWT settings in `appsettings.json`:

```json
{
  "Jwt": {
    "Key": "your-secret-key",
    "Issuer": "your-issuer",
    "Audience": "your-audience",
    "AccessTokenExpirationMinutes": 120,
    "RefreshTokenExpirationDays": 30
  }
}
```

🛠️ Development Guidelines

### Naming Conventions

- **Services**: Suffix with Service (e.g., AuthService)
- **Extensions**: Suffix with Extension (e.g., AuthenticationCoreApplicationServiceExtension)
- **Validators**: Suffix with Validator (e.g., CreateUserRequestValidator)
- **Utils**: Suffix with Util (e.g., JwtUtil)
- **Configuration**: Suffix with Configuration (e.g., JwtConfiguration)

### Code Structure

```
src/MasLazu.AspNet.Authentication.Core.Base/
├── BackgroundServices/    # Background services for maintenance
├── Configuration/         # Configuration classes
├── Extensions/            # DI and application extensions
├── Services/              # Service implementations
├── Utils/                 # Utility classes and property maps
├── Validators/            # Request validators
├── MasLazu.AspNet.Authentication.Core.Base.csproj
└── README.md
```

### Best Practices

- Implement interfaces from the Abstraction layer
- Use dependency injection for service dependencies
- Include proper error handling and validation
- Follow async/await patterns consistently
- Use meaningful variable names and XML documentation

🤝 Contributing

- **New Services**: Implement corresponding interfaces from Abstraction
- **Configuration**: Add new settings to configuration classes
- **Validation**: Create validators for new request models
- **Extensions**: Update DI registration for new services
- **Testing**: Add unit and integration tests for implementations

📄 License

Part of the MasLazu ASP.NET framework ecosystem.
