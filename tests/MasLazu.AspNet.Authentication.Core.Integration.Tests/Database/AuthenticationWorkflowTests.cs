using Microsoft.EntityFrameworkCore;
using MasLazu.AspNet.Authentication.Core.Domain.Entities;
using MasLazu.AspNet.Authentication.Core.EfCore.Data;
using MasLazu.AspNet.Authentication.Core.Integration.Tests.Fixtures;

namespace MasLazu.AspNet.Authentication.Core.Integration.Tests.Database;

public class AuthenticationWorkflowTests : IClassFixture<DatabaseFixture>
{
    private readonly DatabaseFixture _fixture;
    private readonly AuthenticationCoreDbContext _context;

    public AuthenticationWorkflowTests(DatabaseFixture fixture)
    {
        _fixture = fixture;
        _context = _fixture.CreateNewContext();
    }

    [Fact]
    public async Task CompleteAuthenticationFlow_ShouldCreateAllRequiredEntities()
    {
        // Arrange - Create a user
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = "Auth Flow User",
            Email = "authflow@test.com",
            Username = "authflowuser",
            IsEmailVerified = true,
            CreatedAt = DateTimeOffset.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Act - Create user login method
        var userLoginMethod = new UserLoginMethod
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            LoginMethodCode = "email_password",
            LastLoginAt = DateTimeOffset.UtcNow,
            CreatedAt = DateTimeOffset.UtcNow
        };

        _context.UserLoginMethods.Add(userLoginMethod);
        await _context.SaveChangesAsync();

        // Act - Create refresh token
        var refreshToken = new RefreshToken
        {
            Id = Guid.NewGuid(),
            Token = "test_refresh_token_123",
            ExpiresDate = DateTimeOffset.UtcNow.AddDays(30),
            CreatedAt = DateTimeOffset.UtcNow
        };

        _context.RefreshTokens.Add(refreshToken);
        await _context.SaveChangesAsync();

