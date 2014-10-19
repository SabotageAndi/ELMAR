using System;

namespace net.the_engineers.elmar.everywhere.requests
{
    public class WeatherForecastRequest
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int NumberOfSlices { get; set; }

        public Guid? LocationId { get; set; }
        public Guid? LocationNameId { get; set; }
    }
}
