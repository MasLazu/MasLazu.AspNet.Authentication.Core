using MasLazu.AspNet.Framework.Application.Models;

namespace MasLazu.AspNet.Authentication.Core.Abstraction.Models;

public record UpdateUserLoginMethodRequest(
    Guid Id,
    Guid? UserId,
    string? LoginMethodCode
) : BaseUpdateRequest(Id);
