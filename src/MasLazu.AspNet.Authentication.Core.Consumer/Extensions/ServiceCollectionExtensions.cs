using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using MasLazu.AspNet.Authentication.Core.Consumer.Consumers;

namespace MasLazu.AspNet.Authentication.Core.Consumer.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuthenticationCoreConsumers(this IServiceCollection services)
    {
        services.AddScoped<VerificationCompletedConsumer>();

        return services;
    }

    public static IBusRegistrationConfigurator AddAuthenticationCoreConsumers(this IBusRegistrationConfigurator configurator)
    {
        configurator.AddConsumer<VerificationCompletedConsumer>();

        return configurator;
    }
}
