using System;
using System.Configuration;
using System.Diagnostics;
using System.ServiceProcess;
using Microsoft.VisualBasic;

namespace CommandRunner
{
    static class Program
    {
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
                var runner = new Command(CommandService.CommandToExecute, CommandService.SecondsBetweenExecution);

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
                    new CommandService() 
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
