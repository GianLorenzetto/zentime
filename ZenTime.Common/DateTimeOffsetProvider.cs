using System;

namespace ZenTime.Common
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