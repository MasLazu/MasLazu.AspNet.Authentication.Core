using FastEndpoints;
using MasLazu.AspNet.Framework.Endpoint.Endpoints;
using MasLazu.AspNet.Authentication.Core.Abstraction.Interfaces;
using MasLazu.AspNet.Authentication.Core.Endpoint.EndpointGroups;

namespace MasLazu.AspNet.Authentication.Core.Endpoint.Endpoints.Users;

public record SendEmailVerificationRequest(string Email);

public class SendEmailVerificationEndpoint : BaseEndpointWithoutResponse<SendEmailVerificationRequest>
{
    public IUserService UserService { get; set; }

    public override void ConfigureEndpoint()
    {
        Post("/send-email-verification");
        Group<UsersEndpointGroup>();
        AllowAnonymous();
    }

    public override async Task HandleAsync(SendEmailVerificationRequest req, CancellationToken ct)
    {
        await UserService.SendEmailVerificationAsync(req.Email, ct);
        await SendOkResponseAsync("Email verification sent successfully", ct);
    }
}
