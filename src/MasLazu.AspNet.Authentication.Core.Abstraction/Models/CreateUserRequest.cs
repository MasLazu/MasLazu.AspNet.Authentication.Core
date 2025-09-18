namespace MasLazu.AspNet.Authentication.Core.Abstraction.Models;

public record CreateUserRequest(
    string Name,
    string? Email,
    string? PhoneNumber,
    string? Username,
    string? LanguageCode,
    Guid? TimezoneId,
    string? GenderCode
);
