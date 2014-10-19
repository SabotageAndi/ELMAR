using System.Collections.Generic;
using System.Linq;

namespace net.the_engineers.elmar.everywhere.Dto
{
    public class PhotoDto : DtoEntity
    {
        public List<PhotoFileDto> Files { get; set; }

        public FileDto GetQualityFile(Quality quality)
        {
            var elmarPhotoFileDto = Files.Where(i => i.Quality == quality).FirstOrDefault();

            if (elmarPhotoFileDto == null)
                return Files.First().File;

            return elmarPhotoFileDto.File;
        }
    }
}