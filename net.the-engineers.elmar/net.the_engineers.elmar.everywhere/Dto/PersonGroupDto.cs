using System.Collections.Generic;

namespace net.the_engineers.elmar.everywhere.Dto
{
    public class PersonGroupDto : DtoEntity
    {
        public string Name { get; set; }
        public PersonGroupType GroupType { get; set; }

        public List<MusicAlbumDto> MusicAlbums { get; set; }
    }
}
