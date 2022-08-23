using BuberBreadkfast.Contracts.Breakfast;
using FluentValidation;

namespace BuberBreadkfast.WebApi.Validations;

public class CreateBreakfastRequestSavoryItemsValidator : AbstractValidator<CreateBreakfastRequest>
{
    public CreateBreakfastRequestSavoryItemsValidator()
    {
        RuleForEach(x => x.Savory).NotEmpty();
    }
}