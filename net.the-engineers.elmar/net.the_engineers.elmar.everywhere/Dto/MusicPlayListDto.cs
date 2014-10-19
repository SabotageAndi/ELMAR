using System.Collections.Generic;

namespace net.the_engineers.elmar.everywhere.Dto
{
    public class MusicPlayListDto : DtoEntity 
    {
        public string Name { get; set; }
        public List<MusicPlayListEntryDto> Entries { get; set; }

        public MusicPlayListEntryDto LastEntry { get; set; }
    }
}
