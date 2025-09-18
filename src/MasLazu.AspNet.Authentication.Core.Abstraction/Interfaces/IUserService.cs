using MasLazu.AspNet.Framework.Application.Interfaces;
using MasLazu.AspNet.Authentication.Core.Abstraction.Models;

namespace MasLazu.AspNet.Authentication.Core.Abstraction.Interfaces;

public interface IUserService : ICrudService<UserDto, CreateUserRequest, UpdateUserRequest>
{
    Task<UserDto?> GetByUsernameOrEmailAsync(string usernameOrEmail, CancellationToken ct);
    Task<bool> IsEmailTakenAsync(string email, CancellationToken ct);
    Task<bool> IsUsernameTakenAsync(string username, CancellationToken ct);
    Task VerifyEmailAsync(Guid userId, CancellationToken ct = default);
    Task SendEmailVerificationAsync(string email, CancellationToken ct = default);
}
