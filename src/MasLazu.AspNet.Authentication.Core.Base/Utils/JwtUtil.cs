using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MasLazu.AspNet.Authentication.Core.Base.Configuration;

namespace MasLazu.AspNet.Authentication.Core.Base.Utils;

public class JwtUtil
{
    private readonly JwtConfiguration _jwtConfig;
    private readonly SymmetricSecurityKey _signingKey;
    private readonly SigningCredentials _signingCredentials;

    public JwtUtil(IOptions<JwtConfiguration> jwtConfig)
    {
        _jwtConfig = jwtConfig.Value;
        _signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Key));
        _signingCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
    }

    public int AccessTokenExpirationMinutes => _jwtConfig.AccessTokenExpirationMinutes;

    public int RefreshTokenExpirationDays => _jwtConfig.RefreshTokenExpirationDays;

    public (string Token, DateTimeOffset ExpiresAt) GenerateAccessToken(Guid userId, string? email, IEnumerable<string>? roles = null, IDictionary<string, object>? additionalClaims = null)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, email ?? string.Empty),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
        };

        if (roles != null)
        {
            foreach (string role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
        }

        if (additionalClaims != null)
        {
            foreach (KeyValuePair<string, object> claim in additionalClaims)
            {
                claims.Add(new Claim(claim.Key, claim.Value.ToString() ?? string.Empty));
            }
        }

        DateTimeOffset expiresAt = DateTimeOffset.UtcNow.AddMinutes(_jwtConfig.AccessTokenExpirationMinutes);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expiresAt.DateTime,
            Issuer = _jwtConfig.Issuer,
            Audience = _jwtConfig.Audience,
            SigningCredentials = _signingCredentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        string tokenString = tokenHandler.WriteToken(token);

        return (tokenString, expiresAt);
    }

    public (string Token, DateTimeOffset ExpiresAt) GenerateRefreshToken(Guid userId)
    {
        byte[] randomBytes = new byte[64];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
        }

        DateTimeOffset createdAt = DateTimeOffset.UtcNow;
        DateTimeOffset expiresAt = createdAt.AddDays(_jwtConfig.RefreshTokenExpirationDays);

        string tokenData = $"{userId}:{Convert.ToBase64String(randomBytes)}:{createdAt.ToUnixTimeSeconds()}";
        string tokenString = Convert.ToBase64String(Encoding.UTF8.GetBytes(tokenData));

        return (tokenString, expiresAt);
    }

    public ClaimsPrincipal? ValidateAccessToken(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,
                ValidateIssuer = true,
                ValidIssuer = _jwtConfig.Issuer,
                ValidateAudience = true,
                ValidAudience = _jwtConfig.Audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out _);
            return principal;
        }
        catch
        {
            return null;
        }
    }

    public bool ValidateRefreshToken(string token)
    {
        try
        {
            string tokenData = Encoding.UTF8.GetString(Convert.FromBase64String(token));
            string[] parts = tokenData.Split(':');

            if (parts.Length != 3)
            {
                return false;
            }

            string userId = parts[0];
            string randomBytes = parts[1];
            string timestampString = parts[2];

            if (long.TryParse(timestampString, out long timestamp))
            {
                var tokenCreationTime = DateTimeOffset.FromUnixTimeSeconds(timestamp);
                DateTimeOffset expirationTime = tokenCreationTime.AddDays(_jwtConfig.RefreshTokenExpirationDays);

                return DateTimeOffset.UtcNow <= expirationTime;
            }

            return false;
        }
        catch
        {
            return false;
        }
    }

    public string? GetUserIdFromRefreshToken(string token)
    {
        try
        {
            string tokenData = Encoding.UTF8.GetString(Convert.FromBase64String(token));
            string[] parts = tokenData.Split(':');

            if (parts.Length >= 1)
            {
                return parts[0];
            }

            return null;
        }
        catch
        {
            return null;
        }
    }
}
