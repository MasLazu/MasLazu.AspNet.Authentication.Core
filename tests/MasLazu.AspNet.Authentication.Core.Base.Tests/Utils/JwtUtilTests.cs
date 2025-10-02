using Microsoft.Extensions.Options;
using MasLazu.AspNet.Authentication.Core.Base.Configuration;
using MasLazu.AspNet.Authentication.Core.Base.Utils;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MasLazu.AspNet.Authentication.Core.Base.Tests.Utils;

public class JwtUtilTests
{
    private readonly JwtUtil _jwtUtil;
    private readonly JwtConfiguration _jwtConfig;

    public JwtUtilTests()
    {
        _jwtConfig = new JwtConfiguration
        {
            Key = "ThisIsAVerySecretKeyForTestingPurposesOnly1234567890",
            Issuer = "TestIssuer",
            Audience = "TestAudience",
            AccessTokenExpirationMinutes = 120,
            RefreshTokenExpirationDays = 30
        };

        IOptions<JwtConfiguration> options = Options.Create(_jwtConfig);
        _jwtUtil = new JwtUtil(options);
    }

    [Fact]
    public void GenerateAccessToken_WithValidParameters_ShouldReturnValidToken()
    {
        // Arrange
        var userId = Guid.NewGuid();
        string email = "test@example.com";

        // Act
        (string? token, DateTimeOffset expiresAt) = _jwtUtil.GenerateAccessToken(userId, email);

        // Assert
        token.Should().NotBeNullOrEmpty();
        expiresAt.Should().BeAfter(DateTimeOffset.UtcNow);
        expiresAt.Should().BeCloseTo(DateTimeOffset.UtcNow.AddMinutes(_jwtConfig.AccessTokenExpirationMinutes), TimeSpan.FromSeconds(5));

        // Verify token structure
        var handler = new JwtSecurityTokenHandler();
        JwtSecurityToken jwtToken = handler.ReadJwtToken(token);
        jwtToken.Issuer.Should().Be(_jwtConfig.Issuer);
        jwtToken.Audiences.Should().Contain(_jwtConfig.Audience);
        jwtToken.Claims.Should().Contain(c => c.Type == JwtRegisteredClaimNames.Sub && c.Value == userId.ToString());
        jwtToken.Claims.Should().Contain(c => c.Type == JwtRegisteredClaimNames.Email && c.Value == email);
    }

    [Fact]
    public void GenerateAccessToken_WithRoles_ShouldIncludeRolesInToken()
    {
        // Arrange
        var userId = Guid.NewGuid();
        string email = "test@example.com";
        string[] roles = new[] { "Admin", "User" };

        // Act
        (string? token, DateTimeOffset _) = _jwtUtil.GenerateAccessToken(userId, email, roles);

        // Assert
        var handler = new JwtSecurityTokenHandler();
        JwtSecurityToken jwtToken = handler.ReadJwtToken(token);
        // JWT tokens may use the short form "role" instead of ClaimTypes.Role
        var roleClaims = jwtToken.Claims
            .Where(c => c.Type == ClaimTypes.Role || c.Type == "role")
            .Select(c => c.Value)
            .ToList();
        roleClaims.Should().Contain("Admin");
        roleClaims.Should().Contain("User");
    }

    [Fact]
    public void GenerateAccessToken_WithAdditionalClaims_ShouldIncludeClaimsInToken()
    {
        // Arrange
        var userId = Guid.NewGuid();
        string email = "test@example.com";
        var additionalClaims = new Dictionary<string, object>
        {
            { "tenant_id", "tenant-123" },
            { "subscription", "premium" }
        };

        // Act
        (string? token, DateTimeOffset _) = _jwtUtil.GenerateAccessToken(userId, email, null, additionalClaims);

        // Assert
        var handler = new JwtSecurityTokenHandler();
        JwtSecurityToken jwtToken = handler.ReadJwtToken(token);
        jwtToken.Claims.Should().Contain(c => c.Type == "tenant_id" && c.Value == "tenant-123");
        jwtToken.Claims.Should().Contain(c => c.Type == "subscription" && c.Value == "premium");
    }

    [Fact]
    public void GenerateRefreshToken_ShouldReturnValidToken()
    {
        // Arrange
        var userId = Guid.NewGuid();

        // Act
        (string? token, DateTimeOffset expiresAt) = _jwtUtil.GenerateRefreshToken(userId);

        // Assert
        token.Should().NotBeNullOrEmpty();
        expiresAt.Should().BeAfter(DateTimeOffset.UtcNow);
        expiresAt.Should().BeCloseTo(DateTimeOffset.UtcNow.AddDays(_jwtConfig.RefreshTokenExpirationDays), TimeSpan.FromSeconds(5));
    }

    [Fact]
    public void ValidateAccessToken_WithValidToken_ShouldReturnClaimsPrincipal()
    {
        // Arrange
        var userId = Guid.NewGuid();
        string email = "test@example.com";
        (string? token, DateTimeOffset _) = _jwtUtil.GenerateAccessToken(userId, email);

        // Ensure token was generated
        token.Should().NotBeNullOrEmpty();

        // Act
        ClaimsPrincipal? principal = _jwtUtil.ValidateAccessToken(token!);

        // Assert
        principal.Should().NotBeNull();
        // JwtSecurityTokenHandler maps "sub" to ClaimTypes.NameIdentifier
        principal!.Claims.Should().Contain(c => c.Type == ClaimTypes.NameIdentifier && c.Value == userId.ToString());
        // JwtSecurityTokenHandler maps "email" to ClaimTypes.Email
        principal.Claims.Should().Contain(c => c.Type == ClaimTypes.Email && c.Value == email);
    }

