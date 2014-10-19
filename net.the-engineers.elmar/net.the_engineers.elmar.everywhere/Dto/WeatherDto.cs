using System;

namespace net.the_engineers.elmar.everywhere.Dto
{
    public class WeatherDto : DtoEntity
    {
        public Guid LocationId { get; set; }
        public DateTimeOffset From { get; set; }
        public DateTimeOffset To { get; set; }

        public decimal Temperature { get; set; }
        public decimal Humidity { get; set; }
        public decimal Rain { get; set; }
        public decimal WindSpeed { get; set; }
        public decimal WindDirection { get; set; }
        public decimal Clouds { get; set; }
        public decimal Pressure { get; set; }

        public WeatherStatus Status { get; set; }
    }
}
