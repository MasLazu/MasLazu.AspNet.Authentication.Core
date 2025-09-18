namespace MasLazu.AspNet.Authentication.Core.Base.Configuration;

/// <summary>
/// JWT configuration settings
/// </summary>
public class JwtConfiguration
{
    /// <summary>
    /// The JWT signing key
    /// </summary>
    public string Key { get; set; } = string.Empty;

    /// <summary>
    /// The JWT secret (can be same as Key for HMAC)
    /// </summary>
    public string Secret { get; set; } = string.Empty;

    /// <summary>
    /// The JWT issuer
    /// </summary>
    public string Issuer { get; set; } = string.Empty;

    /// <summary>
    /// The JWT audience
    /// </summary>
    public string Audience { get; set; } = string.Empty;

    /// <summary>
    /// Access token expiration time in minutes
    /// </summary>
    public int AccessTokenExpirationMinutes { get; set; } = 120;

    /// <summary>
    /// Refresh token expiration time in days
    /// </summary>
    public int RefreshTokenExpirationDays { get; set; } = 30;
}
