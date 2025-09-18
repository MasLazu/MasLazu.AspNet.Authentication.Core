using FastEndpoints;
using Microsoft.AspNetCore.Http;
using MasLazu.AspNet.Framework.Endpoint.EndpointGroups;

namespace MasLazu.AspNet.Authentication.Core.Endpoint.EndpointGroups;

public class LanguagesEndpointGroup : SubGroup<V1EndpointGroup>
{
    public LanguagesEndpointGroup()
    {
        Configure("languages", ep => ep.Description(x => x.WithTags("Languages")));
    }
}
