using FastEndpoints;
using Microsoft.AspNetCore.Http;
using MasLazu.AspNet.Framework.Endpoint.EndpointGroups;

namespace MasLazu.AspNet.Authentication.Core.Endpoint.EndpointGroups;

public class GendersEndpointGroup : SubGroup<V1EndpointGroup>
{
    public GendersEndpointGroup()
    {
        Configure("genders", ep => ep.Description(x => x.WithTags("Genders")));
    }
}
