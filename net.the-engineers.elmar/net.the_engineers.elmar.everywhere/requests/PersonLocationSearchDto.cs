using System;

namespace net.the_engineers.elmar.everywhere.requests
{
    public class PersonLocationSearchDto
    {
        public Guid? PersonId { get; set; }
        public DateTimeOffset? From { get; set; }
        public DateTimeOffset? To { get; set; }
    }
}
