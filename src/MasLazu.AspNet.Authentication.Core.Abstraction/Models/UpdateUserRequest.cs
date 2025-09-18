using MasLazu.AspNet.Framework.Application.Models;

namespace MasLazu.AspNet.Authentication.Core.Abstraction.Models;

public record UpdateUserRequest(
    Guid Id,
    string? Name,
    string? Email,
    string? PhoneNumber,
    string? Username,
    string? LanguageCode,
    Guid? TimezoneId,
    string? GenderCode
) : BaseUpdateRequest(Id);
