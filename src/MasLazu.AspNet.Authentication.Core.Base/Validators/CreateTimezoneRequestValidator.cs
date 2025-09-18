using FluentValidation;
using MasLazu.AspNet.Authentication.Core.Abstraction.Models;

namespace MasLazu.AspNet.Authentication.Core.Base.Validators;

public class CreateTimezoneRequestValidator : AbstractValidator<CreateTimezoneRequest>
{
    public CreateTimezoneRequestValidator()
    {
        RuleFor(x => x.Identifier)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.OffsetMinutes)
            .InclusiveBetween(-720, 840); // -12 to +14 hours
    }
}
