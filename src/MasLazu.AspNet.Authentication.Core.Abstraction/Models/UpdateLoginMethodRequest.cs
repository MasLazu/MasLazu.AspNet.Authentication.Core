using MasLazu.AspNet.Framework.Application.Models;

namespace MasLazu.AspNet.Authentication.Core.Abstraction.Models;

public record UpdateLoginMethodRequest(
    Guid Id,
    string? Code
) : BaseUpdateRequest(Id);
