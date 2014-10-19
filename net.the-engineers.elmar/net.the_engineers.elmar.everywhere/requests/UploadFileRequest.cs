namespace net.the_engineers.elmar.everywhere.requests
{
    public class UploadFileRequest
    {
        public string Path { get; set; }

        public string Filename { get; set; }

        public string UniqIdentifier { get; set; }

        public byte[] Data { get; set; }
    }
}