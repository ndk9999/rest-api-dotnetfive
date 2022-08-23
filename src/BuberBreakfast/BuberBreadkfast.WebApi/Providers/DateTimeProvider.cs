namespace BuberBreadkfast.WebApi.Providers;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;

    public TimeOnly Midnight => TimeOnly.MinValue;
}