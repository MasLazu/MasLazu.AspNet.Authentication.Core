using MasLazu.AspNet.Authentication.Core.Domain.Entities;

namespace MasLazu.AspNet.Authentication.Core.Base.Tests.Domain;

public class UserEntityTests
{
    [Fact]
    public void User_ShouldInitializeWithDefaultValues()
    {
        // Act
        var user = new User();

        // Assert
        user.Name.Should().Be(string.Empty);
        user.Email.Should().BeNull();
        user.PhoneNumber.Should().BeNull();
        user.Username.Should().BeNull();
        user.LanguageCode.Should().BeNull();
        user.TimezoneId.Should().BeNull();
        user.ProfilePicture.Should().BeNull();
        user.GenderCode.Should().BeNull();
        user.IsEmailVerified.Should().BeFalse();
        user.IsPhoneNumberVerified.Should().BeFalse();
        user.UserLoginMethods.Should().NotBeNull();
        user.UserLoginMethods.Should().BeEmpty();
    }

    [Fact]
    public void User_ShouldAllowSettingAllProperties()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var timezoneId = Guid.NewGuid();

        // Act
        var user = new User
        {
            Id = userId,
            Name = "Test User",
            Email = "test@example.com",
            PhoneNumber = "+1234567890",
            Username = "testuser",
            LanguageCode = "en",
            TimezoneId = timezoneId,
            ProfilePicture = "https://example.com/profile.jpg",
            GenderCode = "male",
            IsEmailVerified = true,
            IsPhoneNumberVerified = true,
            CreatedAt = DateTimeOffset.UtcNow
        };

        // Assert
        user.Id.Should().Be(userId);
        user.Name.Should().Be("Test User");
        user.Email.Should().Be("test@example.com");
        user.PhoneNumber.Should().Be("+1234567890");
        user.Username.Should().Be("testuser");
        user.LanguageCode.Should().Be("en");
        user.TimezoneId.Should().Be(timezoneId);
        user.ProfilePicture.Should().Be("https://example.com/profile.jpg");
        user.GenderCode.Should().Be("male");
        user.IsEmailVerified.Should().BeTrue();
        user.IsPhoneNumberVerified.Should().BeTrue();
    }

    [Fact]
    public void User_ShouldAllowNavigationProperties()
    {
        // Arrange
        var user = new User { Id = Guid.NewGuid() };
        var timezone = new Timezone { Id = Guid.NewGuid(), Identifier = "UTC", Name = "UTC", OffsetMinutes = 0 };
        var gender = new Gender { Code = "male", Name = "Male" };
        var language = new Language { Code = "en", Name = "English" };

        // Act
        user.Timezone = timezone;
        user.Gender = gender;
        user.Language = language;

        // Assert
        user.Timezone.Should().NotBeNull();
        user.Timezone!.Identifier.Should().Be("UTC");
        user.Gender.Should().NotBeNull();
        user.Gender!.Code.Should().Be("male");
        user.Language.Should().NotBeNull();
        user.Language!.Code.Should().Be("en");
    }

    [Fact]
    public void User_ShouldAllowAddingUserLoginMethods()
    {
        // Arrange
        var user = new User { Id = Guid.NewGuid() };
        var loginMethod1 = new UserLoginMethod
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            LoginMethodCode = "email_password"
        };
        var loginMethod2 = new UserLoginMethod
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            LoginMethodCode = "google"
        };

        // Act
        user.UserLoginMethods.Add(loginMethod1);
        user.UserLoginMethods.Add(loginMethod2);

        // Assert
        user.UserLoginMethods.Should().HaveCount(2);
        user.UserLoginMethods.Should().Contain(loginMethod1);
        user.UserLoginMethods.Should().Contain(loginMethod2);
    }
}

public class RefreshTokenEntityTests
{
    [Fact]
    public void RefreshToken_ShouldInitializeWithDefaultValues()
    {
        // Act
        var refreshToken = new RefreshToken();

        // Assert
        refreshToken.Token.Should().Be(string.Empty);
        refreshToken.ExpiresDate.Should().BeNull();
        refreshToken.RevokedDate.Should().BeNull();
    }

