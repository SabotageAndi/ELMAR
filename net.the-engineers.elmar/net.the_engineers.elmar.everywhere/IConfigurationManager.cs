namespace net.the_engineers.elmar.common
{
    public interface IConfigurationManager
    {
        string ElmarUri { get; set; }
        string ElmarUser { get; set; }
        string ElmarKey { get; set; }
        string ClientName { get; set; }
    }
}