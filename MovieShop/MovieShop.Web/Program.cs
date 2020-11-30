using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MovieShop.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
    
    // Kestrel Server
    // Main method is the entry point which will create a hosting environment so that ASP.NET Core can work inside that one.
    // Middleware is new in ASP.NET Core
    // When make a request in ASP.net, the request will go through some middleware.
    // has built-in middlewares
    // we can create own middleware and plugin to pipeline.
