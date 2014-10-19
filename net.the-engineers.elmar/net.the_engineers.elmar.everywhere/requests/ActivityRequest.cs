using System;

namespace net.the_engineers.elmar.everywhere.requests
{
    public class ActivityRequest
    {
        public int ActivitySinceSeconds { get; set; }
        public Guid? ForPerson { get; set; }
    }
}