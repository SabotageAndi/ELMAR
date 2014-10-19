using System.Collections.Generic;

namespace net.the_engineers.elmar.everywhere.Dto
{
    public class MeshDto : DtoEntity 
    {
        public string Name { get; set; }
        public List<DtoEntity> LinkedTo { get; set; }
        public string Text { get; set; }
    }
}
