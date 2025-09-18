using FluentValidation;
using Mapster;
using MasLazu.AspNet.Authentication.Core.Abstraction.Interfaces;
using MasLazu.AspNet.Authentication.Core.Abstraction.Models;
using MasLazu.AspNet.Authentication.Core.Domain.Entities;
using MasLazu.AspNet.Framework.Application.Interfaces;
using MasLazu.AspNet.Framework.Application.Services;

namespace MasLazu.AspNet.Authentication.Core.Base.Services;

public class LoginMethodService : CrudService<LoginMethod, LoginMethodDto, CreateLoginMethodRequest, UpdateLoginMethodRequest>, ILoginMethodService
{
    public LoginMethodService(
        IRepository<LoginMethod> repository,
        IReadRepository<LoginMethod> readRepository,
        IUnitOfWork unitOfWork,
        IEntityPropertyMap<LoginMethod> propertyMap,
        IPaginationValidator<LoginMethod> paginationValidator,
        ICursorPaginationValidator<LoginMethod> cursorPaginationValidator,
        IValidator<CreateLoginMethodRequest>? createValidator = null,
        IValidator<UpdateLoginMethodRequest>? updateValidator = null)
        : base(repository, readRepository, unitOfWork, propertyMap, paginationValidator, cursorPaginationValidator, createValidator, updateValidator)
    {
    }

    public async Task<LoginMethodDto> CreateIfNotExistsAsync(Guid id, CreateLoginMethodRequest createRequest, CancellationToken ct = default)
    {
        await ValidateAsync(createRequest, CreateValidator, ct);

        LoginMethod? existingEntity = await ReadRepository.GetByIdAsync(id, ct);
        if (existingEntity != null)
        {
            return existingEntity.Adapt<LoginMethodDto>();
        }

        LoginMethod entity = createRequest.Adapt<LoginMethod>();
        entity.Id = id;
        LoginMethod createdEntity = await Repository.AddAsync(entity, ct);
        await UnitOfWork.SaveChangesAsync(ct);

        return createdEntity.Adapt<LoginMethodDto>();
    }

    public async Task<IEnumerable<LoginMethodDto>> GetAllEnabledAsync(Guid userId, CancellationToken ct = default)
    {
        IEnumerable<LoginMethod> entities = await ReadRepository.FindAsync(lm => lm.IsEnabled, ct);
        return entities.Adapt<IEnumerable<LoginMethodDto>>();
    }
}
