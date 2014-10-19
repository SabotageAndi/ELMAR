using System;

namespace net.the_engineers.elmar.everywhere.Dto
{
    public class ClientLocationDto : DtoEntity 
    {
        public string ClientName { get; set; }
        public DateTime Timestamp { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public double Altitude { get; set; }
    }
}
