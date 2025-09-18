using FluentValidation;
using MasLazu.AspNet.Authentication.Core.Abstraction.Models;

namespace MasLazu.AspNet.Authentication.Core.Base.Validators;

public class UpdateTimezoneRequestValidator : AbstractValidator<UpdateTimezoneRequest>
{
    public UpdateTimezoneRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Identifier)
            .NotEmpty()
            .MaximumLength(100)
            .When(x => !string.IsNullOrEmpty(x.Identifier));

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100)
            .When(x => !string.IsNullOrEmpty(x.Name));

        RuleFor(x => x.OffsetMinutes)
            .InclusiveBetween(-720, 840)
            .When(x => x.OffsetMinutes.HasValue);
    }
}
