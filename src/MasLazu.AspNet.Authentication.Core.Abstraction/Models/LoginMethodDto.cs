using MasLazu.AspNet.Framework.Application.Models;

namespace MasLazu.AspNet.Authentication.Core.Abstraction.Models;

public record LoginMethodDto(
    Guid Id,
    string Code,
    DateTimeOffset CreatedAt,
    DateTimeOffset? UpdatedAt
) : BaseDto(Id, CreatedAt, UpdatedAt);