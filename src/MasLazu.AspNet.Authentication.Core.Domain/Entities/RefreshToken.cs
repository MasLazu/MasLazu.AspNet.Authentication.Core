using System;
using MasLazu.AspNet.Framework.Domain.Entities;

namespace MasLazu.AspNet.Authentication.Core.Domain.Entities;

public class RefreshToken : BaseEntity
{
    public string Token { get; set; } = string.Empty;
    public DateTimeOffset? ExpiresDate { get; set; }
    public DateTimeOffset? RevokedDate { get; set; }
}
