using Microsoft.Extensions.DependencyInjection;
using MasLazu.AspNet.Authentication.Core.Base.Services;
using MasLazu.AspNet.Authentication.Core.Base.Utils;
using MasLazu.AspNet.Authentication.Core.Base.Validators;
using MasLazu.AspNet.Authentication.Core.Domain.Entities;
using MasLazu.AspNet.Framework.Application.Interfaces;
using FluentValidation;
using MasLazu.AspNet.Authentication.Core.Abstraction.Interfaces;

namespace MasLazu.AspNet.Authentication.Core.Base.Extensions;

public static class AuthenticationCoreApplicationServiceExtension
{
    public static IServiceCollection AddAuthenticationCoreApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IGenderService, GenderService>();
        services.AddScoped<ILanguageService, LanguageService>();
        services.AddScoped<ILoginMethodService, LoginMethodService>();
        services.AddScoped<ITimezoneService, TimezoneService>();
        services.AddScoped<IUserLoginMethodService, UserLoginMethodService>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
