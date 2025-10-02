using FastEndpoints;
using MasLazu.AspNet.Framework.Endpoint.Endpoints;
using MasLazu.AspNet.Authentication.Core.Abstraction.Interfaces;
using MasLazu.AspNet.Authentication.Core.Abstraction.Models;
using MasLazu.AspNet.Framework.Application.Models;
using MasLazu.AspNet.Authentication.Core.Endpoint.EndpointGroups;

namespace MasLazu.AspNet.Authentication.Core.Endpoint.Endpoints.Genders;

public class GetGendersPaginatedEndpoint : BaseEndpoint<PaginationRequest, PaginatedResult<GenderDto>>
{
    public IGenderService GenderService { get; set; }

    public override void ConfigureEndpoint()
    {
        Post("/paginated");
        Group<GendersEndpointGroup>();
    }

    public override async Task HandleAsync(PaginationRequest req, CancellationToken ct)
    {
        PaginatedResult<GenderDto> result = await GenderService.GetPaginatedAsync(req, ct);
        await SendOkResponseAsync(result, "Genders Paginated Retrieved Successfully", ct);
    }
}
