namespace net.the_engineers.elmar.everywhere.Dto
{
    public class MusicPlayListEntryDto : DtoEntity 
    {
        public SongDto Song { get; set; }
        public FileDto File { get; set; }
        public int Position { get; set; }
    }
}
