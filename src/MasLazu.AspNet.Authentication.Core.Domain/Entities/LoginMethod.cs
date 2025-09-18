using MasLazu.AspNet.Framework.Domain.Entities;

namespace MasLazu.AspNet.Authentication.Core.Domain.Entities;

public class LoginMethod : BaseEntity
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsEnabled { get; set; } = true;
}