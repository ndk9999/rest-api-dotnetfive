using BuberDinner.Application.Common.Interfaces.Providers;

namespace BuberDinner.Infrastructure.Providers;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}