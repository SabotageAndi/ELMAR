using System;

namespace net.the_engineers.elmar.everywhere.requests
{
    public class EventSearchRequest
    {
        public string Title { get; set; }
        public DateTimeOffset? Start { get; set; }
        public DateTimeOffset? End { get; set; }
        public int? Count { get; set; }
        public Guid? Attendee { get; set; }

        public bool? OnlyMine { get; set; }
    }
}