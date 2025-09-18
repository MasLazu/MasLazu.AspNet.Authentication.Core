using FluentValidation;
using MasLazu.AspNet.Authentication.Core.Abstraction.Models;

namespace MasLazu.AspNet.Authentication.Core.Base.Validators;

public class UpdateLoginMethodRequestValidator : AbstractValidator<UpdateLoginMethodRequest>
{
    public UpdateLoginMethodRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Code)
            .NotEmpty()
            .MaximumLength(50)
            .When(x => !string.IsNullOrEmpty(x.Code));
    }
}
