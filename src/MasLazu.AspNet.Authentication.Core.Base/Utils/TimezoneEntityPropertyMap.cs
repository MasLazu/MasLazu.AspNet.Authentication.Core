using System.Linq.Expressions;
using MasLazu.AspNet.Framework.Application.Interfaces;
using MasLazu.AspNet.Authentication.Core.Domain.Entities;

namespace MasLazu.AspNet.Authentication.Core.Base.Utils;

public class TimezoneEntityPropertyMap : IEntityPropertyMap<Timezone>
{
    private readonly Dictionary<string, Expression<Func<Timezone, object>>> _map =
        new(StringComparer.OrdinalIgnoreCase)
        {
            { "id", tz => tz.Id },
            { "identifier", tz => tz.Identifier },
            { "name", tz => tz.Name },
            { "offsetMinutes", tz => tz.OffsetMinutes },
            { "createdAt", tz => tz.CreatedAt },
            { "updatedAt", tz => tz.UpdatedAt! },
        };

    public Expression<Func<Timezone, object>> Get(string property)
    {
        if (_map.TryGetValue(property, out Expression<Func<Timezone, object>>? expr))
        {
            return expr;
        }

        throw new ArgumentException($"Property '{property}' is not supported for Timezone. " +
            $"Supported properties: {string.Join(", ", _map.Keys)}");
    }
}
