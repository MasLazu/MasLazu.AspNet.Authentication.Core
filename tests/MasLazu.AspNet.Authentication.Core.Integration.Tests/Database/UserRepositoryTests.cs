using Microsoft.EntityFrameworkCore;
using MasLazu.AspNet.Authentication.Core.Domain.Entities;
using MasLazu.AspNet.Authentication.Core.EfCore.Data;
using MasLazu.AspNet.Authentication.Core.Integration.Tests.Fixtures;

namespace MasLazu.AspNet.Authentication.Core.Integration.Tests.Database;

public class UserRepositoryTests : IClassFixture<DatabaseFixture>
{
    private readonly DatabaseFixture _fixture;
    private readonly AuthenticationCoreDbContext _context;

    public UserRepositoryTests(DatabaseFixture fixture)
    {
        _fixture = fixture;
        _context = _fixture.CreateNewContext();
    }

    [Fact]
    public async Task AddUser_ShouldPersistUserToDatabase()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = "Integration Test User",
            Email = "integration@test.com",
            Username = "integrationuser",
            LanguageCode = "en",
            GenderCode = "male",
            IsEmailVerified = false,
            IsPhoneNumberVerified = false,
            CreatedAt = DateTimeOffset.UtcNow
        };

        // Act
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Assert
        User? savedUser = await _context.Users.FindAsync(user.Id);
        savedUser.Should().NotBeNull();
        savedUser!.Name.Should().Be("Integration Test User");
        savedUser.Email.Should().Be("integration@test.com");
        savedUser.Username.Should().Be("integrationuser");
    }

    [Fact]
    public async Task GetUserByEmail_ShouldReturnCorrectUser()
    {
        // Arrange
        string email = "findme@test.com";
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = "Find Me User",
            Email = email,
            CreatedAt = DateTimeOffset.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Act
        User? foundUser = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);

        // Assert
        foundUser.Should().NotBeNull();
        foundUser!.Email.Should().Be(email);
        foundUser.Name.Should().Be("Find Me User");
    }

    [Fact]
    public async Task GetUserWithRelations_ShouldIncludeRelatedEntities()
    {
        // Arrange
        var userId = Guid.NewGuid();
        Guid timezoneId = _context.Timezones.First().Id;

        var user = new User
        {
            Id = userId,
            Name = "User With Relations",
            Email = "relations@test.com",
            LanguageCode = "en",
            GenderCode = "male",
            TimezoneId = timezoneId,
            CreatedAt = DateTimeOffset.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Act
        User? userWithRelations = await _context.Users
            .Include(u => u.Language)
            .Include(u => u.Gender)
            .Include(u => u.Timezone)
            .FirstOrDefaultAsync(u => u.Id == userId);

        // Assert
        userWithRelations.Should().NotBeNull();
        userWithRelations!.Language.Should().NotBeNull();
        userWithRelations.Gender.Should().NotBeNull();
        userWithRelations.Timezone.Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateUser_ShouldModifyExistingUser()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = "Original Name",
            Email = "original@test.com",
            IsEmailVerified = false,
            CreatedAt = DateTimeOffset.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Act
        user.Name = "Updated Name";
        user.IsEmailVerified = true;
        user.UpdatedAt = DateTimeOffset.UtcNow;
        await _context.SaveChangesAsync();

        // Assert
        User? updatedUser = await _context.Users.FindAsync(user.Id);
        updatedUser.Should().NotBeNull();
        updatedUser!.Name.Should().Be("Updated Name");
        updatedUser.IsEmailVerified.Should().BeTrue();
        updatedUser.UpdatedAt.Should().NotBeNull();
    }

    [Fact]
    public async Task DeleteUser_ShouldRemoveUserFromDatabase()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = "To Be Deleted",
            Email = "delete@test.com",
            CreatedAt = DateTimeOffset.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Act
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        // Assert
        User? deletedUser = await _context.Users.FindAsync(user.Id);
        deletedUser.Should().BeNull();
    }

    [Fact]
    public async Task CheckEmailExists_WithExistingEmail_ShouldReturnTrue()
    {
        // Arrange
        string email = "exists@test.com";
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = "Existing User",
            Email = email,
            CreatedAt = DateTimeOffset.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Act
        bool exists = await _context.Users.AnyAsync(u => u.Email == email);

        // Assert
        exists.Should().BeTrue();
    }

    [Fact]
    public async Task CheckEmailExists_WithNonExistingEmail_ShouldReturnFalse()
    {
        // Arrange
        string email = "nonexisting@test.com";

        // Act
        bool exists = await _context.Users.AnyAsync(u => u.Email == email);

        // Assert
        exists.Should().BeFalse();
    }

    [Fact]
    public async Task GetUsersCount_ShouldReturnCorrectCount()
    {
        // Arrange
        int initialCount = await _context.Users.CountAsync();

        User[] users = new[]
        {
            new User { Id = Guid.NewGuid(), Name = "User 1", Email = "user1@count.com", CreatedAt = DateTimeOffset.UtcNow },
            new User { Id = Guid.NewGuid(), Name = "User 2", Email = "user2@count.com", CreatedAt = DateTimeOffset.UtcNow },
            new User { Id = Guid.NewGuid(), Name = "User 3", Email = "user3@count.com", CreatedAt = DateTimeOffset.UtcNow }
        };

        _context.Users.AddRange(users);
        await _context.SaveChangesAsync();

        // Act
        int finalCount = await _context.Users.CountAsync();

        // Assert
        finalCount.Should().Be(initialCount + 3);
    }
}
