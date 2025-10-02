using FastEndpoints;
using MasLazu.AspNet.Framework.Endpoint.Endpoints;
using MasLazu.AspNet.Authentication.Core.Abstraction.Interfaces;
using MasLazu.AspNet.Authentication.Core.Abstraction.Models;
using MasLazu.AspNet.Framework.Application.Models;
using MasLazu.AspNet.Authentication.Core.Endpoint.EndpointGroups;

namespace MasLazu.AspNet.Authentication.Core.Endpoint.Endpoints.UserLoginMethods;

public class GetUserLoginMethodsByUserIdEndpoint : BaseEndpoint<IdRequest, IEnumerable<UserLoginMethodDto>>
{
    public IUserLoginMethodService UserLoginMethodService { get; set; }

    public override void ConfigureEndpoint()
    {
        Get("/{Id}/login-methods");
        Group<UsersEndpointGroup>();
    }

    public override async Task HandleAsync(IdRequest req, CancellationToken ct)
    {
        IEnumerable<UserLoginMethodDto> result = await UserLoginMethodService.GetByUserIdAsync(req.Id, ct);
        await SendOkResponseAsync(result, "User Login Methods Retrieved Successfully", ct);
    }
}
