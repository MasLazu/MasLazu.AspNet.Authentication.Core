using FluentValidation;
using MasLazu.AspNet.Authentication.Core.Abstraction.Models;

namespace MasLazu.AspNet.Authentication.Core.Base.Validators;

public class CreateLoginMethodRequestValidator : AbstractValidator<CreateLoginMethodRequest>
{
    public CreateLoginMethodRequestValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .MaximumLength(50);
    }
}
