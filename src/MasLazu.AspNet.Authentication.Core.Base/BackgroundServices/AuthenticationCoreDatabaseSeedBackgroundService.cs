using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MasLazu.AspNet.Verification.Abstraction.Interfaces;
using MasLazu.AspNet.Verification.Abstraction.Models;

namespace MasLazu.AspNet.Authentication.Core.Base.BackgroundServices;

public class AuthenticationCoreDatabaseSeedBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public AuthenticationCoreDatabaseSeedBackgroundService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using IServiceScope scope = _serviceScopeFactory.CreateScope();
        IVerificationPurposeService verificationPurposeService = scope.ServiceProvider.GetRequiredService<IVerificationPurposeService>();

        var emailVerificationPurposeId = Guid.Parse("01995124-4693-7c14-a025-abaa93e07f8b");
        var createRequest = new CreateVerificationPurposeRequest("email_verification", "Email Verification", "Verification for email", true);
        await verificationPurposeService.CreateIfNotExistsAsync(emailVerificationPurposeId, createRequest, stoppingToken);
    }
}
