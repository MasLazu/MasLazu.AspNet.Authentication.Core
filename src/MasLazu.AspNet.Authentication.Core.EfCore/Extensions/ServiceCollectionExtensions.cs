using Microsoft.Extensions.DependencyInjection;

namespace MasLazu.AspNet.Authentication.Core.EfCore.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuthenticationCoreEntityFrameworkCore(this IServiceCollection services)
    {
        return services;
    }
}
