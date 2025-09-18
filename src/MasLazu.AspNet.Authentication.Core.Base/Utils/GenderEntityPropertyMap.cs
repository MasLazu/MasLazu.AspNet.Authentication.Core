using System.Linq.Expressions;
using MasLazu.AspNet.Framework.Application.Interfaces;
using MasLazu.AspNet.Authentication.Core.Domain.Entities;

namespace MasLazu.AspNet.Authentication.Core.Base.Utils;

public class GenderEntityPropertyMap : IEntityPropertyMap<Gender>
{
    private readonly Dictionary<string, Expression<Func<Gender, object>>> _map =
        new(StringComparer.OrdinalIgnoreCase)
        {
            { "id", g => g.Id },
            { "code", g => g.Code },
            { "name", g => g.Name },
            { "createdAt", g => g.CreatedAt },
            { "updatedAt", g => g.UpdatedAt! },
        };

    public Expression<Func<Gender, object>> Get(string property)
    {
        if (_map.TryGetValue(property, out Expression<Func<Gender, object>>? expr))
        {
            return expr;
        }

        throw new ArgumentException($"Property '{property}' is not supported for Gender. " +
            $"Supported properties: {string.Join(", ", _map.Keys)}");
    }
}
