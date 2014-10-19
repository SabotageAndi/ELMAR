using System.Collections.Generic;

namespace net.the_engineers.elmar.everywhere.Dto
{
    public class PhotoAlbumDto : DtoEntity
    {
        public string Name { get; set; }
        public List<PhotoDto> Photos { get; set; }
    }
}