using System.Collections.Generic;

namespace net.the_engineers.elmar.everywhere.Dto
{
    public class ProjectDto : DtoEntity
    {
        public string Name { get; set; }

        public List<TicketDto> Tickets { get; set; }
    }
}