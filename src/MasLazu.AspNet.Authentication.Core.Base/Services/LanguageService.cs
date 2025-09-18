using FluentValidation;
using MasLazu.AspNet.Authentication.Core.Abstraction.Interfaces;
using MasLazu.AspNet.Authentication.Core.Abstraction.Models;
using MasLazu.AspNet.Authentication.Core.Domain.Entities;
using MasLazu.AspNet.Framework.Application.Interfaces;
using MasLazu.AspNet.Framework.Application.Services;

namespace MasLazu.AspNet.Authentication.Core.Base.Services;

public class LanguageService : CrudService<Language, LanguageDto, CreateLanguageRequest, UpdateLanguageRequest>, ILanguageService
{
    public LanguageService(
        IRepository<Language> repository,
        IReadRepository<Language> readRepository,
        IUnitOfWork unitOfWork,
        IEntityPropertyMap<Language> propertyMap,
        IPaginationValidator<Language> paginationValidator,
        ICursorPaginationValidator<Language> cursorPaginationValidator,
        IValidator<CreateLanguageRequest>? createValidator = null,
        IValidator<UpdateLanguageRequest>? updateValidator = null)
        : base(repository, readRepository, unitOfWork, propertyMap, paginationValidator, cursorPaginationValidator, createValidator, updateValidator)
    {
    }
}
