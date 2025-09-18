using MasLazu.AspNet.Framework.Application.Models;

namespace MasLazu.AspNet.Authentication.Core.Abstraction.Models;

public record UpdateTimezoneRequest(
    Guid Id,
    string? Identifier,
    string? Name,
    int? OffsetMinutes
) : BaseUpdateRequest(Id);
