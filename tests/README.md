# Test Suite for MasLazu.AspNet.Authentication.Core

Comprehensive test coverage for the authentication and user management system.

## 📦 Test Projects

### MasLazu.AspNet.Authentication.Core.Base.Tests

**Unit tests** for the core business logic layer:

- **Services Tests**

  - `AuthServiceTests.cs` - Authentication service with JWT token generation and login methods
  - `UserServiceTests.cs` - User management including verification and validation

- **Utils Tests**

  - `JwtUtilTests.cs` - JWT token generation, validation, and refresh token handling

- **Validators Tests**

  - `CreateUserRequestValidatorTests.cs` - Input validation for user creation

- **Consumers Tests**

  - `VerificationCompletedConsumerTests.cs` - Email verification event handling

- **Domain Tests**
  - `EntityTests.cs` - Domain entity behavior and relationships

### MasLazu.AspNet.Authentication.Core.Integration.Tests

**Integration tests** for database operations and workflows:

- **Database Tests**

  - `UserRepositoryTests.cs` - CRUD operations for users
  - `AuthenticationWorkflowTests.cs` - Complete authentication flows

- **Fixtures**
  - `DatabaseFixture.cs` - Shared test database context with seed data

## 🚀 Running Tests

### Run All Tests

```bash
dotnet test
```

### Run Specific Test Project

```bash
# Unit tests
dotnet test tests/MasLazu.AspNet.Authentication.Core.Base.Tests/MasLazu.AspNet.Authentication.Core.Base.Tests.csproj

# Integration tests
dotnet test tests/MasLazu.AspNet.Authentication.Core.Integration.Tests/MasLazu.AspNet.Authentication.Core.Integration.Tests.csproj
```

### Run with Coverage

```bash
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
```

### Run Specific Test

```bash
dotnet test --filter "FullyQualifiedName~AuthServiceTests.LoginAsync_WithValidCredentials_ShouldReturnLoginResponse"
```

### Run Tests by Category

```bash
# Run only unit tests
dotnet test --filter "Category=Unit"

# Run only integration tests
dotnet test --filter "Category=Integration"
```

## 📊 Test Coverage

### Unit Tests Coverage

- ✅ **AuthService**: Login flows, token generation, error handling
- ✅ **UserService**: User CRUD, email verification, validation
- ✅ **JwtUtil**: Token generation, validation, expiration, claims
- ✅ **Validators**: Input validation rules for all request models
- ✅ **Consumers**: Event handling for verification completion
- ✅ **Domain Entities**: Entity behavior and relationships

### Integration Tests Coverage

- ✅ **Database Operations**: CRUD operations with EF Core
- ✅ **Authentication Flows**: Complete login and token management
- ✅ **User Verification**: Email verification workflow
- ✅ **Refresh Tokens**: Token revocation and active token queries
- ✅ **Login Methods**: Multiple authentication methods per user

## 🧪 Test Patterns

### Arrange-Act-Assert (AAA)

All tests follow the AAA pattern:

```csharp
[Fact]
public async Task MethodName_Condition_ExpectedBehavior()
{
    // Arrange - Setup test data and mocks
    var userId = Guid.NewGuid();

    // Act - Execute the method under test
    var result = await service.GetUserAsync(userId);

    // Assert - Verify the results
    result.Should().NotBeNull();
}
```

### Test Naming Convention

```
{MethodName}_{Scenario}_{ExpectedResult}
```

Examples:

- `LoginAsync_WithValidCredentials_ShouldReturnLoginResponse`
- `VerifyEmailAsync_WithInvalidUserId_ShouldThrowNotFoundException`

## 🔧 Testing Tools

- **xUnit** - Test framework
- **Moq** - Mocking framework for dependencies
- **FluentAssertions** - Readable assertion library
- **EF Core InMemory** - In-memory database for integration tests
- **Coverlet** - Code coverage tool

## 📝 Writing New Tests

### Unit Test Example

```csharp
public class NewServiceTests
{
    private readonly Mock<IDependency> _dependencyMock;
    private readonly NewService _service;

    public NewServiceTests()
    {
        _dependencyMock = new Mock<IDependency>();
        _service = new NewService(_dependencyMock.Object);
    }

    [Fact]
    public async Task NewMethod_WithValidInput_ShouldSucceed()
    {
        // Arrange
        _dependencyMock
            .Setup(x => x.DoSomething(It.IsAny<string>()))
            .ReturnsAsync("result");

        // Act
        var result = await _service.NewMethod("input");

        // Assert
        result.Should().Be("expected");
        _dependencyMock.Verify(x => x.DoSomething("input"), Times.Once);
    }
}
```

### Integration Test Example

```csharp
public class NewWorkflowTests : IClassFixture<DatabaseFixture>
{
    private readonly DatabaseFixture _fixture;

    public NewWorkflowTests(DatabaseFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task CompleteWorkflow_ShouldPersistAllChanges()
    {
        // Arrange
        var context = _fixture.CreateNewContext();

        // Act
        // ... perform operations
        await context.SaveChangesAsync();

        // Assert
        var result = await context.Entities.FindAsync(id);
        result.Should().NotBeNull();
    }
}
```

## 🎯 Best Practices

1. **Test Independence**: Each test should be independent and not rely on other tests
2. **Clear Names**: Use descriptive test names that explain the scenario
3. **One Assert**: Focus on testing one thing per test method
4. **Mock External Dependencies**: Use mocks for external services and repositories
5. **Use Test Fixtures**: Share expensive setup (like database contexts) with fixtures
6. **Async/Await**: Use async tests for async methods
7. **Edge Cases**: Test both happy paths and error scenarios
8. **Arrange First**: Keep test data setup clear and organized

## 📈 Continuous Integration

Tests are automatically run on:

- Every push to the repository
- Pull request creation/update
- Before deployment

## 🐛 Debugging Tests

### Visual Studio

1. Set breakpoints in test methods
2. Right-click test → Debug Test(s)

### VS Code

1. Install C# extension
2. Set breakpoints
3. Use Test Explorer to debug

### Command Line

```bash
# Run with verbose output
dotnet test --logger "console;verbosity=detailed"
```

## 📚 Resources

- [xUnit Documentation](https://xunit.net/)
- [Moq Quick Start](https://github.com/moq/moq4)
- [FluentAssertions Documentation](https://fluentassertions.com/)
- [EF Core Testing](https://learn.microsoft.com/en-us/ef/core/testing/)

## 🤝 Contributing

When adding new features:

1. Write tests first (TDD approach recommended)
2. Ensure all existing tests pass
3. Aim for >80% code coverage
4. Update this README if adding new test categories

## 📞 Support

For questions about tests or testing approach, please refer to the main project documentation or create an issue.
