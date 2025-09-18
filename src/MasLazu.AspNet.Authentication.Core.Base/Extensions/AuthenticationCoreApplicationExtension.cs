using Microsoft.Extensions.DependencyInjection;

namespace MasLazu.AspNet.Authentication.Core.Base.Extensions;

public static class AuthenticationCoreApplicationExtension
{
    public static IServiceCollection AddAuthenticationCoreApplication(this IServiceCollection services)
    {
        services.AddAuthenticationCoreApplicationServices();
        services.AddAuthenticationCoreApplicationUtils();
        services.AddAuthenticationCoreApplicationValidators();
        services.AddAuthenticationCoreDatabaseSeedBackgroundService();

        return services;
    }
}
