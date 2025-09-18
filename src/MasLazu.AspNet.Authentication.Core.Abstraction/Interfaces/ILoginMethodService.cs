using MasLazu.AspNet.Framework.Application.Interfaces;
using MasLazu.AspNet.Authentication.Core.Abstraction.Models;

namespace MasLazu.AspNet.Authentication.Core.Abstraction.Interfaces;

public interface ILoginMethodService : ICrudService<LoginMethodDto, CreateLoginMethodRequest, UpdateLoginMethodRequest>
{
    Task<LoginMethodDto> CreateIfNotExistsAsync(Guid id, CreateLoginMethodRequest createRequest, CancellationToken ct = default);
    Task<IEnumerable<LoginMethodDto>> GetAllEnabledAsync(Guid userId, CancellationToken ct = default);
}
