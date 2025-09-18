using MasLazu.AspNet.Framework.Domain.Entities;

namespace MasLazu.AspNet.Authentication.Core.Domain.Entities;

public class Timezone : BaseEntity
{
    public string Identifier { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int OffsetMinutes { get; set; }
}
