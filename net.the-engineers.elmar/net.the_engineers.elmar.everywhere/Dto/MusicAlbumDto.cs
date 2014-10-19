using System.Collections.Generic;

namespace net.the_engineers.elmar.everywhere.Dto
{
    public class MusicAlbumDto : DtoEntity
    {
        public string Name { get; set; }
        public List<SongDto> Songs { get; set; }
    }
}
