using System;

namespace net.the_engineers.elmar.everywhere.net45
{
    public interface IGuidGenerator
    {
        Guid Generate();
    }

    public class GuidGenerator : IGuidGenerator
    {
        public Guid Generate()
        {
            return Guid.NewGuid();
        }
    }
}