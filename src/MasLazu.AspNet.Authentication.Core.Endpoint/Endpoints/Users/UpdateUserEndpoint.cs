using FastEndpoints;
using MasLazu.AspNet.Framework.Endpoint.Endpoints;
using MasLazu.AspNet.Authentication.Core.Abstraction.Interfaces;
using MasLazu.AspNet.Authentication.Core.Abstraction.Models;
using MasLazu.AspNet.Authentication.Core.Endpoint.EndpointGroups;

namespace MasLazu.AspNet.Authentication.Core.Endpoint.Endpoints.Users;

public class UpdateUserEndpoint : BaseEndpoint<UpdateUserRequest, UserDto>
{
    public IUserService UserService { get; set; }

    public override void ConfigureEndpoint()
    {
        Put("/{id}");
        Group<UsersEndpointGroup>();
    }

    public override async Task HandleAsync(UpdateUserRequest req, CancellationToken ct)
    {
        UserDto result = await UserService.UpdateAsync(Guid.Empty, req, true, ct);
        await SendOkResponseAsync(result, "User Updated Successfully", ct);
    }
}
