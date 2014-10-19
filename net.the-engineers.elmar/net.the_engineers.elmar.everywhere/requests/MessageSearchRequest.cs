using System;

namespace net.the_engineers.elmar.everywhere.requests
{
    public class MessageSearchRequest
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTimeOffset? ReceiveTimestampStart { get; set; }
        public DateTimeOffset? ReceiveTimestampEnd { get; set; }
        public string EMail { get; set; }

        public Guid? PersonId { get; set; }
        public int? Count { get; set; }

        public bool? OnlyMine { get; set; }
    }
}
