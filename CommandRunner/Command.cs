using System;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Time = System.Timers;

namespace CommandRunner
{
    public class Command
    {
        private string _scriptFile;
        private int _secondsOfDelay;

        public bool ProcessRunning { get; set; }

        public Command(string scriptFile, int secondsBetweenExecution)
        {
            if (!File.Exists(scriptFile)) { throw new FileNotFoundException(); }

            if (secondsBetweenExecution <= 0) { throw new ArgumentException("Number of Seconds must be a positive integer", "secondsOfDelay"); }

            _scriptFile = scriptFile;
            _secondsOfDelay = secondsBetweenExecution;
        }

        public void BeginExecution()
        {
            var t = new Time.Timer(_secondsOfDelay * 1000);
            t.Elapsed += CommandTimer_Elapsed;
            t.Enabled = true;
        }

        private void CommandTimer_Elapsed(object sender, Time.ElapsedEventArgs e)
        {
            if (ProcessRunning)
            {
                Log.Warning(string.Format("Command {0} took longer than {1} second(s) to complete. Execution will not begin until the previous process finishes", _scriptFile, _secondsOfDelay));
                return;
            }

            using (var p = new Process())
            {
                p.StartInfo.FileName = _scriptFile;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.FileName = _scriptFile;
                Log.Information(string.Format("Timer Elapsed, executing {0}", _scriptFile));
                p.Start();

                ProcessRunning = true;
                p.WaitForExit();
                ProcessRunning = false;

                // TODO: Don't log if below are empty strings
                Log.Information(p.StandardOutput.ReadToEnd());
                Log.Error(p.StandardError.ReadToEnd());

            }
        }

    }
}
