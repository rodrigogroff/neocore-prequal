using Master.Entity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace Master
{
    public static class Program
    {
        public static int GetPort(string[] args)
        {
            int port = 59540; // post, put (59539), gets (59540)
            if (args != null && args.Length > 0) port = int.Parse(args[0]);
            return port;
        }

        public static string GetLocation(string[] args)
        {
            string location = "127.0.0.1";

#if RELEASE
            location = "0.0.0.0";
#endif
            if (args != null && args.Length > 1) location = args[1];
            return location;
        }
             
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
                webBuilder.UseKestrel((hostingContext, options) =>
                {

                    options.Listen(IPAddress.Parse(GetLocation(args)), GetPort(args), listenOptions => { });

                    /*
                    try
                    {
                        options.Listen(IPAddress.Parse(GetLocation(args)), GetPort(args), listenOptions =>
                        {
                            listenOptions.UseHttps("certificate.pfx", "gustavo123");
                        });
                    }
                    catch (System.Exception ex) 
                    {
                        Console.WriteLine (ex.ToString());
                    }
                    */

                });

#if RELEASE                
                webBuilder.ConfigureLogging((context, logging) => { logging.ClearProviders(); });
#endif
            });
    }
}
