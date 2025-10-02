using System.Linq.Expressions;
using MasLazu.AspNet.Authentication.Core.Abstraction.Interfaces;
using MasLazu.AspNet.Authentication.Core.Abstraction.Models;
using MasLazu.AspNet.Authentication.Core.Base.Services;
using MasLazu.AspNet.Authentication.Core.Base.Utils;
using MasLazu.AspNet.Authentication.Core.Domain.Entities;
using MasLazu.AspNet.Framework.Application.Exceptions;
using MasLazu.AspNet.Framework.Application.Interfaces;
using Microsoft.Extensions.Options;
using MasLazu.AspNet.Authentication.Core.Base.Configuration;

namespace MasLazu.AspNet.Authentication.Core.Base.Tests.Services;

public class AuthServiceTests
{
    private readonly Mock<IRepository<User>> _userRepositoryMock;
    private readonly Mock<IReadRepository<LoginMethod>> _loginMethodRepositoryMock;
    private readonly Mock<IRepository<UserLoginMethod>> _userLoginMethodRepositoryMock;
    private readonly Mock<IRepository<RefreshToken>> _refreshTokenRepositoryMock;
    private readonly Mock<IRepository<UserRefreshToken>> _userRefreshTokenRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly JwtUtil _jwtUtil;
    private readonly IAuthService _authService;

    public AuthServiceTests()
    {
        _userRepositoryMock = new Mock<IRepository<User>>();
        _loginMethodRepositoryMock = new Mock<IReadRepository<LoginMethod>>();
        _userLoginMethodRepositoryMock = new Mock<IRepository<UserLoginMethod>>();
        _refreshTokenRepositoryMock = new Mock<IRepository<RefreshToken>>();
        _userRefreshTokenRepositoryMock = new Mock<IRepository<UserRefreshToken>>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        IOptions<JwtConfiguration> jwtConfig = Options.Create(new JwtConfiguration
        {
            Key = "ThisIsAVerySecretKeyForTestingPurposesOnly1234567890",
            Issuer = "TestIssuer",
            Audience = "TestAudience",
            AccessTokenExpirationMinutes = 120,
            RefreshTokenExpirationDays = 30
        });
        _jwtUtil = new JwtUtil(jwtConfig);

        _authService = new AuthService(
            _userRepositoryMock.Object,
            _userLoginMethodRepositoryMock.Object,
            _refreshTokenRepositoryMock.Object,
            _userRefreshTokenRepositoryMock.Object,
            _loginMethodRepositoryMock.Object,
            _jwtUtil,
            _unitOfWorkMock.Object
        );
    }

    [Fact]
    public async Task LoginAsync_WithValidCredentials_ShouldReturnLoginResponse()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var userLoginMethodId = Guid.NewGuid();
        string loginMethodCode = "email_password";

        var user = new User
        {
            Id = userId,
            Name = "Test User",
            Email = "test@example.com",
            IsEmailVerified = true
        };

        var userLoginMethod = new UserLoginMethod
        {
            Id = userLoginMethodId,
            UserId = userId,
            LoginMethodCode = loginMethodCode
        };

        var loginMethod = new LoginMethod
        {
            Code = loginMethodCode,
            Name = "Email Password",
            IsEnabled = true
        };

