using Microsoft.EntityFrameworkCore;
using MasLazu.AspNet.Authentication.Core.Domain.Entities;
using MasLazu.AspNet.Authentication.Core.EfCore.Data;

namespace MasLazu.AspNet.Authentication.Core.Integration.Tests.Fixtures;

public class DatabaseFixture : IDisposable
{
    public AuthenticationCoreDbContext DbContext { get; private set; }
    private readonly string _databaseName;

    public DatabaseFixture()
    {
        _databaseName = Guid.NewGuid().ToString();
        DbContextOptions<AuthenticationCoreDbContext> options = new DbContextOptionsBuilder<AuthenticationCoreDbContext>()
            .UseInMemoryDatabase(databaseName: _databaseName)
            .Options;

        DbContext = new AuthenticationCoreDbContext(options);
        SeedDatabase();
    }

    private void SeedDatabase()
    {
        // Seed reference data
        var genders = new List<Gender>
        {
            new Gender { Code = "male", Name = "Male", CreatedAt = DateTimeOffset.UtcNow },
            new Gender { Code = "female", Name = "Female", CreatedAt = DateTimeOffset.UtcNow },
            new Gender { Code = "other", Name = "Other", CreatedAt = DateTimeOffset.UtcNow }
        };

        var languages = new List<Language>
        {
            new Language { Code = "en", Name = "English", CreatedAt = DateTimeOffset.UtcNow },
            new Language { Code = "es", Name = "Spanish", CreatedAt = DateTimeOffset.UtcNow },
            new Language { Code = "fr", Name = "French", CreatedAt = DateTimeOffset.UtcNow }
        };

        var timezones = new List<Timezone>
        {
            new Timezone { Id = Guid.NewGuid(), Identifier = "UTC", Name = "UTC", OffsetMinutes = 0, CreatedAt = DateTimeOffset.UtcNow },
            new Timezone { Id = Guid.NewGuid(), Identifier = "America/New_York", Name = "Eastern Standard Time", OffsetMinutes = -300, CreatedAt = DateTimeOffset.UtcNow },
            new Timezone { Id = Guid.NewGuid(), Identifier = "America/Los_Angeles", Name = "Pacific Standard Time", OffsetMinutes = -480, CreatedAt = DateTimeOffset.UtcNow }
        };

        var loginMethods = new List<LoginMethod>
        {
            new LoginMethod { Code = "email_password", Name = "Email Password", IsEnabled = true, CreatedAt = DateTimeOffset.UtcNow },
            new LoginMethod { Code = "google", Name = "Google", IsEnabled = true, CreatedAt = DateTimeOffset.UtcNow },
            new LoginMethod { Code = "facebook", Name = "Facebook", IsEnabled = false, CreatedAt = DateTimeOffset.UtcNow }
        };

        DbContext.Genders.AddRange(genders);
        DbContext.Languages.AddRange(languages);
        DbContext.Timezones.AddRange(timezones);
        DbContext.LoginMethods.AddRange(loginMethods);
        DbContext.SaveChanges();
    }

    public void Dispose()
    {
        DbContext.Database.EnsureDeleted();
        DbContext.Dispose();
    }

    public AuthenticationCoreDbContext CreateNewContext()
    {
        DbContextOptions<AuthenticationCoreDbContext> options = new DbContextOptionsBuilder<AuthenticationCoreDbContext>()
            .UseInMemoryDatabase(databaseName: _databaseName)
            .Options;

        return new AuthenticationCoreDbContext(options);
    }
}
