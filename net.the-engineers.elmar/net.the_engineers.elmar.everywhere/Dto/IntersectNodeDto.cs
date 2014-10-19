namespace net.the_engineers.elmar.everywhere.Dto
{
    public class IntersectNodeDto : DtoEntity 
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public bool AlwaysConnected { get; set; }
        public bool IsConnected { get; set; }
    }
}
