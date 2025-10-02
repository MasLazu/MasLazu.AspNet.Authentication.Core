using FluentValidation;
using MasLazu.AspNet.Authentication.Core.Abstraction.Interfaces;
using MasLazu.AspNet.Authentication.Core.Abstraction.Models;
using MasLazu.AspNet.Authentication.Core.Domain.Entities;
using MasLazu.AspNet.Framework.Application.Interfaces;
using MasLazu.AspNet.Framework.Application.Services;
using Mapster;

namespace MasLazu.AspNet.Authentication.Core.Base.Services;

public class UserLoginMethodService : CrudService<UserLoginMethod, UserLoginMethodDto, CreateUserLoginMethodRequest, UpdateUserLoginMethodRequest>, IUserLoginMethodService
{
    public UserLoginMethodService(
        IRepository<UserLoginMethod> repository,
        IReadRepository<UserLoginMethod> readRepository,
        IUnitOfWork unitOfWork,
        IEntityPropertyMap<UserLoginMethod> propertyMap,
        IPaginationValidator<UserLoginMethod> paginationValidator,
        ICursorPaginationValidator<UserLoginMethod> cursorPaginationValidator,
        IValidator<CreateUserLoginMethodRequest>? createValidator = null,
        IValidator<UpdateUserLoginMethodRequest>? updateValidator = null)
        : base(repository, readRepository, unitOfWork, propertyMap, paginationValidator, cursorPaginationValidator, createValidator, updateValidator)
    {
    }

    public async Task<IEnumerable<UserLoginMethodDto>> GetByUserIdAsync(Guid userId, CancellationToken ct = default)
    {
        IEnumerable<UserLoginMethod> entities = await Repository.FindAsync(e => e.UserId == userId, ct);
        return entities.Adapt<IEnumerable<UserLoginMethodDto>>();
    }
}
