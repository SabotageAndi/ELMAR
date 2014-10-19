using System;

namespace net.the_engineers.elmar.everywhere.net45
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
        DateTimeOffset UtcNowOffset { get; }
    }

    public class SystemDateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow
        {
            get { return DateTime.UtcNow; }
        }

        public DateTimeOffset UtcNowOffset
        {
            get { return DateTimeOffset.UtcNow; }
        }
    }
}