using System;

namespace net.the_engineers.elmar.everywhere.Dto
{
    public class ActivityDto : DtoEntity
    {
        public DateTimeOffset Timestamp { get; set; }
        public string Title { get; set; }
        public EntityType Type { get; set; }
    }
}