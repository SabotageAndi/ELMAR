using System.Runtime.Serialization;

namespace net.the_engineers.elmar.everywhere.Dto
{
    public class FileDto : DtoEntity
    {
        public string Filename { get; set; }

        //[IgnoreDataMember]
        //public string Extension
        //{
        //    get
        //    {
        //        if (string.IsNullOrWhiteSpace(Filename))
        //            return string.Empty;

        //        var lastPoint = Filename.LastIndexOf(".", System.StringComparison.Ordinal);

        //        var extenstion = Filename.Substring(lastPoint);

        //        return extenstion;
        //    }
        //}
    }
}