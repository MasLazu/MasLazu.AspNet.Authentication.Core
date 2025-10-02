using FastEndpoints;
using MasLazu.AspNet.Framework.Endpoint.Endpoints;
using MasLazu.AspNet.Authentication.Core.Abstraction.Interfaces;
using MasLazu.AspNet.Authentication.Core.Abstraction.Models;
using MasLazu.AspNet.Framework.Application.Models;
using MasLazu.AspNet.Authentication.Core.Endpoint.EndpointGroups;
using MasLazu.AspNet.Framework.Application.Exceptions;

namespace MasLazu.AspNet.Authentication.Core.Endpoint.Endpoints.Languages;

public class GetLanguageByIdEndpoint : BaseEndpoint<IdRequest, LanguageDto>
{
    public ILanguageService LanguageService { get; set; }

    public override void ConfigureEndpoint()
    {
        Get("/{Id}");
        Group<LanguagesEndpointGroup>();
    }

    public override async Task HandleAsync(IdRequest req, CancellationToken ct)
    {
        LanguageDto result = await LanguageService.GetByIdAsync(req.Id, ct) ??
            throw new NotFoundException(nameof(LanguageDto), req.Id);
        await SendOkResponseAsync(result, "Language Retrieved Successfully", ct);
    }
}
