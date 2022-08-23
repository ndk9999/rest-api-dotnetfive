namespace BuberBreadkfast.WebApi.Providers;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }

    TimeOnly Midnight { get; }
}