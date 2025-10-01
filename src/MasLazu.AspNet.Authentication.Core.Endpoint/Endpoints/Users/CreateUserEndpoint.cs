using FastEndpoints;
using MasLazu.AspNet.Framework.Endpoint.Endpoints;
using MasLazu.AspNet.Authentication.Core.Abstraction.Interfaces;
using MasLazu.AspNet.Authentication.Core.Abstraction.Models;
using MasLazu.AspNet.Authentication.Core.Endpoint.EndpointGroups;

namespace MasLazu.AspNet.Authentication.Core.Endpoint.Endpoints.Users;

public class CreateUserEndpoint : BaseEndpoint<CreateUserRequest, UserDto>
{
    public IUserService UserService { get; set; }

    public override void ConfigureEndpoint()
    {
        Post("");
        Group<UsersEndpointGroup>();
    }

    public override async Task HandleAsync(CreateUserRequest req, CancellationToken ct)
    {
        UserDto result = await UserService.CreateAsync(Guid.Empty, req, true, ct);
        await SendOkResponseAsync(result, "User Created Successfully", ct);
    }
}
