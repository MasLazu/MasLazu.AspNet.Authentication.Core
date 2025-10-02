using FastEndpoints;
using MasLazu.AspNet.Framework.Endpoint.Endpoints;
using MasLazu.AspNet.Authentication.Core.Abstraction.Interfaces;
using MasLazu.AspNet.Authentication.Core.Abstraction.Models;
using MasLazu.AspNet.Framework.Application.Models;
using MasLazu.AspNet.Authentication.Core.Endpoint.EndpointGroups;
using MasLazu.AspNet.Framework.Application.Exceptions;

namespace MasLazu.AspNet.Authentication.Core.Endpoint.Endpoints.Genders;

public class GetGenderByIdEndpoint : BaseEndpoint<IdRequest, GenderDto>
{
    public IGenderService GenderService { get; set; }

    public override void ConfigureEndpoint()
    {
        Get("/{Id}");
        Group<GendersEndpointGroup>();
    }

    public override async Task HandleAsync(IdRequest req, CancellationToken ct)
    {
        GenderDto? result = await GenderService.GetByIdAsync(req.Id, ct) ??
            throw new NotFoundException(nameof(GenderDto), req.Id);
        await SendOkResponseAsync(result, "Gender Retrieved Successfully", ct);
    }
}
