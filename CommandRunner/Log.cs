using System;
using System.Diagnostics;

namespace CommandRunner
{
    public class Log
    {
        private static TraceSource _traceSource;
        private static string _traceSourceName = "_";
        private static int _traceId = 0;

        public static TraceSource TextTraceSource
        {
            get
            {
                if (_traceSource == null) { _traceSource = new TraceSource(_traceSourceName); }
                return _traceSource;
            }
        }

        public static void Information(string message)
        {
            WriteEvent(TraceEventType.Information, message);
        }

        public static void Warning(string message)
        {
            WriteEvent(TraceEventType.Warning, message);
        }

        public static void Error(string message)
        {
            WriteEvent(TraceEventType.Error, message);
        }

        private static void WriteEvent(TraceEventType eventType, string message)
        {
            _traceSource.TraceEvent(eventType, _traceId++, "{0} - {1}", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"), message);
        }
    }
}
