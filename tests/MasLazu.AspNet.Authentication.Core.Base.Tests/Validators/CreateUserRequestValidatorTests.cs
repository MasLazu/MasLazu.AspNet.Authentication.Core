using FluentValidation.Results;
using MasLazu.AspNet.Authentication.Core.Abstraction.Models;
using MasLazu.AspNet.Authentication.Core.Base.Validators;

namespace MasLazu.AspNet.Authentication.Core.Base.Tests.Validators;

public class CreateUserRequestValidatorTests
{
    private readonly CreateUserRequestValidator _validator;

    public CreateUserRequestValidatorTests()
    {
        _validator = new CreateUserRequestValidator();
    }

    [Fact]
    public void Validate_WithValidRequest_ShouldPass()
    {
        // Arrange
        var request = new CreateUserRequest(
            Name: "John Doe",
            Email: "john.doe@example.com",
            PhoneNumber: "+1234567890",
            Username: "johndoe",
            LanguageCode: "en",
            TimezoneId: Guid.NewGuid(),
            GenderCode: "male"
        );

        // Act
        ValidationResult result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_WithEmptyName_ShouldFail()
    {
        // Arrange
        var request = new CreateUserRequest(
            Name: "",
            Email: "john.doe@example.com",
            PhoneNumber: null,
            Username: null,
            LanguageCode: null,
            TimezoneId: null,
            GenderCode: null
        );

        // Act
        ValidationResult result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Name");
    }

    [Fact]
    public void Validate_WithNameTooLong_ShouldFail()
    {
        // Arrange
        string longName = new string('a', 101);
        var request = new CreateUserRequest(
            Name: longName,
            Email: null,
            PhoneNumber: null,
            Username: null,
            LanguageCode: null,
            TimezoneId: null,
            GenderCode: null
        );

        // Act
        ValidationResult result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Name");
    }

    [Fact]
    public void Validate_WithInvalidEmail_ShouldFail()
    {
        // Arrange
        var request = new CreateUserRequest(
            Name: "John Doe",
            Email: "invalid-email",
            PhoneNumber: null,
            Username: null,
            LanguageCode: null,
            TimezoneId: null,
            GenderCode: null
        );

        // Act
        ValidationResult result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Email");
    }

    [Fact]
    public void Validate_WithValidEmail_ShouldPass()
    {
        // Arrange
        var request = new CreateUserRequest(
            Name: "John Doe",
            Email: "valid@example.com",
            PhoneNumber: null,
            Username: null,
            LanguageCode: null,
            TimezoneId: null,
            GenderCode: null
        );

        // Act
        ValidationResult result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_WithNullEmail_ShouldPass()
    {
        // Arrange
        var request = new CreateUserRequest(
            Name: "John Doe",
            Email: null,
            PhoneNumber: null,
            Username: null,
            LanguageCode: null,
            TimezoneId: null,
            GenderCode: null
        );

        // Act
        ValidationResult result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_WithInvalidPhoneNumber_ShouldFail()
    {
        // Arrange
        var request = new CreateUserRequest(
            Name: "John Doe",
            Email: null,
            PhoneNumber: "123",
            Username: null,
            LanguageCode: null,
            TimezoneId: null,
            GenderCode: null
        );

        // Act
        ValidationResult result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "PhoneNumber");
    }

    [Theory]
    [InlineData("+12345678901")]
    [InlineData("+1234567890123")]
    [InlineData("12345678901")]
    [InlineData("+447123456789")]
    public void Validate_WithValidPhoneNumber_ShouldPass(string phoneNumber)
    {
        // Arrange
        var request = new CreateUserRequest(
            Name: "John Doe",
            Email: null,
            PhoneNumber: phoneNumber,
            Username: null,
            LanguageCode: null,
            TimezoneId: null,
            GenderCode: null
        );

        // Act
        ValidationResult result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_WithUsernameTooLong_ShouldFail()
    {
        // Arrange
        string longUsername = new string('a', 51);
        var request = new CreateUserRequest(
            Name: "John Doe",
            Email: null,
            PhoneNumber: null,
            Username: longUsername,
            LanguageCode: null,
            TimezoneId: null,
            GenderCode: null
        );

        // Act
        ValidationResult result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Username");
    }

    [Fact]
    public void Validate_WithLanguageCodeTooLong_ShouldFail()
    {
        // Arrange
        string longLanguageCode = new string('a', 11);
        var request = new CreateUserRequest(
            Name: "John Doe",
            Email: null,
            PhoneNumber: null,
            Username: null,
            LanguageCode: longLanguageCode,
            TimezoneId: null,
            GenderCode: null
        );

        // Act
        ValidationResult result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "LanguageCode");
    }

    [Fact]
    public void Validate_WithGenderCodeTooLong_ShouldFail()
    {
        // Arrange
        string longGenderCode = new string('a', 11);
        var request = new CreateUserRequest(
            Name: "John Doe",
            Email: null,
            PhoneNumber: null,
            Username: null,
            LanguageCode: null,
            TimezoneId: null,
            GenderCode: longGenderCode
        );

        // Act
        ValidationResult result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "GenderCode");
    }

    [Fact]
    public void Validate_WithAllOptionalFieldsNull_ShouldPass()
    {
        // Arrange
        var request = new CreateUserRequest(
            Name: "John Doe",
            Email: null,
            PhoneNumber: null,
            Username: null,
            LanguageCode: null,
            TimezoneId: null,
            GenderCode: null
        );

        // Act
        ValidationResult result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}
