using System;
using System.Collections.Generic;

namespace net.the_engineers.elmar.everywhere.Dto
{
    public class EventDto : DtoEntity
    {
        public EventDto()
        {
            Attendees = new List<PersonEventDto>();
        }

        public string Title { get; set; }
        public DateTimeOffset Start { get; set; }
        public DateTimeOffset End { get; set; }
        public string Description { get; set; }
        public string ExternalId { get; set; }
        public string Location { get; set; }

        public List<PersonEventDto> Attendees { get; set; }

        public string LocationUri { get; set; }
    }
}