using FastEndpoints;
using MasLazu.AspNet.Framework.Endpoint.Endpoints;
using MasLazu.AspNet.Authentication.Core.Abstraction.Interfaces;
using MasLazu.AspNet.Authentication.Core.Abstraction.Models;
using MasLazu.AspNet.Authentication.Core.Endpoint.EndpointGroups;

namespace MasLazu.AspNet.Authentication.Core.Endpoint.Endpoints.Genders;

public class CreateGenderEndpoint : BaseEndpoint<CreateGenderRequest, GenderDto>
{
    public IGenderService GenderService { get; set; }

    public override void ConfigureEndpoint()
    {
        Post("");
        Group<GendersEndpointGroup>();
    }

    public override async Task HandleAsync(CreateGenderRequest req, CancellationToken ct)
    {
        GenderDto result = await GenderService.CreateAsync(req, true, ct);
        await SendOkResponseAsync(result, "Gender Created Successfully", ct);
    }
}
