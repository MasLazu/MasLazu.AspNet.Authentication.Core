using System.Linq.Expressions;
using MasLazu.AspNet.Framework.Application.Interfaces;
using MasLazu.AspNet.Authentication.Core.Domain.Entities;

namespace MasLazu.AspNet.Authentication.Core.Base.Utils;

public class RefreshTokenEntityPropertyMap : IEntityPropertyMap<RefreshToken>
{
    private readonly Dictionary<string, Expression<Func<RefreshToken, object>>> _map =
        new(StringComparer.OrdinalIgnoreCase)
        {
            { "id", rt => rt.Id },
            { "token", rt => rt.Token },
            { "expiresDate", rt => rt.ExpiresDate! },
            { "revokedDate", rt => rt.RevokedDate! },
            { "createdAt", rt => rt.CreatedAt },
            { "updatedAt", rt => rt.UpdatedAt! },
        };

    public Expression<Func<RefreshToken, object>> Get(string property)
    {
        if (_map.TryGetValue(property, out Expression<Func<RefreshToken, object>>? expr))
        {
            return expr;
        }

        throw new ArgumentException($"Property '{property}' is not supported for RefreshToken. " +
            $"Supported properties: {string.Join(", ", _map.Keys)}");
    }
}
