using MasLazu.AspNet.Authentication.Core.Abstraction.Models;

namespace MasLazu.AspNet.Authentication.Core.Abstraction.Interfaces;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(Guid userLoginMethodId, CancellationToken ct = default);
    Task<UserLoginMethodDto> GetUserLoginMethodByUserIdAsync(Guid userId, CancellationToken ct = default);
}
