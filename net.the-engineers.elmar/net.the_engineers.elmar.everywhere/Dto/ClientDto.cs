namespace net.the_engineers.elmar.everywhere.Dto
{
    public class ClientDto : DtoEntity 
    {
        public string Name { get; set; }
        public string ConnectionId { get; set; }
        public string Functions { get; set; }
        public ClientType Type { get; set; }
    }

    public class LoginClientDto : ClientDto
    {
        
    }

    public class LogoutClientDto : ClientDto
    {}
}