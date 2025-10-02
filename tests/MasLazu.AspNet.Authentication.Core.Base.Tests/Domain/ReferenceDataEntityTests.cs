using MasLazu.AspNet.Authentication.Core.Domain.Entities;

namespace MasLazu.AspNet.Authentication.Core.Base.Tests.Domain;

public class ReferenceDataEntityTests
{
    [Fact]
    public void Gender_ShouldInitializeCorrectly()
    {
        // Act
        var gender = new Gender
        {
            Code = "male",
            Name = "Male",
            CreatedAt = DateTimeOffset.UtcNow
        };

        // Assert
        gender.Code.Should().Be("male");
        gender.Name.Should().Be("Male");
        gender.CreatedAt.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void Language_ShouldInitializeCorrectly()
    {
        // Act
        var language = new Language
        {
            Code = "en",
            Name = "English",
            CreatedAt = DateTimeOffset.UtcNow
        };

        // Assert
        language.Code.Should().Be("en");
        language.Name.Should().Be("English");
    }

    [Fact]
    public void Timezone_ShouldHaveGuidId()
    {
        // Act
        var timezoneId = Guid.NewGuid();
        var timezone = new Timezone
        {
            Id = timezoneId,
            Identifier = "UTC",
            Name = "Coordinated Universal Time",
            OffsetMinutes = 0,
            CreatedAt = DateTimeOffset.UtcNow
        };

        // Assert
        timezone.Id.Should().Be(timezoneId);
        timezone.Identifier.Should().Be("UTC");
        timezone.Name.Should().Be("Coordinated Universal Time");
        timezone.OffsetMinutes.Should().Be(0);
    }

    [Fact]
    public void LoginMethod_ShouldSupportEnabledFlag()
    {
        // Arrange & Act
        var enabledMethod = new LoginMethod
        {
            Code = "email",
            Name = "Email",
            IsEnabled = true
        };

        var disabledMethod = new LoginMethod
        {
            Code = "facebook",
            Name = "Facebook",
            IsEnabled = false
        };

        // Assert
        enabledMethod.IsEnabled.Should().BeTrue();
        disabledMethod.IsEnabled.Should().BeFalse();
    }

    [Fact]
    public void UserRefreshToken_ShouldLinkUserAndRefreshToken()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var refreshTokenId = Guid.NewGuid();

        // Act
        var userRefreshToken = new UserRefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            RefreshTokenId = refreshTokenId,
            CreatedAt = DateTimeOffset.UtcNow
        };

        // Assert
        userRefreshToken.UserId.Should().Be(userId);
        userRefreshToken.RefreshTokenId.Should().Be(refreshTokenId);
    }
}
