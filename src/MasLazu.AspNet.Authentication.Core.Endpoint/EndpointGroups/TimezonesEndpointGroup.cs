using FastEndpoints;
using Microsoft.AspNetCore.Http;
using MasLazu.AspNet.Framework.Endpoint.EndpointGroups;

namespace MasLazu.AspNet.Authentication.Core.Endpoint.EndpointGroups;

public class TimezonesEndpointGroup : SubGroup<V1EndpointGroup>
{
    public TimezonesEndpointGroup()
    {
        Configure("timezones", ep => ep.Description(x => x.WithTags("Timezones")));
    }
}
