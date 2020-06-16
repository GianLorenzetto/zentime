using System;

namespace ZenTime.Api.Services
{
    public interface IDateTimeOffsetProvider
    {
        DateTimeOffset UtcNow { get; }
    }
    
    public class DateTimeOffsetProvider : IDateTimeOffsetProvider
    {
        public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
    }
}