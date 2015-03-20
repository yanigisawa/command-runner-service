﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace CommandRunner
{
    public partial class CommandService : ServiceBase
    {

        private static string _commandToExecute = ConfigurationManager.AppSettings["CommandToExecute"].ToString();

        public int SecondsBetweenExecution
        {
            get
            {
                int seconds;
                if (!int.TryParse(ConfigurationManager.AppSettings["SecondsBetweenExecution"], out seconds))
                {
                    seconds = -1;
                }

                return seconds;
            }
        }

        public CommandService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            var runner = new Command(_commandToExecute, SecondsBetweenExecution);
            runner.BeginExecution();
        }

        protected override void OnStop()
        {
        }
    }
}
