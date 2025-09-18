# MasLazu.AspNet.Authentication.Core.EfCore

This project contains the Entity Framework Core configuration for the MasLazu ASP.NET Authentication Core library.

## Overview

The `MasLazu.AspNet.Authentication.Core.EfCore` project provides:

- **Entity Configurations**: EF Core mappings for all domain entities
- **DbContext Classes**: Database contexts for write and read operations
- **Database Mappings**: Table structures, relationships, and constraints
- **CQRS Support**: Separate read and write contexts

## DbContext Classes

### AuthenticationCoreDbContext (Write Context)

- Extends `BaseDbContext` for write operations
- Includes all entities: Users, RefreshTokens, LoginMethods, etc.
- Applies entity configurations from the assembly
- Used for create, update, delete operations

### AuthenticationCoreReadDbContext (Read Context)

- Extends `BaseReadDbContext` for read operations
- Includes read-optimized entity sets
- Supports CQRS pattern for query separation
- Used for read-only operations

## Entity Configurations

### Core Entity Configurations

- **UserConfiguration**: User entity mapping with relationships and indexes
- **LoginMethodConfiguration**: Login method with unique code constraint
- **UserLoginMethodConfiguration**: Junction table for user-login method relationships
- **RefreshTokenConfiguration**: Refresh token storage configuration
- **UserRefreshTokenConfiguration**: User-refresh token relationships

### Reference Data Configurations

- **GenderConfiguration**: Gender reference data
- **LanguageConfiguration**: Language reference data
- **TimezoneConfiguration**: Timezone with offset information

## Database Schema

### Key Tables

- **Users**: User accounts with profile information
- **LoginMethods**: Available authentication methods
- **UserLoginMethods**: User-authentication method associations
- **RefreshTokens**: JWT refresh token storage
- **UserRefreshTokens**: User-refresh token relationships
- **Genders**: Gender reference data
- **Languages**: Language reference data
- **Timezones**: Timezone reference data

### Relationships

```
Users
├── FK → Genders (GenderCode)
├── FK → Languages (LanguageCode)
├── FK → Timezones (TimezoneId)
└── 1:N → UserLoginMethods
    └── FK → LoginMethods (LoginMethodCode)

Users
└── 1:N → UserRefreshTokens
    └── FK → RefreshTokens (RefreshTokenId)
```

## Project Configuration

- **Target Framework**: .NET 9.0
- **Implicit Usings**: Enabled
- **Nullable Reference Types**: Enabled

## Dependencies

- **Package References**:

  - `MasLazu.AspNet.Framework.EfCore` - Base EF Core framework

- **Project References**:
  - `MasLazu.AspNet.Authentication.Core.Domain` - Domain entities

## Usage

Register the DbContexts in your application's DI container:

```csharp
services.AddDbContext<AuthenticationCoreDbContext>(options =>
    options.UseSqlServer(connectionString));

services.AddDbContext<AuthenticationCoreReadDbContext>(options =>
    options.UseSqlServer(connectionString));
```

For CQRS setup:

```csharp
// Write context for commands
services.AddScoped<AuthenticationCoreDbContext>();

// Read context for queries
services.AddScoped<AuthenticationCoreReadDbContext>();
```

## Migration Strategy

Create and apply migrations:

```bash
dotnet ef migrations add InitialCreate --project src/MasLazu.AspNet.Authentication.Core.EfCore --startup-project YourStartupProject
dotnet ef database update --project src/MasLazu.AspNet.Authentication.Core.EfCore --startup-project YourStartupProject
```

## Performance Considerations

- **Indexes**: Strategic indexes on frequently queried columns (Email, Username, PhoneNumber)
- **Relationships**: Proper foreign key constraints and cascade behaviors
- **Read Optimization**: Separate read context for query performance
- **Connection Management**: Proper connection pooling and lifetime management

🛠️ Development Guidelines

### Naming Conventions

- **Configurations**: Suffix with Configuration (e.g., UserConfiguration)
- **DbContexts**: Suffix with DbContext (e.g., AuthenticationCoreDbContext)
- **Extensions**: Suffix with Extensions (e.g., ServiceCollectionExtensions)
- **Table Names**: Plural form of entity names

### Code Structure

```
src/MasLazu.AspNet.Authentication.Core.EfCore/
├── Configurations/       # Entity type configurations
├── Data/                 # DbContext classes
├── Extensions/           # DI registration extensions
├── MasLazu.AspNet.Authentication.Core.EfCore.csproj
└── README.md
```

### Best Practices

- Use fluent API for complex mappings
- Define indexes for performance-critical queries
- Configure cascade delete behaviors appropriately
- Use separate read/write contexts for CQRS
- Include data annotations for simple validations
- Follow EF Core migration best practices

🤝 Contributing

- **New Entities**: Create corresponding configuration classes
- **Relationships**: Define foreign keys and navigation properties
- **Indexes**: Add indexes for query performance
- **Migrations**: Test migrations thoroughly before applying
- **Performance**: Optimize queries and consider read replicas
- **Testing**: Add integration tests for data access

📄 License

Part of the MasLazu ASP.NET framework ecosystem.
