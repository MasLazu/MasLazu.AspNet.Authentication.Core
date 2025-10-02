using MasLazu.AspNet.Authentication.Core.Domain.Entities;
using MasLazu.AspNet.Authentication.Core.EfCore.Data;
using MasLazu.AspNet.Framework.Application.Interfaces;
using MasLazu.AspNet.Framework.EfCore.Data;
using MasLazu.AspNet.Framework.EfCore.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace MasLazu.AspNet.Authentication.Core.EfCore.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuthenticationCoreEntityFrameworkCore(this IServiceCollection services)
    {
        services.AddScoped<IRepository<User>, Repository<User, AuthenticationCoreDbContext>>();
        services.AddScoped<IRepository<RefreshToken>, Repository<RefreshToken, AuthenticationCoreDbContext>>();
        services.AddScoped<IRepository<UserRefreshToken>, Repository<UserRefreshToken, AuthenticationCoreDbContext>>();
        services.AddScoped<IRepository<LoginMethod>, Repository<LoginMethod, AuthenticationCoreDbContext>>();
        services.AddScoped<IRepository<UserLoginMethod>, Repository<UserLoginMethod, AuthenticationCoreDbContext>>();
        services.AddScoped<IRepository<Language>, Repository<Language, AuthenticationCoreDbContext>>();
        services.AddScoped<IRepository<Gender>, Repository<Gender, AuthenticationCoreDbContext>>();
        services.AddScoped<IRepository<Timezone>, Repository<Timezone, AuthenticationCoreDbContext>>();

        services.AddScoped<IReadRepository<User>, ReadRepository<User, AuthenticationCoreReadDbContext>>();
        services.AddScoped<IReadRepository<RefreshToken>, ReadRepository<RefreshToken, AuthenticationCoreReadDbContext>>();
        services.AddScoped<IReadRepository<UserRefreshToken>, ReadRepository<UserRefreshToken, AuthenticationCoreReadDbContext>>();
        services.AddScoped<IReadRepository<LoginMethod>, ReadRepository<LoginMethod, AuthenticationCoreReadDbContext>>();
        services.AddScoped<IReadRepository<UserLoginMethod>, ReadRepository<UserLoginMethod, AuthenticationCoreReadDbContext>>();
        services.AddScoped<IReadRepository<Language>, ReadRepository<Language, AuthenticationCoreReadDbContext>>();
        services.AddScoped<IReadRepository<Gender>, ReadRepository<Gender, AuthenticationCoreReadDbContext>>();
        services.AddScoped<IReadRepository<Timezone>, ReadRepository<Timezone, AuthenticationCoreReadDbContext>>();

        services.AddScoped<BaseDbContext>(sp => sp.GetRequiredService<AuthenticationCoreDbContext>());

        return services;
    }
}
