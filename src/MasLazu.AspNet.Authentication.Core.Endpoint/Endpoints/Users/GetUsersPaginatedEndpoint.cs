using FastEndpoints;
using MasLazu.AspNet.Framework.Endpoint.Endpoints;
using MasLazu.AspNet.Authentication.Core.Abstraction.Interfaces;
using MasLazu.AspNet.Authentication.Core.Abstraction.Models;
using MasLazu.AspNet.Framework.Application.Models;
using MasLazu.AspNet.Authentication.Core.Endpoint.EndpointGroups;

namespace MasLazu.AspNet.Authentication.Core.Endpoint.Endpoints.Users;

public class GetUsersPaginatedEndpoint : BaseEndpoint<PaginationRequest, PaginatedResult<UserDto>>
{
    public IUserService UserService { get; set; }

    public override void ConfigureEndpoint()
    {
        Post("/paginated");
        Group<UsersEndpointGroup>();
        AllowAnonymous();
    }

    public override async Task HandleAsync(PaginationRequest req, CancellationToken ct)
    {
        PaginatedResult<UserDto> result = await UserService.GetPaginatedAsync(Guid.Empty, req, ct);
        await SendOkResponseAsync(result, "Users Paginated Retrieved Successfully", ct);
    }
}
