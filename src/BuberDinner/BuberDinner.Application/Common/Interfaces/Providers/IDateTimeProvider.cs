namespace BuberDinner.Application.Common.Interfaces.Providers;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}