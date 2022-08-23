using BuberBreadkfast.WebApi.Providers;
using FluentValidation;

namespace BuberBreadkfast.WebApi.Validations.Validators;

public static class DateTimeValidators
{
    public static IRuleBuilderOptions<T, DateTime> AfterSunrise<T>(
        this IRuleBuilder<T, DateTime> ruleBuilder, IDateTimeProvider dateTimeProvider)
    {
        var sunrise = dateTimeProvider.Midnight.AddHours(6);

        return ruleBuilder
            .Must((objectRoot, dateTime, context) =>
            {
                var providedTime = TimeOnly.FromDateTime(dateTime);

                context.MessageFormatter.AppendArgument("Sunrise", sunrise);
                context.MessageFormatter.AppendArgument("ProvidedTime", providedTime);

                return providedTime > sunrise;
            })
            .WithMessage("{PropertyName} must be after sunrise {Sunrise}. You provided {ProvidedTime}.");
    }
}