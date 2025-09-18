using MasLazu.AspNet.Framework.Application.Models;

namespace MasLazu.AspNet.Authentication.Core.Abstraction.Models;

public record UpdateGenderRequest(
    Guid Id,
    string? Code,
    string? Name
) : BaseUpdateRequest(Id);
