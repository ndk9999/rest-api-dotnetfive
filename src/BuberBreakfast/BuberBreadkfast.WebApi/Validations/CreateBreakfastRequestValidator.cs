using BuberBreadkfast.Contracts.Breakfast;
using BuberBreadkfast.WebApi.Providers;
using BuberBreadkfast.WebApi.Validations.Validators;
using FluentValidation;

namespace BuberBreadkfast.WebApi.Validations;

public class CreateBreakfastRequestValidator : AbstractValidator<CreateBreakfastRequest>
{
    public CreateBreakfastRequestValidator(IDateTimeProvider dateTimeProvider)
    {
        // RuleFor(x => x.BreakfastDetails.Name).Length(3, 50);

        // RuleFor(x => x.StartDateTime).Must(x => x > dateTimeProvider.UtcNow);
        // RuleFor(x => x.EndDateTime).GreaterThan(x => x.StartDateTime);

        // RuleForEach(x => x.Savory)
        //     .Must(NotBeEmpty)
        //     .WithMessage("Savory must have at leat one item");

        // RuleFor(x => x.BreakfastDetails.StartDateTime).AfterSunrise(dateTimeProvider);

        Include(new CreateBreakfastRequestSavoryItemsValidator());
        RuleFor(x => x.BreakfastDetails).SetValidator(new BreakfastDetailsValidator(dateTimeProvider));
    }

    private static bool NotBeEmpty(string savoryItem)
    {
        return savoryItem.Length > 0;
    }
}