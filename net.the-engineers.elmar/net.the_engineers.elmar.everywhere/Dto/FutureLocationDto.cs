using System;

namespace net.the_engineers.elmar.everywhere.Dto
{
    public class FutureLocationDto : DtoEntity 
    {
        public DateTimeOffset StayingFrom { get; set; }
        public DateTimeOffset StayingTo { get; set; }
        public String LocationNameUri { get; set; }

    }
}
