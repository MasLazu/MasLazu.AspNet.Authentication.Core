using MasLazu.AspNet.Framework.Application.Interfaces;
using MasLazu.AspNet.Authentication.Core.Abstraction.Models;

namespace MasLazu.AspNet.Authentication.Core.Abstraction.Interfaces;

public interface IUserLoginMethodService : ICrudService<UserLoginMethodDto, CreateUserLoginMethodRequest, UpdateUserLoginMethodRequest>
{
    Task<IEnumerable<UserLoginMethodDto>> GetByUserIdAsync(Guid userId, Guid targetUserId, CancellationToken ct = default);
}
