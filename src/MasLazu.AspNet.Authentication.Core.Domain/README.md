# MasLazu.AspNet.Authentication.Core.Domain

This project contains the domain entities for the MasLazu ASP.NET Authentication Core library.

## Overview

The `MasLazu.AspNet.Authentication.Core.Domain` project provides:

- **Domain Entities**: Core business objects representing users, authentication, and reference data
- **Entity Relationships**: Navigation properties defining relationships between entities
- **Base Entity Inheritance**: Common properties through BaseEntity

## Key Entities

### Core Authentication Entities

- `User`: Represents a user account with profile information
- `LoginMethod`: Defines available authentication methods (e.g., email, phone)
- `UserLoginMethod`: Junction entity linking users to their login methods
- `RefreshToken`: Stores refresh token data for JWT authentication
- `UserRefreshToken`: Junction entity linking users to their refresh tokens

### Reference Data Entities

- `Gender`: Gender reference data
- `Language`: Language reference data
- `Timezone`: Timezone reference data with offset information

## Entity Relationships

```
User
├── 1:1 → Gender
├── 1:1 → Language
├── 1:1 → Timezone
└── 1:N → UserLoginMethod
    └── 1:1 → LoginMethod

User
└── 1:N → UserRefreshToken
    └── 1:1 → RefreshToken
```

## Project Configuration

- **Target Framework**: .NET 9.0
- **Implicit Usings**: Enabled
- **Nullable Reference Types**: Enabled

## Dependencies

- **Package References**:
  - `MasLazu.AspNet.Framework.Domain` - Base domain framework with BaseEntity

## Entity Details

### User Entity

- **Properties**: Name, Email, PhoneNumber, Username, LanguageCode, TimezoneId, ProfilePicture, GenderCode
- **Verification Flags**: IsEmailVerified, IsPhoneNumberVerified
- **Relationships**: Timezone, Gender, Language, UserLoginMethods

### LoginMethod Entity

- **Properties**: Code, Name, Description, IsEnabled
- **Purpose**: Defines available authentication methods

### RefreshToken Entity

- **Properties**: Token, ExpiresDate, RevokedDate
- **Purpose**: JWT refresh token storage

### Reference Entities

- **Gender**: Code, Name
- **Language**: Code, Name
- **Timezone**: Identifier, Name, OffsetMinutes

## Usage

These entities are used throughout the application for:

- User management and authentication
- JWT token handling
- Reference data storage
- Entity Framework Core mappings

## Domain Model Principles

- **Single Responsibility**: Each entity has a clear, focused purpose
- **Navigation Properties**: Define relationships between entities
- **BaseEntity Inheritance**: Common auditing properties (Id, CreatedAt, UpdatedAt)
- **Nullable References**: Proper nullability for optional relationships

🛠️ Development Guidelines

### Naming Conventions

- **Entities**: PascalCase class names (e.g., User, LoginMethod)
- **Properties**: PascalCase property names
- **Navigation Properties**: Plural for collections (e.g., UserLoginMethods)
- **Foreign Keys**: EntityName + Id pattern (e.g., UserId, TimezoneId)

### Code Structure

```
src/MasLazu.AspNet.Authentication.Core.Domain/
├── Entities/              # Domain entity classes
├── MasLazu.AspNet.Authentication.Core.Domain.csproj
└── README.md
```

### Best Practices

- Inherit from BaseEntity for common properties
- Use navigation properties for entity relationships
- Include XML documentation for public properties
- Use appropriate data types and constraints
- Follow DDD principles for entity design

🤝 Contributing

- **New Entities**: Follow existing patterns and naming conventions
- **Relationships**: Define navigation properties appropriately
- **Properties**: Use meaningful names and correct data types
- **Documentation**: Add XML comments for new entities and properties
- **Validation**: Consider domain validation rules

📄 License

Part of the MasLazu ASP.NET framework ecosystem.
