using System.Linq.Expressions;
using MasLazu.AspNet.Authentication.Core.Abstraction.Interfaces;
using MasLazu.AspNet.Authentication.Core.Abstraction.Models;
using MasLazu.AspNet.Authentication.Core.Base.Services;
using MasLazu.AspNet.Authentication.Core.Domain.Entities;
using MasLazu.AspNet.Framework.Application.Exceptions;
using MasLazu.AspNet.Framework.Application.Interfaces;
using MasLazu.AspNet.Verification.Abstraction.Interfaces;
using MasLazu.AspNet.Verification.Abstraction.Models;
using FluentValidation;

namespace MasLazu.AspNet.Authentication.Core.Base.Tests.Services;

public class UserServiceTests
{
    private readonly Mock<IRepository<User>> _repositoryMock;
    private readonly Mock<IReadRepository<User>> _readRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IEntityPropertyMap<User>> _propertyMapMock;
    private readonly Mock<IPaginationValidator<User>> _paginationValidatorMock;
    private readonly Mock<ICursorPaginationValidator<User>> _cursorPaginationValidatorMock;
    private readonly Mock<IVerificationService> _verificationServiceMock;
    private readonly Mock<IValidator<CreateUserRequest>> _createValidatorMock;
    private readonly Mock<IValidator<UpdateUserRequest>> _updateValidatorMock;
    private readonly IUserService _userService;

    public UserServiceTests()
    {
        _repositoryMock = new Mock<IRepository<User>>();
        _readRepositoryMock = new Mock<IReadRepository<User>>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _propertyMapMock = new Mock<IEntityPropertyMap<User>>();
        _paginationValidatorMock = new Mock<IPaginationValidator<User>>();
        _cursorPaginationValidatorMock = new Mock<ICursorPaginationValidator<User>>();
        _verificationServiceMock = new Mock<IVerificationService>();
        _createValidatorMock = new Mock<IValidator<CreateUserRequest>>();
        _updateValidatorMock = new Mock<IValidator<UpdateUserRequest>>();

        _userService = new UserService(
            _repositoryMock.Object,
            _readRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _propertyMapMock.Object,
            _paginationValidatorMock.Object,
            _cursorPaginationValidatorMock.Object,
            _verificationServiceMock.Object,
            _createValidatorMock.Object,
            _updateValidatorMock.Object
        );
    }

    [Fact]
    public async Task GetByUsernameOrEmailAsync_WithValidEmail_ShouldReturnUser()
    {
        // Arrange
        string email = "test@example.com";
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = "Test User",
            Email = email,
            Username = "testuser"
        };

        _readRepositoryMock
            .Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        // Act
        UserDto? result = await _userService.GetByUsernameOrEmailAsync(email, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result!.Email.Should().Be(email);
        result.Name.Should().Be("Test User");
    }

    [Fact]
    public async Task GetByUsernameOrEmailAsync_WithValidUsername_ShouldReturnUser()
    {
        // Arrange
        string username = "testuser";
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = "Test User",
            Email = "test@example.com",
            Username = username
        };

        _readRepositoryMock
            .Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        // Act
        UserDto? result = await _userService.GetByUsernameOrEmailAsync(username, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result!.Username.Should().Be(username);
    }

    [Fact]
    public async Task GetByUsernameOrEmailAsync_WithNonExistentUser_ShouldReturnNull()
    {
        // Arrange
        string usernameOrEmail = "nonexistent@example.com";

        _readRepositoryMock
            .Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((User?)null);

        // Act
        UserDto? result = await _userService.GetByUsernameOrEmailAsync(usernameOrEmail, CancellationToken.None);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task IsEmailTakenAsync_WithTakenEmail_ShouldReturnTrue()
    {
        // Arrange
        string email = "taken@example.com";

        _readRepositoryMock
            .Setup(x => x.ExistsAsync(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        bool result = await _userService.IsEmailTakenAsync(email, CancellationToken.None);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task IsEmailTakenAsync_WithAvailableEmail_ShouldReturnFalse()
    {
        // Arrange
        string email = "available@example.com";

        _readRepositoryMock
            .Setup(x => x.ExistsAsync(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        bool result = await _userService.IsEmailTakenAsync(email, CancellationToken.None);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task IsUsernameTakenAsync_WithTakenUsername_ShouldReturnTrue()
    {
        // Arrange
        string username = "takenuser";

        _readRepositoryMock
            .Setup(x => x.ExistsAsync(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        bool result = await _userService.IsUsernameTakenAsync(username, CancellationToken.None);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task IsUsernameTakenAsync_WithAvailableUsername_ShouldReturnFalse()
    {
        // Arrange
        string username = "availableuser";

        _readRepositoryMock
            .Setup(x => x.ExistsAsync(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        bool result = await _userService.IsUsernameTakenAsync(username, CancellationToken.None);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task VerifyEmailAsync_WithValidUserId_ShouldUpdateUserAndSave()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new User
        {
            Id = userId,
            Name = "Test User",
            Email = "test@example.com",
            IsEmailVerified = false
        };

        _repositoryMock
            .Setup(x => x.GetByIdAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        _repositoryMock
            .Setup(x => x.UpdateAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _unitOfWorkMock
            .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        await _userService.VerifyEmailAsync(userId);

        // Assert
        user.IsEmailVerified.Should().BeTrue();
        _repositoryMock.Verify(x => x.UpdateAsync(It.Is<User>(u => u.IsEmailVerified == true), It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task VerifyEmailAsync_WithInvalidUserId_ShouldThrowNotFoundException()
    {
        // Arrange
        var userId = Guid.NewGuid();

        _repositoryMock
            .Setup(x => x.GetByIdAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((User?)null);

        // Act
        Func<Task> act = async () => await _userService.VerifyEmailAsync(userId);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage("*User*not found*");
    }

    [Fact]
    public async Task SendEmailVerificationAsync_WithValidEmail_ShouldSendVerification()
    {
        // Arrange
        string email = "test@example.com";
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = "Test User",
            Email = email,
            IsEmailVerified = false
        };

        _repositoryMock
            .Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        _verificationServiceMock
            .Setup(x => x.SendVerificationAsync(It.IsAny<Guid>(), It.IsAny<SendVerificationRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(It.IsAny<MasLazu.AspNet.Verification.Abstraction.Models.VerificationDto>());

        // Act
        await _userService.SendEmailVerificationAsync(email);

        // Assert
        _verificationServiceMock.Verify(
            x => x.SendVerificationAsync(
                user.Id,
                It.Is<SendVerificationRequest>(r => r.Destination == email && r.PurposeCode == "email_verification"),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task SendEmailVerificationAsync_WithNonExistentEmail_ShouldThrowNotFoundException()
    {
        // Arrange
        string email = "nonexistent@example.com";

        _repositoryMock
            .Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((User?)null);

        // Act
        Func<Task> act = async () => await _userService.SendEmailVerificationAsync(email);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage("*User*not found*");
    }

    [Fact]
    public async Task SendEmailVerificationAsync_WithUserWithoutEmail_ShouldThrowBadRequestException()
    {
        // Arrange
        string email = "test@example.com";
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = "Test User",
            Email = null
        };

        _repositoryMock
            .Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        // Act
        Func<Task> act = async () => await _userService.SendEmailVerificationAsync(email);

        // Assert
        await act.Should().ThrowAsync<BadRequestException>()
            .WithMessage("*email*verify*");
    }
}
