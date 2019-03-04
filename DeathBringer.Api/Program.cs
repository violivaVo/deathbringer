using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DeathBringer.Core.Data;
using DeathBringer.Core.Mocks;
using DeathBringer.EntityFramework.Data.Repositories;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DeathBringer.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //DependencyInjectionContainer
            //    .Register<IUtenteRepository, EntityFrameworkUtenteRepository>();
            DependencyInjectionContainer
                .Register<IUtenteRepository, MockUtenteRepository>();

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
