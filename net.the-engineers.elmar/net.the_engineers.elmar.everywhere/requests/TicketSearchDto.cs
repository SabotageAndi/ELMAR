using System;

namespace net.the_engineers.elmar.everywhere.requests
{
    public class TicketSearchDto
    {
        public string TicketId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string Priority { get; set; }

        public string Status { get; set; }

        public string Substatus { get; set; }

        public DateTimeOffset? ChangedAtStart { get; set; }
        public DateTimeOffset? ChangedAtEnd { get; set; }

        public int? Count { get; set; }

        public bool? OnlyMine { get; set; }

        public Guid? PersonId { get; set; }
    }
}