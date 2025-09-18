using System.Linq.Expressions;
using MasLazu.AspNet.Framework.Application.Interfaces;
using MasLazu.AspNet.Authentication.Core.Domain.Entities;

namespace MasLazu.AspNet.Authentication.Core.Base.Utils;

public class UserEntityPropertyMap : IEntityPropertyMap<User>
{
    private readonly Dictionary<string, Expression<Func<User, object>>> _map =
        new(StringComparer.OrdinalIgnoreCase)
        {
            { "id", u => u.Id },
            { "name", u => u.Name },
            { "email", u => u.Email! },
            { "phoneNumber", u => u.PhoneNumber! },
            { "username", u => u.Username! },
            { "languageCode", u => u.LanguageCode! },
            { "timezoneId", u => u.TimezoneId! },
            { "profilePicture", u => u.ProfilePicture! },
            { "genderCode", u => u.GenderCode! },
            { "createdAt", u => u.CreatedAt },
            { "updatedAt", u => u.UpdatedAt! }
        };

    public Expression<Func<User, object>> Get(string property)
    {
        if (_map.TryGetValue(property, out Expression<Func<User, object>>? expr))
        {
            return expr;
        }

        throw new ArgumentException($"Unknown property: {property}");
    }
}
