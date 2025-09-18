using FastEndpoints;
using Microsoft.Extensions.DependencyInjection;

namespace MasLazu.AspNet.Authentication.Core.Endpoint.Extensions;

public static class AuthenticationCoreEndpointExtenstion
{
    public static IServiceCollection AddAuthenticationCoreEndpoints(this IServiceCollection services)
    {
        return services;
    }
}
