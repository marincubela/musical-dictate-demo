using Application.Common.Interfaces;

namespace Persistence.Services;

public class DateService : IDateService
{
    public DateTime Now => DateTime.UtcNow;
}