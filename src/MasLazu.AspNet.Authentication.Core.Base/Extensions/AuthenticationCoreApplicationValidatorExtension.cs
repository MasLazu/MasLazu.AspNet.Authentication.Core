using Microsoft.Extensions.DependencyInjection;
using MasLazu.AspNet.Authentication.Core.Base.Validators;
using FluentValidation;
using MasLazu.AspNet.Authentication.Core.Abstraction.Models;

namespace MasLazu.AspNet.Authentication.Core.Base.Extensions;

public static class AuthenticationCoreApplicationValidatorExtension
{
    public static IServiceCollection AddAuthenticationCoreApplicationValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateUserRequest>, CreateUserRequestValidator>();
        services.AddScoped<IValidator<UpdateUserRequest>, UpdateUserRequestValidator>();
        services.AddScoped<IValidator<CreateGenderRequest>, CreateGenderRequestValidator>();
        services.AddScoped<IValidator<UpdateGenderRequest>, UpdateGenderRequestValidator>();
        services.AddScoped<IValidator<CreateLanguageRequest>, CreateLanguageRequestValidator>();
        services.AddScoped<IValidator<UpdateLanguageRequest>, UpdateLanguageRequestValidator>();
        services.AddScoped<IValidator<CreateLoginMethodRequest>, CreateLoginMethodRequestValidator>();
        services.AddScoped<IValidator<UpdateLoginMethodRequest>, UpdateLoginMethodRequestValidator>();
        services.AddScoped<IValidator<CreateTimezoneRequest>, CreateTimezoneRequestValidator>();
        services.AddScoped<IValidator<UpdateTimezoneRequest>, UpdateTimezoneRequestValidator>();

        return services;
    }
}
