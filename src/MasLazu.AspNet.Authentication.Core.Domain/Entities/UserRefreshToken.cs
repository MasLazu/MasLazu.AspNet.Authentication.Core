using System;
using MasLazu.AspNet.Framework.Domain.Entities;

namespace MasLazu.AspNet.Authentication.Core.Domain.Entities;

public class UserRefreshToken : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid RefreshTokenId { get; set; }

    public User? User { get; set; }
    public RefreshToken? RefreshToken { get; set; }
}
