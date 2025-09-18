using FluentValidation;
using MasLazu.AspNet.Authentication.Core.Abstraction.Interfaces;
using MasLazu.AspNet.Authentication.Core.Abstraction.Models;
using MasLazu.AspNet.Authentication.Core.Domain.Entities;
using MasLazu.AspNet.Framework.Application.Interfaces;
using MasLazu.AspNet.Framework.Application.Services;

namespace MasLazu.AspNet.Authentication.Core.Base.Services;

public class GenderService : CrudService<Gender, GenderDto, CreateGenderRequest, UpdateGenderRequest>, IGenderService
{
    public GenderService(
        IRepository<Gender> repository,
        IReadRepository<Gender> readRepository,
        IUnitOfWork unitOfWork,
        IEntityPropertyMap<Gender> propertyMap,
        IPaginationValidator<Gender> paginationValidator,
        ICursorPaginationValidator<Gender> cursorPaginationValidator,
        IValidator<CreateGenderRequest>? createValidator = null,
        IValidator<UpdateGenderRequest>? updateValidator = null)
        : base(repository, readRepository, unitOfWork, propertyMap, paginationValidator, cursorPaginationValidator, createValidator, updateValidator)
    {
    }
}
