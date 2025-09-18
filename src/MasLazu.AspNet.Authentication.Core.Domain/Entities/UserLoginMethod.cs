using MasLazu.AspNet.Framework.Domain.Entities;

namespace MasLazu.AspNet.Authentication.Core.Domain.Entities;

public class UserLoginMethod : BaseEntity
{
    public Guid UserId { get; set; }
    public string LoginMethodCode { get; set; } = string.Empty;
    public DateTimeOffset? LastLoginAt { get; set; }

    public User? User { get; set; }
}