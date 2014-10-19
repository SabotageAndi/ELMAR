using System;

namespace net.the_engineers.elmar.everywhere.requests
{
    public class WeatherSearchRequest
    {
        public Guid? LocationId { get; set; }
        public Guid? LocationNameId { get; set; }
        public DateTimeOffset? From { get; set; }
        public DateTimeOffset? To { get; set; }
        public DateTimeOffset? DateTime { get; set; }
    }
}
