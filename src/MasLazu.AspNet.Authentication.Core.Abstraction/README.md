# MasLazu.AspNet.Authentication.Core.Abstraction

This project contains the abstractions and interfaces for the MasLazu ASP.NET Authentication Core library.

## Overview

The `MasLazu.AspNet.Authentication.Core.Abstraction` project provides:

- **Service Interfaces**: Contracts for authentication, user management, and related services
- **Data Transfer Objects (DTOs)**: Models for users, login methods, genders, languages, timezones, etc.
- **Request/Response Models**: Structures for creating, updating, and retrieving data
- **Authentication Models**: Login responses with access and refresh tokens

## Key Interfaces

- `IAuthService`: Handles user authentication and login methods
- `IUserService`: Manages user CRUD operations and email verification
- `IGenderService`: CRUD operations for gender data
- `ILanguageService`: CRUD operations for language data
- `ILoginMethodService`: CRUD operations for login methods with additional functionality
- `ITimezoneService`: CRUD operations for timezone data
- `IUserLoginMethodService`: CRUD operations for user login method associations

## Key Models

- `UserDto`: Complete user information including profile details
- `LoginResponse`: Authentication tokens and expiration times
- `GenderDto`, `LanguageDto`, `TimezoneDto`: Reference data DTOs
- `LoginMethodDto`: Login method information
- `UserLoginMethodDto`: Association between users and login methods
- Create/Update request models for all entities

## Project Configuration

- **Target Framework**: .NET 9.0
- **Implicit Usings**: Enabled
- **Nullable Reference Types**: Enabled

## Dependencies

- **Package References**:
  - `MasLazu.AspNet.Framework.Application` - Provides base DTOs and CRUD service interfaces

This project serves as the foundation for implementing authentication-related functionality across the solution.

## Usage

Implement the provided interfaces in your concrete classes to integrate with the authentication system. Use the DTOs for data transfer between layers.

🛠️ Development Guidelines

### Naming Conventions

- **Interfaces**: Prefix with I (e.g., IAuthService)
- **DTOs**: Suffix with Dto (e.g., UserDto)
- **Requests**: Suffix with Request (e.g., CreateUserRequest)
- **Responses**: Suffix with Response (e.g., LoginResponse)

### Code Structure

```
src/MasLazu.AspNet.Authentication.Core.Abstraction/
├── Interfaces/         # Service contracts
├── Models/             # DTOs and request/response models
├── MasLazu.AspNet.Authentication.Core.Abstraction.csproj
└── README.md
```

### Best Practices

- Keep interfaces focused and minimal
- Use records for immutable DTOs
- Include XML documentation for public APIs
- Follow async/await patterns consistently
- Use meaningful property names and types

🤝 Contributing

- **Interface Changes**: Consider backward compatibility
- **New Models**: Follow existing naming and structure patterns
- **Documentation**: Update XML comments for new members
- **Testing**: Add tests for new abstractions
- **Versioning**: Consider semantic versioning for breaking changes

📄 License

Part of the MasLazu ASP.NET framework ecosystem.
