using Microsoft.Extensions.DependencyInjection;
using MasLazu.AspNet.Authentication.Core.Base.Utils;
using MasLazu.AspNet.Authentication.Core.Domain.Entities;
using MasLazu.AspNet.Framework.Application.Interfaces;
using MasLazu.AspNet.Framework.Application.Utils;

namespace MasLazu.AspNet.Authentication.Core.Base.Extensions;

public static class AuthenticationCoreApplicationUtilExtension
{
    public static IServiceCollection AddAuthenticationCoreApplicationUtils(this IServiceCollection services)
    {
        RegisterPropertyMapsAndExpressionBuilders(services);
        RegisterJwtUtil(services);

        return services;
    }

    private static void RegisterPropertyMapsAndExpressionBuilders(IServiceCollection services)
    {
        var entityPropertyMapPairs = new (Type entityType, Type propertyMapType)[]
        {
            (typeof(User), typeof(UserEntityPropertyMap)),
            (typeof(RefreshToken), typeof(RefreshTokenEntityPropertyMap)),
            (typeof(UserRefreshToken), typeof(UserRefreshTokenEntityPropertyMap)),
            (typeof(LoginMethod), typeof(LoginMethodEntityPropertyMap)),
            (typeof(UserLoginMethod), typeof(UserLoginMethodEntityPropertyMap)),
            (typeof(Language), typeof(LanguageEntityPropertyMap)),
            (typeof(Gender), typeof(GenderEntityPropertyMap)),
            (typeof(Timezone), typeof(TimezoneEntityPropertyMap))
        };

        foreach ((Type entityType, Type propertyMapType) in entityPropertyMapPairs)
        {
            Type propertyMapInterfaceType = typeof(IEntityPropertyMap<>).MakeGenericType(entityType);
            services.AddSingleton(propertyMapInterfaceType, propertyMapType);

            Type expressionBuilderType = typeof(ExpressionBuilder<>).MakeGenericType(entityType);
            services.AddScoped(expressionBuilderType);
        }
    }

    private static void RegisterJwtUtil(IServiceCollection services)
    {
        services.AddSingleton<JwtUtil>();
    }
}