        // Act - Link user to refresh token
        var userRefreshToken = new UserRefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            RefreshTokenId = refreshToken.Id,
            CreatedAt = DateTimeOffset.UtcNow
        };

        _context.UserRefreshTokens.Add(userRefreshToken);
        await _context.SaveChangesAsync();

        // Assert - Verify all entities are created correctly
        User? savedUser = await _context.Users.FindAsync(user.Id);
        savedUser.Should().NotBeNull();

        UserLoginMethod? savedLoginMethod = await _context.UserLoginMethods
            .FirstOrDefaultAsync(ulm => ulm.UserId == user.Id);
        savedLoginMethod.Should().NotBeNull();
        savedLoginMethod!.LoginMethodCode.Should().Be("email_password");

        RefreshToken? savedRefreshToken = await _context.RefreshTokens.FindAsync(refreshToken.Id);
        savedRefreshToken.Should().NotBeNull();

        UserRefreshToken? savedUserRefreshToken = await _context.UserRefreshTokens
            .FirstOrDefaultAsync(urt => urt.UserId == user.Id && urt.RefreshTokenId == refreshToken.Id);
        savedUserRefreshToken.Should().NotBeNull();
    }

    [Fact]
    public async Task RefreshTokenRevocation_ShouldUpdateRevokedDate()
    {
        // Arrange
        var refreshToken = new RefreshToken
        {
            Id = Guid.NewGuid(),
            Token = "token_to_revoke",
            ExpiresDate = DateTimeOffset.UtcNow.AddDays(30),
            RevokedDate = null,
            CreatedAt = DateTimeOffset.UtcNow
        };

        _context.RefreshTokens.Add(refreshToken);
        await _context.SaveChangesAsync();

        // Act
        refreshToken.RevokedDate = DateTimeOffset.UtcNow;
        await _context.SaveChangesAsync();

        // Assert
        RefreshToken? revokedToken = await _context.RefreshTokens.FindAsync(refreshToken.Id);
        revokedToken.Should().NotBeNull();
        revokedToken!.RevokedDate.Should().NotBeNull();
    }

    [Fact]
    public async Task GetActiveRefreshTokensForUser_ShouldReturnNonRevokedTokens()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = "Token Test User",
            Email = "tokentest@test.com",
            CreatedAt = DateTimeOffset.UtcNow
        };

        var activeToken = new RefreshToken
        {
            Id = Guid.NewGuid(),
            Token = "active_token",
            ExpiresDate = DateTimeOffset.UtcNow.AddDays(30),
            RevokedDate = null,
            CreatedAt = DateTimeOffset.UtcNow
        };

        var revokedToken = new RefreshToken
        {
            Id = Guid.NewGuid(),
            Token = "revoked_token",
            ExpiresDate = DateTimeOffset.UtcNow.AddDays(30),
            RevokedDate = DateTimeOffset.UtcNow.AddDays(-1),
            CreatedAt = DateTimeOffset.UtcNow
        };

        _context.Users.Add(user);
        _context.RefreshTokens.AddRange(activeToken, revokedToken);
        await _context.SaveChangesAsync();

        _context.UserRefreshTokens.AddRange(
            new UserRefreshToken { Id = Guid.NewGuid(), UserId = user.Id, RefreshTokenId = activeToken.Id, CreatedAt = DateTimeOffset.UtcNow },
            new UserRefreshToken { Id = Guid.NewGuid(), UserId = user.Id, RefreshTokenId = revokedToken.Id, CreatedAt = DateTimeOffset.UtcNow }
        );
        await _context.SaveChangesAsync();

        // Act
        List<UserRefreshToken> activeTokens = await _context.UserRefreshTokens
            .Include(urt => urt.RefreshToken)
            .Where(urt => urt.UserId == user.Id && urt.RefreshToken!.RevokedDate == null)
            .ToListAsync();

        // Assert
        activeTokens.Should().HaveCount(1);
        activeTokens.First().RefreshToken!.Token.Should().Be("active_token");
    }

    [Fact]
    public async Task MultipleLoginMethods_ShouldAllowUserToHaveMultipleAuthMethods()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = "Multi Auth User",
            Email = "multiauth@test.com",
            CreatedAt = DateTimeOffset.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        UserLoginMethod[] loginMethods = new[]
        {
            new UserLoginMethod
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                LoginMethodCode = "email_password",
                LastLoginAt = DateTimeOffset.UtcNow,
                CreatedAt = DateTimeOffset.UtcNow
            },
            new UserLoginMethod
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                LoginMethodCode = "google",
                LastLoginAt = DateTimeOffset.UtcNow.AddDays(-1),
                CreatedAt = DateTimeOffset.UtcNow
            }
        };

        // Act
        _context.UserLoginMethods.AddRange(loginMethods);
        await _context.SaveChangesAsync();

        // Assert
        List<UserLoginMethod> userLoginMethods = await _context.UserLoginMethods
            .Where(ulm => ulm.UserId == user.Id)
            .ToListAsync();

        userLoginMethods.Should().HaveCount(2);
        userLoginMethods.Should().Contain(ulm => ulm.LoginMethodCode == "email_password");
        userLoginMethods.Should().Contain(ulm => ulm.LoginMethodCode == "google");
    }

    [Fact]
    public async Task UserVerification_ShouldUpdateVerificationFlags()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = "Verification Test User",
            Email = "verify@test.com",
            PhoneNumber = "+1234567890",
            IsEmailVerified = false,
            IsPhoneNumberVerified = false,
            CreatedAt = DateTimeOffset.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Act
        user.IsEmailVerified = true;
        await _context.SaveChangesAsync();

        // Assert
        User? verifiedUser = await _context.Users.FindAsync(user.Id);
        verifiedUser.Should().NotBeNull();
        verifiedUser!.IsEmailVerified.Should().BeTrue();
        verifiedUser.IsPhoneNumberVerified.Should().BeFalse();
    }

    [Fact]
    public async Task LoginMethodEnabled_ShouldOnlyReturnEnabledMethods()
    {
        // Act
        List<LoginMethod> enabledMethods = await _context.LoginMethods
            .Where(lm => lm.IsEnabled)
            .ToListAsync();

        // Assert
        enabledMethods.Should().NotBeEmpty();
        enabledMethods.Should().OnlyContain(lm => lm.IsEnabled == true);
        enabledMethods.Should().NotContain(lm => lm.Code == "facebook"); // facebook is disabled in seed
    }
}
