using FluentValidation;
using MasLazu.AspNet.Authentication.Core.Abstraction.Models;

namespace MasLazu.AspNet.Authentication.Core.Base.Validators;

public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100)
            .When(x => !string.IsNullOrEmpty(x.Name));

        RuleFor(x => x.Email)
            .EmailAddress()
            .When(x => !string.IsNullOrEmpty(x.Email));

        RuleFor(x => x.PhoneNumber)
            .Matches(@"^\+?[1-9]\d{1,14}$")
            .When(x => !string.IsNullOrEmpty(x.PhoneNumber))
            .WithMessage("Invalid phone number format.");

        RuleFor(x => x.Username)
            .MaximumLength(50)
            .When(x => !string.IsNullOrEmpty(x.Username));

        RuleFor(x => x.LanguageCode)
            .MaximumLength(10)
            .When(x => !string.IsNullOrEmpty(x.LanguageCode));

        RuleFor(x => x.GenderCode)
            .MaximumLength(10)
            .When(x => !string.IsNullOrEmpty(x.GenderCode));
    }
}
