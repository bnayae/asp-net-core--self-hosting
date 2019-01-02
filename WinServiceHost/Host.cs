using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WinServiceHost
{
    public partial class Host : ServiceBase
    {
        private const string URL = "http://localhost:5002";
        private bool _consoleMode;
        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        private static IWebHost BuildWebHost(string[] args)
        {
            var host = WebHost.CreateDefaultBuilder(args).UseUrls(URL)
                   .UseStartup<Web.Startup>()
                   //.UseStartup<Startup>()
                   .Build();
            return host;
        }

        private IWebHost _host;

        public Host()
        {
            InitializeComponent();
        }

        internal void RunAsConsole(string[] args)
        {
            _consoleMode = true;
            OnStart(args);
            OnStop();
            Dispose();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                //EventLog.WriteEvent("Self-Hosting", new EventInstance(0, 0, EventLogEntryType.Information), new string[] { "Service start successfully." });
                _host = BuildWebHost(args);
                if (_consoleMode)
                    _host.Run();
                else
                {
                    Task _ = _host.RunAsync(cancellationTokenSource.Token);
                }

            }
            catch (Exception ex)
            {
                //EventLog.WriteEvent("Self-Hosting", new EventInstance(0, 0, EventLogEntryType.Error), new string[] { "Service failed at startup." });
            }
        }

        protected override void OnStop()
        {
            cancellationTokenSource.Cancel();
            _host.Dispose();
            _host = null;
        }
    }
}
