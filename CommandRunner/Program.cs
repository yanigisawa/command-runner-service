using System;
using System.Configuration;
using System.Diagnostics;
using System.ServiceProcess;

namespace CommandRunner
{
    static class Program
    {
        private static string _commandToExecute = ConfigurationManager.AppSettings["CommandToExecute"].ToString();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            if (args.Length > 0 && args[0] == "--interactive")
            {
                var consoleTrace = new ConsoleTraceListener();

                Log.TextTraceSource.Listeners.Add(consoleTrace);

                Log.Information("Constructing Runner");
                var runner = new RunCommands(_commandToExecute, 1);

                Log.Information("Beginning Execution");
                runner.BeginExecution();

                Console.WriteLine("press enter...");
                Console.ReadLine();
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] 
                { 
                    new Service1() 
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
