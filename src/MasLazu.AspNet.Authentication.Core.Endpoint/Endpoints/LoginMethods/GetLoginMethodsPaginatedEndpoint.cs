using FastEndpoints;
using MasLazu.AspNet.Framework.Endpoint.Endpoints;
using MasLazu.AspNet.Authentication.Core.Abstraction.Interfaces;
using MasLazu.AspNet.Authentication.Core.Abstraction.Models;
using MasLazu.AspNet.Framework.Application.Models;
using MasLazu.AspNet.Authentication.Core.Endpoint.EndpointGroups;

namespace MasLazu.AspNet.Authentication.Core.Endpoint.Endpoints.LoginMethods;

public class GetLoginMethodsPaginatedEndpoint : BaseEndpoint<PaginationRequest, PaginatedResult<LoginMethodDto>>
{
    public ILoginMethodService LoginMethodService { get; set; }

    public override void ConfigureEndpoint()
    {
        Post("/paginated");
        Group<LoginMethodsEndpointGroup>();
    }

    public override async Task HandleAsync(PaginationRequest req, CancellationToken ct)
    {
        PaginatedResult<LoginMethodDto> result = await LoginMethodService.GetPaginatedAsync(req, ct);
        await SendOkResponseAsync(result, "Login Methods Paginated Retrieved Successfully", ct);
    }
}
