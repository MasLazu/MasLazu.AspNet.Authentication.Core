using MasLazu.AspNet.Framework.Domain.Entities;

namespace MasLazu.AspNet.Authentication.Core.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Username { get; set; }
    public string? LanguageCode { get; set; }
    public Guid? TimezoneId { get; set; }
    public string? ProfilePicture { get; set; }
    public string? GenderCode { get; set; }
    public bool IsEmailVerified { get; set; }
    public bool IsPhoneNumberVerified { get; set; }

    public Timezone? Timezone { get; set; }
    public Gender? Gender { get; set; }
    public Language? Language { get; set; }
    public ICollection<UserLoginMethod> UserLoginMethods { get; set; } = [];
}