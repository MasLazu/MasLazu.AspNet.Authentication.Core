using FluentValidation;
using MasLazu.AspNet.Authentication.Core.Abstraction.Interfaces;
using MasLazu.AspNet.Authentication.Core.Abstraction.Models;
using MasLazu.AspNet.Authentication.Core.Domain.Entities;
using MasLazu.AspNet.Framework.Application.Interfaces;
using MasLazu.AspNet.Framework.Application.Services;

namespace MasLazu.AspNet.Authentication.Core.Base.Services;

public class TimezoneService : CrudService<Timezone, TimezoneDto, CreateTimezoneRequest, UpdateTimezoneRequest>, ITimezoneService
{
    public TimezoneService(
        IRepository<Timezone> repository,
        IReadRepository<Timezone> readRepository,
        IUnitOfWork unitOfWork,
        IEntityPropertyMap<Timezone> propertyMap,
        IPaginationValidator<Timezone> paginationValidator,
        ICursorPaginationValidator<Timezone> cursorPaginationValidator,
        IValidator<CreateTimezoneRequest>? createValidator = null,
        IValidator<UpdateTimezoneRequest>? updateValidator = null)
        : base(repository, readRepository, unitOfWork, propertyMap, paginationValidator, cursorPaginationValidator, createValidator, updateValidator)
    {
    }
}
