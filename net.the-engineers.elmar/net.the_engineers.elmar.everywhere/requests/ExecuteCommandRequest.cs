namespace net.the_engineers.elmar.everywhere.requests
{
    public class ExecuteCommandRequest
    {
        public int ClientId { get; set; }
        public string Command { get; set; }
        public string Parameter { get; set; }
        public int TimeToLive { get; set; }
    }
}
