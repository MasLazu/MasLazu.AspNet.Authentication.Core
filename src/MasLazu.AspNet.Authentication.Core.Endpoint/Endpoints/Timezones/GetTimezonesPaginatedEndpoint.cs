using FastEndpoints;
using MasLazu.AspNet.Framework.Endpoint.Endpoints;
using MasLazu.AspNet.Authentication.Core.Abstraction.Interfaces;
using MasLazu.AspNet.Authentication.Core.Abstraction.Models;
using MasLazu.AspNet.Framework.Application.Models;
using MasLazu.AspNet.Authentication.Core.Endpoint.EndpointGroups;

namespace MasLazu.AspNet.Authentication.Core.Endpoint.Endpoints.Timezones;

public class GetTimezonesPaginatedEndpoint : BaseEndpoint<PaginationRequest, PaginatedResult<TimezoneDto>>
{
    public ITimezoneService TimezoneService { get; set; }

    public override void ConfigureEndpoint()
    {
        Post("/paginated");
        Group<TimezonesEndpointGroup>();
    }

    public override async Task HandleAsync(PaginationRequest req, CancellationToken ct)
    {
        PaginatedResult<TimezoneDto> result = await TimezoneService.GetPaginatedAsync(req, ct);
        await SendOkResponseAsync(result, "Timezones Paginated Retrieved Successfully", ct);
    }
}
