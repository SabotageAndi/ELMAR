using System.Collections.Generic;

namespace net.the_engineers.elmar.everywhere.requests
{
    public class VoiceRequest
    {
        public List<string> SpokenText { get; set; }
        public string Language { get; set; }
    }
}
