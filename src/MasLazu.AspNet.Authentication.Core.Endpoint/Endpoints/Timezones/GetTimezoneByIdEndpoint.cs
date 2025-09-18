using FastEndpoints;
using MasLazu.AspNet.Framework.Endpoint.Endpoints;
using MasLazu.AspNet.Authentication.Core.Abstraction.Interfaces;
using MasLazu.AspNet.Authentication.Core.Abstraction.Models;
using MasLazu.AspNet.Framework.Application.Models;
using MasLazu.AspNet.Authentication.Core.Endpoint.EndpointGroups;
using MasLazu.AspNet.Framework.Application.Exceptions;

namespace MasLazu.AspNet.Authentication.Core.Endpoint.Endpoints.Timezones;

public class GetTimezoneByIdEndpoint : BaseEndpoint<IdRequest, TimezoneDto>
{
    public ITimezoneService TimezoneService { get; set; }

    public override void ConfigureEndpoint()
    {
        Get("/{Id}");
        Group<TimezonesEndpointGroup>();
    }

    public override async Task HandleAsync(IdRequest req, CancellationToken ct)
    {
        TimezoneDto result = await TimezoneService.GetByIdAsync(Guid.Empty, req.Id, ct) ??
            throw new NotFoundException(nameof(TimezoneDto), req.Id);
        await SendOkResponseAsync(result, "Timezone Retrieved Successfully", ct);
    }
}
