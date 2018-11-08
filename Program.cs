using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace SystemSupportingMSE
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                 // .UseUrls("http://localhost:5000", "https://localhost:5001", "http://192.168.1.109:5000", "https://192.168.1.109:5001")
                 .UseUrls("https://192.168.1.109:443")
                .UseStartup<Startup>();
    }
}
