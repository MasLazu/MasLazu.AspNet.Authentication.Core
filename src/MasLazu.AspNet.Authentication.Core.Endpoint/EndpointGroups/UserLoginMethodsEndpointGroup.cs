using FastEndpoints;
using Microsoft.AspNetCore.Http;
using MasLazu.AspNet.Framework.Endpoint.EndpointGroups;

namespace MasLazu.AspNet.Authentication.Core.Endpoint.EndpointGroups;

public class UserLoginMethodsEndpointGroup : SubGroup<V1EndpointGroup>
{
    public UserLoginMethodsEndpointGroup()
    {
        Configure("user-login-methods", ep => ep.Description(x => x.WithTags("UserLoginMethods")));
    }
}
