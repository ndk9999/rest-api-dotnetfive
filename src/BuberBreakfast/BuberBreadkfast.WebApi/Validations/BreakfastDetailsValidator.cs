using BuberBreadkfast.Contracts.Breakfast.Common;
using BuberBreadkfast.WebApi.Providers;
using FluentValidation;

namespace BuberBreadkfast.WebApi.Validations;

public class BreakfastDetailsValidator : AbstractValidator<BreakfastDetails>
{
    public BreakfastDetailsValidator(IDateTimeProvider dateTimeProvider)
    {
        RuleFor(x => x.Name).NotEmpty().Length(3, 50);
        RuleFor(x => x.Description).NotEmpty();

        RuleFor(x => x.StartDateTime).Must(x => x > dateTimeProvider.UtcNow);
        RuleFor(x => x.EndDateTime).GreaterThan(x => x.StartDateTime);
    }
}