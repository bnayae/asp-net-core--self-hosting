using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;

namespace Bnaya.Samples
{
    class Program
    {
        private const string URL = "http://localhost:5002";

        static void Main(string[] args)
        {
            BuildWebHost(args).Run();
            Console.WriteLine("Please enter any key to exit");
            Console.ReadKey();
        }
        private static IWebHost BuildWebHost(string[] args)
        {
            var host = WebHost.CreateDefaultBuilder(args).UseUrls(URL)
                   .UseStartup<Web.Startup>()
                   //.UseStartup<Startup>()
                   .Build();
            return host;
        }
    }
}