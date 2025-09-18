using Microsoft.EntityFrameworkCore;
using MasLazu.AspNet.Framework.EfCore.Data;
using MasLazu.AspNet.Authentication.Core.Domain.Entities;
using System.Reflection;

namespace MasLazu.AspNet.Authentication.Core.EfCore.Data;

public class AuthenticationCoreReadDbContext : BaseReadDbContext
{
    public AuthenticationCoreReadDbContext(DbContextOptions<AuthenticationCoreReadDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<LoginMethod> LoginMethods { get; set; }
    public DbSet<UserLoginMethod> UserLoginMethods { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<Gender> Genders { get; set; }
    public DbSet<Timezone> Timezones { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
