using FastEndpoints;
using MasLazu.AspNet.Framework.Endpoint.Endpoints;
using MasLazu.AspNet.Authentication.Core.Abstraction.Interfaces;
using MasLazu.AspNet.Authentication.Core.Abstraction.Models;
using MasLazu.AspNet.Framework.Application.Models;
using MasLazu.AspNet.Authentication.Core.Endpoint.EndpointGroups;
using MasLazu.AspNet.Framework.Application.Exceptions;

namespace MasLazu.AspNet.Authentication.Core.Endpoint.Endpoints.LoginMethods;

public class GetLoginMethodByIdEndpoint : BaseEndpoint<IdRequest, LoginMethodDto>
{
    public ILoginMethodService LoginMethodService { get; set; }

    public override void ConfigureEndpoint()
    {
        Get("/{Id}");
        Group<LoginMethodsEndpointGroup>();
    }

    public override async Task HandleAsync(IdRequest req, CancellationToken ct)
    {
        LoginMethodDto? result = await LoginMethodService.GetByIdAsync(Guid.Empty, req.Id, ct) ??
            throw new NotFoundException(nameof(LoginMethodDto), req.Id);
        await SendOkResponseAsync(result, "Login Method Retrieved Successfully", ct);
    }
}
