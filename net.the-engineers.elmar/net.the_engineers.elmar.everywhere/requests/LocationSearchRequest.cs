namespace net.the_engineers.elmar.everywhere.requests
{
    public class LocationSearchRequest
    {
        public string Country { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public bool? LoadWeather { get; set; }

        public string City { get; set; }
    }
}
