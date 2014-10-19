namespace net.the_engineers.elmar.everywhere.Dto
{
    public class LocationDto : DtoEntity
    {
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public bool LoadWeather { get; set; }
        public string Name { get; set; }
    }
}
