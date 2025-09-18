using MasLazu.AspNet.Framework.Application.Models;

namespace MasLazu.AspNet.Authentication.Core.Abstraction.Models;

public record UserDto(
    Guid Id,
    string Name,
    string? Email,
    string? PhoneNumber,
    string? Username,
    string? LanguageCode,
    Guid? TimezoneId,
    string? ProfilePicture,
    string? GenderCode,
    TimezoneDto? Timezone,
    GenderDto? Gender,
    LanguageDto? Language,
    ICollection<UserLoginMethodDto> UserLoginMethods,
    DateTimeOffset CreatedAt,
    DateTimeOffset? UpdatedAt
) : BaseDto(Id, CreatedAt, UpdatedAt);
