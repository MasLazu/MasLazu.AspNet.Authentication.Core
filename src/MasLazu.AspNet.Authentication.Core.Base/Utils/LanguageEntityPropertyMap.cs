using System.Linq.Expressions;
using MasLazu.AspNet.Framework.Application.Interfaces;
using MasLazu.AspNet.Authentication.Core.Domain.Entities;

namespace MasLazu.AspNet.Authentication.Core.Base.Utils;

public class LanguageEntityPropertyMap : IEntityPropertyMap<Language>
{
    private readonly Dictionary<string, Expression<Func<Language, object>>> _map =
        new(StringComparer.OrdinalIgnoreCase)
        {
            { "id", l => l.Id },
            { "code", l => l.Code },
            { "name", l => l.Name },
            { "createdAt", l => l.CreatedAt },
            { "updatedAt", l => l.UpdatedAt! },
        };

    public Expression<Func<Language, object>> Get(string property)
    {
        if (_map.TryGetValue(property, out Expression<Func<Language, object>>? expr))
        {
            return expr;
        }

        throw new ArgumentException($"Property '{property}' is not supported for Language. " +
            $"Supported properties: {string.Join(", ", _map.Keys)}");
    }
}
