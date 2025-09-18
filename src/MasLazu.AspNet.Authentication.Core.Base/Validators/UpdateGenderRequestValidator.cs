using FluentValidation;
using MasLazu.AspNet.Authentication.Core.Abstraction.Models;

namespace MasLazu.AspNet.Authentication.Core.Base.Validators;

public class UpdateGenderRequestValidator : AbstractValidator<UpdateGenderRequest>
{
    public UpdateGenderRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Code)
            .NotEmpty()
            .MaximumLength(10)
            .When(x => !string.IsNullOrEmpty(x.Code));

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100)
            .When(x => !string.IsNullOrEmpty(x.Name));
    }
}
