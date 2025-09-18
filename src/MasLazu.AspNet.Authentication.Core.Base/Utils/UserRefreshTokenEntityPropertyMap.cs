using System.Linq.Expressions;
using MasLazu.AspNet.Framework.Application.Interfaces;

namespace MasLazu.AspNet.Authentication.Core.Base.Utils;

public class UserRefreshTokenEntityPropertyMap : IEntityPropertyMap<Domain.Entities.UserRefreshToken>
{
    private readonly Dictionary<string, Expression<Func<Domain.Entities.UserRefreshToken, object>>> _map =
        new(StringComparer.OrdinalIgnoreCase)
        {
            { "id", urt => urt.Id },
            { "userId", urt => urt.UserId },
            { "refreshTokenId", urt => urt.RefreshTokenId }
        };

    public Expression<Func<Domain.Entities.UserRefreshToken, object>> Get(string property)
    {
        if (_map.TryGetValue(property, out Expression<Func<Domain.Entities.UserRefreshToken, object>>? expr))
        {
            return expr;
        }

        throw new ArgumentException($"Property '{property}' is not supported for UserRefreshToken. " +
            $"Supported properties: {string.Join(", ", _map.Keys)}");
    }
}
