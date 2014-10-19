using System.Collections.Generic;

namespace net.the_engineers.elmar.everywhere.Dto
{
    public class SongDto : DtoEntity
    {
        public string Name { get; set; }
        public uint Tracknumber { get; set; }
        public string Genres { get; set; }

        public List<FileDto> Files { get; set; }
        public MusicAlbumDto MusicAlbum { get; set; }
        public PersonGroupDto Artist { get; set; }
    }
}
