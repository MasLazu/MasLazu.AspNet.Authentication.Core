using FastEndpoints;
using MasLazu.AspNet.Framework.Endpoint.Endpoints;
using MasLazu.AspNet.Authentication.Core.Abstraction.Interfaces;
using MasLazu.AspNet.Authentication.Core.Abstraction.Models;
using MasLazu.AspNet.Authentication.Core.Endpoint.EndpointGroups;

namespace MasLazu.AspNet.Authentication.Core.Endpoint.Endpoints.LoginMethods;

public class GetAllEnabledLoginMethodsEndpoint : BaseEndpointWithoutRequest<IEnumerable<LoginMethodDto>>
{
    public ILoginMethodService LoginMethodService { get; set; }

    public override void ConfigureEndpoint()
    {
        Get("/enabled");
        Group<LoginMethodsEndpointGroup>();
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        IEnumerable<LoginMethodDto> result = await LoginMethodService.GetAllEnabledAsync(ct);
        await SendOkResponseAsync(result, "Enabled Login Methods Retrieved Successfully");
    }
}