    [Fact]
    public void ValidateAccessToken_WithInvalidToken_ShouldReturnNull()
    {
        // Arrange
        string invalidToken = "invalid.token.here";

        // Act
        ClaimsPrincipal? principal = _jwtUtil.ValidateAccessToken(invalidToken);

        // Assert
        principal.Should().BeNull();
    }

    [Fact]
    public void ValidateAccessToken_WithTokenFromDifferentKey_ShouldReturnNull()
    {
        // Arrange
        // Create a token with a different signing key
        var differentConfig = new JwtConfiguration
        {
            Key = "DifferentSecretKeyForTestingPurposesOnly1234567890",
            Issuer = _jwtConfig.Issuer,
            Audience = _jwtConfig.Audience,
            AccessTokenExpirationMinutes = 120,
            RefreshTokenExpirationDays = 30
        };
        var differentJwtUtil = new JwtUtil(Options.Create(differentConfig));
        var userId = Guid.NewGuid();
        (string? token, DateTimeOffset _) = differentJwtUtil.GenerateAccessToken(userId, "test@example.com");

        // Act - Validate with the original util (different key)
        ClaimsPrincipal? principal = _jwtUtil.ValidateAccessToken(token);

        // Assert
        principal.Should().BeNull();
    }

    [Fact]
    public void ValidateRefreshToken_WithValidToken_ShouldReturnTrue()
    {
        // Arrange
        var userId = Guid.NewGuid();
        (string? token, DateTimeOffset _) = _jwtUtil.GenerateRefreshToken(userId);

        // Act
        bool isValid = _jwtUtil.ValidateRefreshToken(token);

        // Assert
        isValid.Should().BeTrue();
    }

    [Fact]
    public void ValidateRefreshToken_WithInvalidToken_ShouldReturnFalse()
    {
        // Arrange
        string invalidToken = "invalid-token";

        // Act
        bool isValid = _jwtUtil.ValidateRefreshToken(invalidToken);

        // Assert
        isValid.Should().BeFalse();
    }

    [Fact]
    public void ValidateRefreshToken_WithMalformedToken_ShouldReturnFalse()
    {
        // Arrange
        string malformedToken = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("malformed:data"));

        // Act
        bool isValid = _jwtUtil.ValidateRefreshToken(malformedToken);

        // Assert
        isValid.Should().BeFalse();
    }

    [Fact]
    public void GetUserIdFromRefreshToken_WithValidToken_ShouldReturnUserId()
    {
        // Arrange
        var userId = Guid.NewGuid();
        (string? token, DateTimeOffset _) = _jwtUtil.GenerateRefreshToken(userId);

        // Act
        string? extractedUserId = _jwtUtil.GetUserIdFromRefreshToken(token);

        // Assert
        extractedUserId.Should().NotBeNullOrEmpty();
        extractedUserId.Should().Be(userId.ToString());
    }

    [Fact]
    public void GetUserIdFromRefreshToken_WithInvalidToken_ShouldReturnNull()
    {
        // Arrange
        string invalidToken = "invalid-token";

        // Act
        string? userId = _jwtUtil.GetUserIdFromRefreshToken(invalidToken);

        // Assert
        userId.Should().BeNull();
    }

    [Fact]
    public void AccessTokenExpirationMinutes_ShouldReturnConfiguredValue()
    {
        // Act
        int expirationMinutes = _jwtUtil.AccessTokenExpirationMinutes;

        // Assert
        expirationMinutes.Should().Be(_jwtConfig.AccessTokenExpirationMinutes);
    }

    [Fact]
    public void RefreshTokenExpirationDays_ShouldReturnConfiguredValue()
    {
        // Act
        int expirationDays = _jwtUtil.RefreshTokenExpirationDays;

        // Assert
        expirationDays.Should().Be(_jwtConfig.RefreshTokenExpirationDays);
    }

    [Fact]
    public void GenerateAccessToken_MultipleCalls_ShouldGenerateDifferentTokens()
    {
        // Arrange
        var userId = Guid.NewGuid();
        string email = "test@example.com";

        // Act
        (string? token1, DateTimeOffset _) = _jwtUtil.GenerateAccessToken(userId, email);
        Thread.Sleep(100); // Small delay to ensure different timestamp
        (string? token2, DateTimeOffset _) = _jwtUtil.GenerateAccessToken(userId, email);

        // Assert
        token1.Should().NotBe(token2);
    }

    [Fact]
    public void GenerateRefreshToken_MultipleCalls_ShouldGenerateDifferentTokens()
    {
        // Arrange
        var userId = Guid.NewGuid();

        // Act
        (string? token1, DateTimeOffset _) = _jwtUtil.GenerateRefreshToken(userId);
        (string? token2, DateTimeOffset _) = _jwtUtil.GenerateRefreshToken(userId);

        // Assert
        token1.Should().NotBe(token2);
    }
}
