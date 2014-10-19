using System;
using System.Collections.Generic;

namespace net.the_engineers.elmar.everywhere.Dto
{
    public class MessageDto : DtoEntity
    {
        public MessageDto()
        {
            To = new List<PersonDto>();
            Cc = new List<PersonDto>();
            Bcc = new List<PersonDto>();
            Attachments = new List<FileDto>();
        }

        public string MessageId { get; set; }
        public PersonDto From { get; set; }
        public List<PersonDto> To { get; set; }
        public List<PersonDto> Cc { get; set; }
        public List<PersonDto> Bcc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTimeOffset ReceiveTimestamp { get; set; }
        public List<FileDto> Attachments { get; set; }
    }
}