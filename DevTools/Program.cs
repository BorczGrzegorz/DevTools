using DevTools;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;

namespace DevTools
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                   .ConfigureKestrel(x => x.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(3))
                   .UseStartup<Startup>();
    }
}
