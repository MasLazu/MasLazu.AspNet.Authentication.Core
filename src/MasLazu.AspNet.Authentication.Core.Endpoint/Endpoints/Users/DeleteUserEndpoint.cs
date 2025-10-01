using FastEndpoints;
using MasLazu.AspNet.Framework.Endpoint.Endpoints;
using MasLazu.AspNet.Authentication.Core.Abstraction.Interfaces;
using MasLazu.AspNet.Framework.Application.Models;
using MasLazu.AspNet.Authentication.Core.Endpoint.EndpointGroups;

namespace MasLazu.AspNet.Authentication.Core.Endpoint.Endpoints.Users;

public class DeleteUserEndpoint : BaseEndpointWithoutResponse<IdRequest>
{
    public IUserService UserService { get; set; }

    public override void ConfigureEndpoint()
    {
        Delete("/{Id}");
        Group<UsersEndpointGroup>();
    }

    public override async Task HandleAsync(IdRequest req, CancellationToken ct)
    {
        await UserService.DeleteAsync(Guid.Empty, req.Id, true, ct);
        await SendOkResponseAsync("User Deleted Successfully", ct);
    }
}
