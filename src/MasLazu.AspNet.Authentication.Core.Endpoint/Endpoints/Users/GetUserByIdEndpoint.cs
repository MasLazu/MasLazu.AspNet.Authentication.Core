using FastEndpoints;
using MasLazu.AspNet.Framework.Endpoint.Endpoints;
using MasLazu.AspNet.Authentication.Core.Abstraction.Interfaces;
using MasLazu.AspNet.Authentication.Core.Abstraction.Models;
using MasLazu.AspNet.Framework.Application.Models;
using MasLazu.AspNet.Authentication.Core.Endpoint.EndpointGroups;
using MasLazu.AspNet.Framework.Application.Exceptions;

namespace MasLazu.AspNet.Authentication.Core.Endpoint.Endpoints.Users;

public class GetUserByIdEndpoint : BaseEndpoint<IdRequest, UserDto>
{
    public IUserService UserService { get; set; }

    public override void ConfigureEndpoint()
    {
        Get("/{Id}");
        Group<UsersEndpointGroup>();
    }

    public override async Task HandleAsync(IdRequest req, CancellationToken ct)
    {
        UserDto result = await UserService.GetByIdAsync(req.Id, ct) ??
            throw new NotFoundException(nameof(UserDto), req.Id);
        await SendOkResponseAsync(result, "User Retrieved Successfully", ct);
    }
}