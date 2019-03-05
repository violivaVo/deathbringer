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
            //*** Configurazione dei repositories con EntityFramework
            //DependencyInjectionContainer.Register<IUtenteRepository, EntityFrameworkUtenteRepository>();
            //DependencyInjectionContainer.Register<ICategoriaRepository, EntityFrameworkCategoriaRepository>();

            //*** Configurazione dei repositories con Mock
            DependencyInjectionContainer.Register<IUtenteRepository, MockUtenteRepository>();
            DependencyInjectionContainer.Register<ICategoriaRepository, MockCategoriaRepository>();

            //Avvio dell'host
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
