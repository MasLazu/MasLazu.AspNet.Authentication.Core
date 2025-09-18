using FastEndpoints;
using Microsoft.AspNetCore.Http;
using MasLazu.AspNet.Framework.Endpoint.EndpointGroups;

namespace MasLazu.AspNet.Authentication.Core.Endpoint.EndpointGroups;

public class LoginMethodsEndpointGroup : SubGroup<V1EndpointGroup>
{
    public LoginMethodsEndpointGroup()
    {
        Configure("login-methods", ep => ep.Description(x => x.WithTags("Login Methods")));
    }
}
