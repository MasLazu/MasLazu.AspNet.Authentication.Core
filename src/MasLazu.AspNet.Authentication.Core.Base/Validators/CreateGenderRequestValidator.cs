using FluentValidation;
using MasLazu.AspNet.Authentication.Core.Abstraction.Models;

namespace MasLazu.AspNet.Authentication.Core.Base.Validators;

public class CreateGenderRequestValidator : AbstractValidator<CreateGenderRequest>
{
    public CreateGenderRequestValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .MaximumLength(10);

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);
    }
}
