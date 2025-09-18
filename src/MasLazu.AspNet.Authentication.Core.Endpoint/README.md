# MasLazu.AspNet.Authentication.Core.Endpoint

This project contains the API endpoints for the MasLazu ASP.NET Authentication Core library.

## Overview

The `MasLazu.AspNet.Authentication.Core.Endpoint` project provides:

- **REST API Endpoints**: FastEndpoints implementations for authentication and user management
- **Endpoint Groups**: Organized API versioning and Swagger documentation
- **CRUD Operations**: Complete Create, Read, Update, Delete endpoints for all entities
- **Authentication Endpoints**: Login, verification, and user management APIs

## API Endpoints

### Users Endpoints

- `POST /users` - Create user
- `GET /users/{id}` - Get user by ID
- `GET /users` - Get users paginated
- `PUT /users/{id}` - Update user
- `DELETE /users/{id}` - Delete user
- `POST /users/send-email-verification` - Send email verification

### Login Methods Endpoints

- `GET /login-methods/{id}` - Get login method by ID
- `GET /login-methods` - Get login methods paginated
- `GET /login-methods/enabled` - Get all enabled login methods (anonymous)

### User Login Methods Endpoints

- `GET /user-login-methods` - Get user login methods paginated
- `GET /user-login-methods/{id}` - Get by ID
- `POST /user-login-methods` - Create
- `PUT /user-login-methods/{id}` - Update
- `DELETE /user-login-methods/{id}` - Delete

### Reference Data Endpoints

- **Genders**: CRUD operations for gender data
- **Languages**: CRUD operations for language data
- **Timezones**: CRUD operations for timezone data

## Endpoint Groups

- `UsersEndpointGroup`: `/api/v1/users/*`
- `LoginMethodsEndpointGroup`: `/api/v1/login-methods/*`
- `UserLoginMethodsEndpointGroup`: `/api/v1/user-login-methods/*`
- `GendersEndpointGroup`: `/api/v1/genders/*`
- `LanguagesEndpointGroup`: `/api/v1/languages/*`
- `TimezonesEndpointGroup`: `/api/v1/timezones/*`

## Project Configuration

- **Target Framework**: .NET 9.0
- **Implicit Usings**: Enabled
- **Nullable Reference Types**: Enabled

## Dependencies

- **Package References**:

  - `MasLazu.AspNet.Framework.Endpoint` - Base endpoint framework

- **Project References**:
  - `MasLazu.AspNet.Authentication.Core.Abstraction` - Service interfaces and models

## Usage

Register the endpoints in your application's startup:

```csharp
app.UseFastEndpoints();
```

The endpoints will be automatically discovered and registered with FastEndpoints.

## Authentication & Authorization

- Most endpoints require authentication
- `GetAllEnabledLoginMethodsEndpoint` allows anonymous access
- JWT tokens are used for authentication
- Role-based authorization can be implemented as needed

## Response Format

All endpoints return standardized responses:

```json
{
  "data": { ... },
  "message": "Operation completed successfully",
  "success": true
}
```

## Error Handling

- Validation errors return 400 Bad Request
- Not found errors return 404 Not Found
- Unauthorized access returns 401 Unauthorized
- Forbidden access returns 403 Forbidden
- Server errors return 500 Internal Server Error

🛠️ Development Guidelines

### Naming Conventions

- **Endpoints**: Suffix with Endpoint (e.g., CreateUserEndpoint)
- **Endpoint Groups**: Suffix with EndpointGroup (e.g., UsersEndpointGroup)
- **Extensions**: Suffix with Extension (e.g., AuthenticationCoreEndpointExtension)
- **HTTP Methods**: Use appropriate verbs (GET, POST, PUT, DELETE)

### Code Structure

```
src/MasLazu.AspNet.Authentication.Core.Endpoint/
├── Endpoints/             # API endpoint implementations
│   ├── Users/            # User-related endpoints
│   ├── LoginMethods/     # Login method endpoints
│   ├── Genders/          # Gender endpoints
│   └── ...
├── EndpointGroups/       # API grouping and versioning
├── Extensions/           # DI registration extensions
├── MasLazu.AspNet.Authentication.Core.Endpoint.csproj
└── README.md
```

### Best Practices

- Extend `BaseEndpoint<TRequest, TResponse>` for standard endpoints
- Use `BaseEndpointWithoutRequest<TResponse>` for GET endpoints
- Implement proper validation using FluentValidation
- Include XML documentation for Swagger generation
- Follow RESTful API design principles
- Use appropriate HTTP status codes

🤝 Contributing

- **New Endpoints**: Follow existing patterns and naming conventions
- **Endpoint Groups**: Create groups for new resource types
- **Validation**: Add request validation using validators
- **Documentation**: Include XML comments for Swagger docs
- **Testing**: Add integration tests for new endpoints
- **Security**: Implement proper authentication and authorization

📄 License

Part of the MasLazu ASP.NET framework ecosystem.
