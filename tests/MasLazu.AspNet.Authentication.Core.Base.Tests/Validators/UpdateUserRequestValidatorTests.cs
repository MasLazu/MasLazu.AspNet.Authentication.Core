using FluentValidation.Results;
using MasLazu.AspNet.Authentication.Core.Abstraction.Models;
using MasLazu.AspNet.Authentication.Core.Base.Validators;

namespace MasLazu.AspNet.Authentication.Core.Base.Tests.Validators;

public class UpdateUserRequestValidatorTests
{
    private readonly UpdateUserRequestValidator _validator;

    public UpdateUserRequestValidatorTests()
    {
        _validator = new UpdateUserRequestValidator();
    }

    [Fact]
    public void Validate_WithValidRequest_ShouldPass()
    {
        // Arrange
        var request = new UpdateUserRequest(
            Id: Guid.NewGuid(),
            Name: "Jane Doe",
            Email: "jane.doe@example.com",
            PhoneNumber: "+9876543210",
            Username: "janedoe",
            LanguageCode: "es",
            TimezoneId: Guid.NewGuid(),
            GenderCode: "female"
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
        var request = new UpdateUserRequest(
            Id: Guid.NewGuid(),
            Name: "",
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
        var request = new UpdateUserRequest(
            Id: Guid.NewGuid(),
            Name: "Jane Doe",
            Email: "not-an-email",
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

    [Theory]
    [InlineData("+12345678901")]
    [InlineData("+447123456789")]
    [InlineData("19876543210")]
    public void Validate_WithValidPhoneNumber_ShouldPass(string phoneNumber)
    {
        // Arrange
        var request = new UpdateUserRequest(
            Id: Guid.NewGuid(),
            Name: "Jane Doe",
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
    public void Validate_WithNullOptionalFields_ShouldPass()
    {
        // Arrange
        var request = new UpdateUserRequest(
            Id: Guid.NewGuid(),
            Name: "Jane Doe",
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
