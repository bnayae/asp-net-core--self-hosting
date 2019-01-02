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
using System.Threading.Tasks;

namespace WinServiceHost
{
    public partial class Host : ServiceBase
    {
        private const string URL = "http://localhost:5002";

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
            OnStart(args);
            OnStop();
            Dispose();
        }

        protected override void OnStart(string[] args)
        {
            _host = BuildWebHost(args);
            _host.Run();
        }

        protected override void OnStop()
        {
            _host.Dispose();
            _host = null;
        }
    }
}
