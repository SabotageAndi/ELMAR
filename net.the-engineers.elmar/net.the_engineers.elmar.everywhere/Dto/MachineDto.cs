using System;

namespace net.the_engineers.elmar.everywhere.Dto
{
    public class MachineDto : DtoEntity
    {
        public string Hostname { get; set; }
        public string IPv4Address { get; set; }
        public string IPv6Address { get; set; }
        public DateTimeOffset LastSeen { get; set; }
    }
}