        _userLoginMethodRepositoryMock
            .Setup(x => x.GetByIdAsync(userLoginMethodId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(userLoginMethod);

        _userRepositoryMock
            .Setup(x => x.GetByIdAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        _loginMethodRepositoryMock
            .Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<LoginMethod, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(loginMethod);

        _refreshTokenRepositoryMock
            .Setup(x => x.AddAsync(It.IsAny<RefreshToken>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((RefreshToken token, CancellationToken _) => token);

        _userRefreshTokenRepositoryMock
            .Setup(x => x.AddAsync(It.IsAny<UserRefreshToken>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((UserRefreshToken userToken, CancellationToken _) => userToken);

        _unitOfWorkMock
            .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        LoginResponse result = await _authService.LoginAsync(userLoginMethodId);

        // Assert
        result.Should().NotBeNull();
        result.AccessToken.Should().NotBeNullOrEmpty();
        result.RefreshToken.Should().NotBeNullOrEmpty();
        result.AccessTokenExpiresAt.Should().BeAfter(DateTimeOffset.UtcNow);
        result.RefreshTokenExpiresAt.Should().BeAfter(DateTimeOffset.UtcNow);

        _refreshTokenRepositoryMock.Verify(x => x.AddAsync(It.IsAny<RefreshToken>(), It.IsAny<CancellationToken>()), Times.Once);
        _userRefreshTokenRepositoryMock.Verify(x => x.AddAsync(It.IsAny<UserRefreshToken>(), It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task LoginAsync_WithInvalidUserLoginMethodId_ShouldThrowUnauthorizedException()
    {
        // Arrange
        var userLoginMethodId = Guid.NewGuid();

        _userLoginMethodRepositoryMock
            .Setup(x => x.GetByIdAsync(userLoginMethodId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((UserLoginMethod?)null);

        // Act
        Func<Task> act = async () => await _authService.LoginAsync(userLoginMethodId);

        // Assert
        await act.Should().ThrowAsync<UnauthorizedException>()
            .WithMessage("*UserLoginMethod*");
    }

    [Fact]
    public async Task LoginAsync_WithNonExistentUser_ShouldThrowUnauthorizedException()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var userLoginMethodId = Guid.NewGuid();

        var userLoginMethod = new UserLoginMethod
        {
            Id = userLoginMethodId,
            UserId = userId,
            LoginMethodCode = "email_password"
        };

        _userLoginMethodRepositoryMock
            .Setup(x => x.GetByIdAsync(userLoginMethodId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(userLoginMethod);

        _userRepositoryMock
            .Setup(x => x.GetByIdAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((User?)null);

        // Act
        Func<Task> act = async () => await _authService.LoginAsync(userLoginMethodId);

        // Assert
        await act.Should().ThrowAsync<UnauthorizedException>()
            .WithMessage("*User*");
    }

    [Fact]
    public async Task LoginAsync_WithDisabledLoginMethod_ShouldThrowForbiddenException()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var userLoginMethodId = Guid.NewGuid();
        string loginMethodCode = "email_password";

        var user = new User
        {
            Id = userId,
            Name = "Test User",
            Email = "test@example.com"
        };

        var userLoginMethod = new UserLoginMethod
        {
            Id = userLoginMethodId,
            UserId = userId,
            LoginMethodCode = loginMethodCode
        };

        var loginMethod = new LoginMethod
        {
            Code = loginMethodCode,
            Name = "Email Password",
            IsEnabled = false
        };

        _userLoginMethodRepositoryMock
            .Setup(x => x.GetByIdAsync(userLoginMethodId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(userLoginMethod);

        _userRepositoryMock
            .Setup(x => x.GetByIdAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        _loginMethodRepositoryMock
            .Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<LoginMethod, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(loginMethod);

        // Act
        Func<Task> act = async () => await _authService.LoginAsync(userLoginMethodId);

        // Assert
        await act.Should().ThrowAsync<ForbiddenException>()
            .WithMessage("*disabled*");
    }

    [Fact]
    public async Task GetUserLoginMethodByUserIdAsync_WithValidUserId_ShouldReturnUserLoginMethodDto()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var userLoginMethod = new UserLoginMethod
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            LoginMethodCode = "email_password",
            LastLoginAt = DateTimeOffset.UtcNow
        };

        _userLoginMethodRepositoryMock
            .Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<UserLoginMethod, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(userLoginMethod);

        // Act
        UserLoginMethodDto result = await _authService.GetUserLoginMethodByUserIdAsync(userId);

        // Assert
        result.Should().NotBeNull();
        result.UserId.Should().Be(userId);
        result.LoginMethodCode.Should().Be("email_password");
    }

    [Fact]
    public async Task GetUserLoginMethodByUserIdAsync_WithInvalidUserId_ShouldThrowNotFoundException()
    {
        // Arrange
        var userId = Guid.NewGuid();

        _userLoginMethodRepositoryMock
            .Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<UserLoginMethod, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((UserLoginMethod?)null);

        // Act
        Func<Task> act = async () => await _authService.GetUserLoginMethodByUserIdAsync(userId);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage("*UserLoginMethod*");
    }
}
