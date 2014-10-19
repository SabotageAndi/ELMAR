using System;

namespace net.the_engineers.elmar.everywhere.Dto
{
    public class PersonLocationDto : DtoEntity
    {
        public DateTimeOffset Timestamp { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public double Altitude { get; set; }

        public Guid PersonId { get; set; }
    }
}
