using System.Collections.Generic;

namespace net.the_engineers.elmar.everywhere.Dto
{
    public class UploadCalendarDto
    {
        public string ExternalId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Start { get; set; }

        public string End { get; set; }

        public string Location { get; set; }

        public List<UploadCalendarAttendes> Attendees { get; set; }

        public bool Deleted { get; set; }

        public class UploadCalendarAttendes
        {
            public string Email { get; set; }

            public string Name { get; set; }

            public string Type { get; set; }
        }
    }
}