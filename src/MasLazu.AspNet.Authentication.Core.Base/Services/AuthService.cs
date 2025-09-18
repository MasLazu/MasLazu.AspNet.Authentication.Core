using MasLazu.AspNet.Authentication.Core.Abstraction.Interfaces;
using MasLazu.AspNet.Authentication.Core.Base.Utils;
using MasLazu.AspNet.Authentication.Core.Domain.Entities;
using MasLazu.AspNet.Framework.Application.Exceptions;
using MasLazu.AspNet.Framework.Application.Interfaces;
using MasLazu.AspNet.Authentication.Core.Abstraction.Models;
using Mapster;
using MasLazu.AspNet.Verification.Abstraction.Models;
using MasLazu.AspNet.Verification.Abstraction.Interfaces;

namespace MasLazu.AspNet.Authentication.Core.Base.Services;

public class AuthService : IAuthService
{
    private readonly IRepository<User> _userRepository;
    private readonly IReadRepository<LoginMethod> _loginMethodRepository;
    private readonly IRepository<UserLoginMethod> _userLoginMethodRepository;
    private readonly IRepository<RefreshToken> _refreshTokenRepository;
    private readonly IRepository<UserRefreshToken> _userRefreshTokenRepository;
    private readonly JwtUtil _jwtUtil;
    private readonly IUnitOfWork _unitOfWork;

    public AuthService(
        IRepository<User> userRepository,
        IRepository<UserLoginMethod> userLoginMethodRepository,
        IRepository<RefreshToken> refreshTokenRepository,
        IRepository<UserRefreshToken> userRefreshTokenRepository,
        IReadRepository<LoginMethod> loginMethodRepository,
        JwtUtil jwtUtil,
        IUnitOfWork unitOfWork,
        IVerificationService verificationService
    )
    {
        _userRepository = userRepository;
        _userLoginMethodRepository = userLoginMethodRepository;
        _loginMethodRepository = loginMethodRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _userRefreshTokenRepository = userRefreshTokenRepository;
        _jwtUtil = jwtUtil;
        _unitOfWork = unitOfWork;
    }

    public async Task<UserLoginMethodDto> GetUserLoginMethodByUserIdAsync(Guid userId, CancellationToken ct = default)
    {
        UserLoginMethod userLoginMethod = await _userLoginMethodRepository.FirstOrDefaultAsync(ulm => ulm.UserId == userId, ct) ??
            throw new NotFoundException(nameof(UserLoginMethod), $"No login method found for user with ID {userId}");

        return userLoginMethod.Adapt<UserLoginMethodDto>();
    }

    public async Task<LoginResponse> LoginAsync(Guid userLoginMethodId, CancellationToken ct = default)
    {
        UserLoginMethod? userLoginMethod = await _userLoginMethodRepository.GetByIdAsync(userLoginMethodId, ct) ??
            throw new UnauthorizedException(nameof(UserLoginMethod));

        User? user = await _userRepository.GetByIdAsync(userLoginMethod.UserId, ct) ??
            throw new UnauthorizedException(nameof(User));

        LoginMethod loginMethod = await _loginMethodRepository.FirstOrDefaultAsync(lm => lm.Code == userLoginMethod.LoginMethodCode, ct) ??
            throw new UnauthorizedException(nameof(LoginMethod));

        if (!loginMethod.IsEnabled)
        {
            throw new ForbiddenException("Login method is disabled.");
        }

        (string accessTokenString, DateTimeOffset accessTokenExpiresAt) = _jwtUtil.GenerateAccessToken(user.Id, user.Email);
        (string refreshTokenString, DateTimeOffset refreshTokenExpiresAt) = _jwtUtil.GenerateRefreshToken(user.Id);

        var refreshToken = new RefreshToken
        {
            Token = refreshTokenString,
            ExpiresDate = refreshTokenExpiresAt
        };

        await _refreshTokenRepository.AddAsync(refreshToken, ct);
        await _userRefreshTokenRepository.AddAsync(new UserRefreshToken
        {
            UserId = user.Id,
            RefreshTokenId = refreshToken.Id
        }, ct);

        await _unitOfWork.SaveChangesAsync(ct);

        return new LoginResponse(
            AccessToken: accessTokenString,
            RefreshToken: refreshTokenString,
            AccessTokenExpiresAt: accessTokenExpiresAt,
            RefreshTokenExpiresAt: refreshTokenExpiresAt
        );
    }
}