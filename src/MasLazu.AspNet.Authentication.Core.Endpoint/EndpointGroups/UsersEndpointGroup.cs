using FastEndpoints;
using Microsoft.AspNetCore.Http;
using MasLazu.AspNet.Framework.Endpoint.EndpointGroups;

namespace MasLazu.AspNet.Authentication.Core.Endpoint.EndpointGroups;

public class UsersEndpointGroup : SubGroup<V1EndpointGroup>
{
    public UsersEndpointGroup()
    {
        Configure("users", ep => ep.Description(x => x.WithTags("Users")));
    }
}
