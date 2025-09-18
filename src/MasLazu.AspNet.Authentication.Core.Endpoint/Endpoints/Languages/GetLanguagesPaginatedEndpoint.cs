using FastEndpoints;
using MasLazu.AspNet.Framework.Endpoint.Endpoints;
using MasLazu.AspNet.Authentication.Core.Abstraction.Interfaces;
using MasLazu.AspNet.Authentication.Core.Abstraction.Models;
using MasLazu.AspNet.Framework.Application.Models;
using MasLazu.AspNet.Authentication.Core.Endpoint.EndpointGroups;

namespace MasLazu.AspNet.Authentication.Core.Endpoint.Endpoints.Languages;

public class GetLanguagesPaginatedEndpoint : BaseEndpoint<PaginationRequest, PaginatedResult<LanguageDto>>
{
    public ILanguageService LanguageService { get; set; }

    public override void ConfigureEndpoint()
    {
        Post("/paginated");
        Group<LanguagesEndpointGroup>();
    }

    public override async Task HandleAsync(PaginationRequest req, CancellationToken ct)
    {
        PaginatedResult<LanguageDto> result = await LanguageService.GetPaginatedAsync(Guid.Empty, req, ct);
        await SendOkResponseAsync(result, "Languages Paginated Retrieved Successfully", ct);
    }
}
