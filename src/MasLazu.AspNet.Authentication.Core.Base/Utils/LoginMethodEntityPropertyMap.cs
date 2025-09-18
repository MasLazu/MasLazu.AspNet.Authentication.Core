using System.Linq.Expressions;
using MasLazu.AspNet.Framework.Application.Interfaces;
using MasLazu.AspNet.Authentication.Core.Domain.Entities;

namespace MasLazu.AspNet.Authentication.Core.Base.Utils;

public class LoginMethodEntityPropertyMap : IEntityPropertyMap<LoginMethod>
{
    private readonly Dictionary<string, Expression<Func<LoginMethod, object>>> _map =
        new(StringComparer.OrdinalIgnoreCase)
        {
            { "id", lm => lm.Id },
            { "code", lm => lm.Code },
            { "createdAt", lm => lm.CreatedAt },
            { "updatedAt", lm => lm.UpdatedAt! },
        };

    public Expression<Func<LoginMethod, object>> Get(string property)
    {
        if (_map.TryGetValue(property, out Expression<Func<LoginMethod, object>>? expr))
        {
            return expr;
        }

        throw new ArgumentException($"Property '{property}' is not supported for LoginMethod. " +
            $"Supported properties: {string.Join(", ", _map.Keys)}");
    }
}
