using System;
using log4net;
using log4net.Config;

namespace net.the_engineers.elmar.everywhere.net45.Logging
{
    public class Loging
    {
        private static ILog _log;
        private static ILog _performance;

        public static void Init(string loggerName)
        {
            XmlConfigurator.Configure();
            _log = LogManager.GetLogger(loggerName);
            _performance = LogManager.GetLogger("performance") ?? _log;
        }

        public static void Performance(string message)
        {
            _performance.Debug(message);
        }

        public static void PerformanceFormat(string format, params object[] arg)
        {
            _performance.DebugFormat(format, arg);
        }

        public static void Debug(object message, Exception exception)
        {
            _log.Debug(message, exception);
        }
        
        public static void Info(object message, Exception exception)
        {
            _log.Info(message, exception);
        }

        public static void Error(object message, Exception exception)
        {
            _log.Error(message, exception);
        }

        public static void Warn(object message, Exception exception)
        {
            _log.Warn(message, exception);
        }

        public static void Fatal(object message, Exception exception)
        {
            _log.Fatal(message, exception);
        }

        public static void DebugFormat(string format, params object[] arg)
        {
            _log.DebugFormat(format, arg);
        }

        public static void InfoFormat(string format, params object[] arg)
        {
            _log.InfoFormat(format, arg);
        }

        public static void ErrorFormat(string format, params object[] arg)
        {
            _log.ErrorFormat(format, arg);
        }

        public static void FatalFormat(string format, params object[] arg)
        {
            _log.FatalFormat(format, arg);
        }

        public static void WarnFormat(string format, params object[] arg)
        {
            _log.WarnFormat(format, arg);
        }

        public static void Info(string message)
        {
            _log.Info(message);
        }
    }
}
