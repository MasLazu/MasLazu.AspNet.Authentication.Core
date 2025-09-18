namespace MasLazu.AspNet.Authentication.Core.Abstraction.Models;

public record CreateUserLoginMethodRequest(
    Guid UserId,
    string LoginMethodCode
);
