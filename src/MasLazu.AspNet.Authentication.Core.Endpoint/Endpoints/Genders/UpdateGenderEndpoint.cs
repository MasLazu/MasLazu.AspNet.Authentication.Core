using FastEndpoints;
using MasLazu.AspNet.Framework.Endpoint.Endpoints;
using MasLazu.AspNet.Authentication.Core.Abstraction.Interfaces;
using MasLazu.AspNet.Authentication.Core.Abstraction.Models;
using MasLazu.AspNet.Authentication.Core.Endpoint.EndpointGroups;

namespace MasLazu.AspNet.Authentication.Core.Endpoint.Endpoints.Genders;

public class UpdateGenderEndpoint : BaseEndpoint<UpdateGenderRequest, GenderDto>
{
    public IGenderService GenderService { get; set; }

    public override void ConfigureEndpoint()
    {
        Put("/{id}");
        Group<GendersEndpointGroup>();
    }

    public override async Task HandleAsync(UpdateGenderRequest req, CancellationToken ct)
    {
        GenderDto result = await GenderService.UpdateAsync(req, true, ct);
        await SendOkResponseAsync(result, "Gender Updated Successfully", ct);
    }
}
