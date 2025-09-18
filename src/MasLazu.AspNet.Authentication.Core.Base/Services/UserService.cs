using FluentValidation;
using MasLazu.AspNet.Authentication.Core.Abstraction.Interfaces;
using MasLazu.AspNet.Authentication.Core.Abstraction.Models;
using MasLazu.AspNet.Authentication.Core.Domain.Entities;
using MasLazu.AspNet.Framework.Application.Interfaces;
using MasLazu.AspNet.Framework.Application.Services;
using Mapster;
using MasLazu.AspNet.Framework.Application.Exceptions;
using MasLazu.AspNet.Verification.Abstraction.Models;
using MasLazu.AspNet.Verification.Abstraction.Interfaces;

namespace MasLazu.AspNet.Authentication.Core.Base.Services;

public class UserService : CrudService<User, UserDto, CreateUserRequest, UpdateUserRequest>, IUserService
{
    private readonly IVerificationService _verificationService;

    public UserService(
        IRepository<User> repository,
        IReadRepository<User> readRepository,
        IUnitOfWork unitOfWork,
        IEntityPropertyMap<User> propertyMap,
        IPaginationValidator<User> paginationValidator,
        ICursorPaginationValidator<User> cursorPaginationValidator,
        IVerificationService verificationService,
        IValidator<CreateUserRequest>? createValidator = null,
        IValidator<UpdateUserRequest>? updateValidator = null)
        : base(repository, readRepository, unitOfWork, propertyMap, paginationValidator, cursorPaginationValidator, createValidator, updateValidator)
    {
        _verificationService = verificationService;
    }

    public async Task<UserDto?> GetByUsernameOrEmailAsync(string usernameOrEmail, CancellationToken ct = default)
    {
        return (await ReadRepository.FirstOrDefaultAsync(u => u.Username == usernameOrEmail || u.Email == usernameOrEmail, ct))?.Adapt<UserDto>();
    }

    public async Task<bool> IsEmailTakenAsync(string email, CancellationToken ct = default)
    {
        return await ReadRepository.ExistsAsync(u => u.Email == email, ct);
    }

    public async Task<bool> IsUsernameTakenAsync(string username, CancellationToken ct = default)
    {
        return await ReadRepository.ExistsAsync(u => u.Username == username, ct);
    }

    public async Task VerifyEmailAsync(Guid userId, CancellationToken ct = default)
    {
        User user = await Repository.GetByIdAsync(userId, ct) ??
            throw new NotFoundException(nameof(User), $"User with ID {userId} not found");

        user.IsEmailVerified = true;

        await Repository.UpdateAsync(user, ct);
        await UnitOfWork.SaveChangesAsync(ct);
    }

    public async Task SendEmailVerificationAsync(string email, CancellationToken ct = default)
    {
        User user = await Repository.FirstOrDefaultAsync(u => u.Email == email, ct) ??
            throw new NotFoundException(nameof(User), $"User with email {email} not found");

        if (user.Email is null)
        {
            throw new BadRequestException("User does not have an email to verify.");
        }

        var sendVerificationRequest = new SendVerificationRequest(
            UserId: user.Id,
            Destination: user.Email,
            PurposeCode: "email_verification"
        );
        await _verificationService.SendVerificationAsync(user.Id, sendVerificationRequest, ct);
    }
}
