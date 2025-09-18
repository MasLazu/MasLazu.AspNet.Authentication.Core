namespace MasLazu.AspNet.Authentication.Core.Abstraction.Models;

public record LoginResponse(
    string AccessToken,
    string RefreshToken,
    DateTimeOffset AccessTokenExpiresAt,
    DateTimeOffset RefreshTokenExpiresAt,
    string TokenType = "Bearer"
);
