using MasLazu.AspNet.Framework.Domain.Entities;

namespace MasLazu.AspNet.Authentication.Core.Domain.Entities;

public class Language : BaseEntity
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}
