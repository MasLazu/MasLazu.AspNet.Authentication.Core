using FastEndpoints;
using MasLazu.AspNet.Framework.Endpoint.Endpoints;
using MasLazu.AspNet.Authentication.Core.Abstraction.Interfaces;
using MasLazu.AspNet.Authentication.Core.Abstraction.Models;
using MasLazu.AspNet.Authentication.Core.Endpoint.EndpointGroups;

namespace MasLazu.AspNet.Authentication.Core.Endpoint.Endpoints.Auth;

public class RefreshTokenEndpoint : BaseEndpoint<RefreshTokenRequest, LoginResponse>
{
    public IAuthService AuthService { get; set; }

    public override void ConfigureEndpoint()
    {
        Post("refresh");
        Group<AuthEndpointGroup>();
    }

    public override async Task HandleAsync(RefreshTokenRequest req, CancellationToken ct)
    {
        LoginResponse result = await AuthService.RefreshTokenAsync(req, ct);
        await SendOkResponseAsync(result, "Token refreshed successfully", ct);
    }
}