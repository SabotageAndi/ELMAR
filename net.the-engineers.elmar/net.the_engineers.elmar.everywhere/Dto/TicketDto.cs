using System;
using System.Collections.Generic;

namespace net.the_engineers.elmar.everywhere.Dto
{
    public class TicketDto : DtoEntity
    {
        public string TicketId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Substatus { get; set; }
        public string Priority { get; set; }
        public string Comment { get; set; }
        public DateTimeOffset? ChangedAt { get; set; }

        public PersonDto AssignedTo { get; set; }
        public PersonDto CreatedTicket { get; set; }

        public List<PersonDto> ChangedTicket { get; set; }

        public List<TicketHistoryEntry> HistoryEntries { get; set; }

        public List<FileDto> Files { get; set; }
    }
}