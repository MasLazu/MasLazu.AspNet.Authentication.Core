using Microsoft.Extensions.DependencyInjection;
using MasLazu.AspNet.Authentication.Core.Base.BackgroundServices;

namespace MasLazu.AspNet.Authentication.Core.Base.Extensions;

public static class AuthenticationCoreApplicationBackgroundServiceExtension
{
    public static IServiceCollection AddAuthenticationCoreDatabaseSeedBackgroundService(this IServiceCollection services)
    {
        services.AddHostedService<AuthenticationCoreDatabaseSeedBackgroundService>();
        return services;
    }
}
