using System;
using System.Diagnostics;

namespace CommandRunner
{
    public class Log
    {
        private static TraceSource _traceSource;
        private static string _traceSourceName = "CommandRunnerTraceSource";

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
            var dateFormattedMessage = string.Format("{0} - {1}", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"), message);


            for (int i = 0; i < _traceSource.Listeners.Count; i++)
            {
                var listener = _traceSource.Listeners[i];
                listener.WriteLine(dateFormattedMessage);
                listener.Flush();
            }
        }
    }
}
