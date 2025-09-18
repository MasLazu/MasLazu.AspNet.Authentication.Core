using FastEndpoints;
using MasLazu.AspNet.Framework.Endpoint.Endpoints;
using MasLazu.AspNet.Authentication.Core.Abstraction.Interfaces;
using MasLazu.AspNet.Framework.Application.Models;
using MasLazu.AspNet.Authentication.Core.Endpoint.EndpointGroups;

namespace MasLazu.AspNet.Authentication.Core.Endpoint.Endpoints.Genders;

public class DeleteGenderEndpoint : BaseEndpointWithoutResponse<IdRequest>
{
    public IGenderService GenderService { get; set; }

    public override void ConfigureEndpoint()
    {
        Delete("/{Id}");
        Group<GendersEndpointGroup>();
    }

    public override async Task HandleAsync(IdRequest req, CancellationToken ct)
    {
        await GenderService.DeleteAsync(Guid.Empty, req.Id, ct);
        await SendOkResponseAsync("Gender Deleted Successfully", ct);
    }
}
