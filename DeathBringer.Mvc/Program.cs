using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DeathBringer.Core.Data;
using DeathBringer.EntityFramework.Data.Repositories;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DeathBringer.Mvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DependencyInjectionContainer
                .Register<IUtenteRepository, EntityFrameworkUtenteRepository>();


            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
