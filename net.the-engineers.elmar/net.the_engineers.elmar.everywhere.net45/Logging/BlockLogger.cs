using System;
using System.Runtime.CompilerServices;

namespace net.the_engineers.elmar.everywhere.net45.Logging
{
    public class BlockLogger : IDisposable
    {
        private readonly string _methodName;

        private readonly string _text;

        public BlockLogger(string text = null, [CallerMemberName] string methodName = null)
        {
            _text = text;
            _methodName = methodName;
            Loging.DebugFormat("{0} {1} begin", _methodName, _text);
        }


        public void Dispose()
        {
            Loging.DebugFormat("{0} {1} end", _methodName, _text);
        }
    }
}