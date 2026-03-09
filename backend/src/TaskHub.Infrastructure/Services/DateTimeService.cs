using System;
using TaskHub.Application.Common.Interfaces;

namespace TaskHub.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.UtcNow;
}