    [Fact]
    public void RefreshToken_ShouldAllowSettingAllProperties()
    {
        // Arrange
        var tokenId = Guid.NewGuid();
        DateTimeOffset expiresDate = DateTimeOffset.UtcNow.AddDays(30);
        DateTimeOffset revokedDate = DateTimeOffset.UtcNow;

        // Act
        var refreshToken = new RefreshToken
        {
            Id = tokenId,
            Token = "test_token_123",
            ExpiresDate = expiresDate,
            RevokedDate = revokedDate,
            CreatedAt = DateTimeOffset.UtcNow
        };

        // Assert
        refreshToken.Id.Should().Be(tokenId);
        refreshToken.Token.Should().Be("test_token_123");
        refreshToken.ExpiresDate.Should().Be(expiresDate);
        refreshToken.RevokedDate.Should().Be(revokedDate);
    }

    [Fact]
    public void RefreshToken_ShouldIndicateIfTokenIsExpired()
    {
        // Arrange
        var expiredToken = new RefreshToken
        {
            ExpiresDate = DateTimeOffset.UtcNow.AddDays(-1)
        };

        var validToken = new RefreshToken
        {
            ExpiresDate = DateTimeOffset.UtcNow.AddDays(1)
        };

        // Assert
        expiredToken.ExpiresDate.Should().BeBefore(DateTimeOffset.UtcNow);
        validToken.ExpiresDate.Should().BeAfter(DateTimeOffset.UtcNow);
    }

    [Fact]
    public void RefreshToken_ShouldIndicateIfTokenIsRevoked()
    {
        // Arrange
        var revokedToken = new RefreshToken
        {
            RevokedDate = DateTimeOffset.UtcNow.AddDays(-1)
        };

        var activeToken = new RefreshToken
        {
            RevokedDate = null
        };

        // Assert
        revokedToken.RevokedDate.Should().NotBeNull();
        activeToken.RevokedDate.Should().BeNull();
    }
}

public class UserLoginMethodEntityTests
{
    [Fact]
    public void UserLoginMethod_ShouldInitializeWithDefaultValues()
    {
        // Act
        var userLoginMethod = new UserLoginMethod();

        // Assert
        userLoginMethod.LoginMethodCode.Should().Be(string.Empty);
        userLoginMethod.LastLoginAt.Should().BeNull();
    }

    [Fact]
    public void UserLoginMethod_ShouldAllowSettingAllProperties()
    {
        // Arrange
        var id = Guid.NewGuid();
        var userId = Guid.NewGuid();
        DateTimeOffset lastLoginAt = DateTimeOffset.UtcNow;

        // Act
        var userLoginMethod = new UserLoginMethod
        {
            Id = id,
            UserId = userId,
            LoginMethodCode = "email_password",
            LastLoginAt = lastLoginAt,
            CreatedAt = DateTimeOffset.UtcNow
        };

        // Assert
        userLoginMethod.Id.Should().Be(id);
        userLoginMethod.UserId.Should().Be(userId);
        userLoginMethod.LoginMethodCode.Should().Be("email_password");
        userLoginMethod.LastLoginAt.Should().Be(lastLoginAt);
    }

    [Fact]
    public void UserLoginMethod_ShouldAllowUserNavigation()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new User { Id = userId, Name = "Test User" };
        var userLoginMethod = new UserLoginMethod
        {
            UserId = userId,
            LoginMethodCode = "google",
            // Act
            User = user
        };

        // Assert
        userLoginMethod.User.Should().NotBeNull();
        userLoginMethod.User!.Id.Should().Be(userId);
        userLoginMethod.User.Name.Should().Be("Test User");
    }
}

public class LoginMethodEntityTests
{
    [Fact]
    public void LoginMethod_ShouldHaveCodeAndName()
    {
        // Act
        var loginMethod = new LoginMethod
        {
            Code = "email_password",
            Name = "Email Password",
            IsEnabled = true
        };

        // Assert
        loginMethod.Code.Should().Be("email_password");
        loginMethod.Name.Should().Be("Email Password");
        loginMethod.IsEnabled.Should().BeTrue();
    }

    [Fact]
    public void LoginMethod_ShouldSupportDisabledState()
    {
        // Act
        var loginMethod = new LoginMethod
        {
            Code = "facebook",
            Name = "Facebook",
            IsEnabled = false
        };

        // Assert
        loginMethod.IsEnabled.Should().BeFalse();
    }
}
