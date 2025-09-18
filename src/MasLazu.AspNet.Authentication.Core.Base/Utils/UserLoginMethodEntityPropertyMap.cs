using System.Linq.Expressions;
using MasLazu.AspNet.Framework.Application.Interfaces;
using MasLazu.AspNet.Authentication.Core.Domain.Entities;

namespace MasLazu.AspNet.Authentication.Core.Base.Utils;

public class UserLoginMethodEntityPropertyMap : IEntityPropertyMap<UserLoginMethod>
{
    private readonly Dictionary<string, Expression<Func<UserLoginMethod, object>>> _map =
        new(StringComparer.OrdinalIgnoreCase)
        {
            { "id", ulm => ulm.Id },
            { "userId", ulm => ulm.UserId },
            { "loginMethodCode", ulm => ulm.LoginMethodCode },
            { "createdAt", ulm => ulm.CreatedAt },
            { "updatedAt", ulm => ulm.UpdatedAt! },
        };

    public Expression<Func<UserLoginMethod, object>> Get(string property)
    {
        if (_map.TryGetValue(property, out Expression<Func<UserLoginMethod, object>>? expr))
        {
            return expr;
        }

        throw new ArgumentException($"Property '{property}' is not supported for UserLoginMethod. " +
            $"Supported properties: {string.Join(", ", _map.Keys)}");
    }
}
