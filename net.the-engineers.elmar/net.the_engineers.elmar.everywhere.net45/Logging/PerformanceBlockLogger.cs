using System;
using System.Runtime.CompilerServices;

namespace net.the_engineers.elmar.everywhere.net45.Logging
{
    public class PerformanceBlockLogger : IDisposable
    {
        [ThreadStatic]
        private static int _level = 0;

        private readonly Guid _blockId;
        private readonly string _methodName;

        private readonly DateTime _startTime;
        private readonly string _text;

        public PerformanceBlockLogger(string text = null, [CallerMemberName] string methodName = null)
        {
            _text = text;
            _methodName = methodName;
            _blockId = Guid.NewGuid();
            _startTime = DateTime.Now;

            _level++;
            var spaces = new string(' ', _level);

            Loging.PerformanceFormat("{0} {1} {2} {3}", spaces, _blockId, _methodName, _text);
        }

        public void Dispose()
        {
            var endTime = DateTime.Now;

            var duration = endTime - _startTime;

            var spaces = new string(' ', _level);

            Loging.PerformanceFormat("{0} {1} {2} {3} {4}", spaces, _blockId, _methodName, _text, duration.TotalSeconds);

            _level--;
        }
    }
}