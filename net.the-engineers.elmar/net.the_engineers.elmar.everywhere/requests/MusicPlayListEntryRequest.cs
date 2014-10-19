namespace net.the_engineers.elmar.everywhere.requests
{
    public class MusicPlayListEntryRequest
    {
        public int? Id { get; set; }
        public string PlayListName { get; set; }
        public string SongUri { get; set; }
        public string FileUri { get; set; }
        public int Position { get; set; }
    }
}
