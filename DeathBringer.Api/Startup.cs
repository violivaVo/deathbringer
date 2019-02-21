using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iCubed.Ragnarok.Api.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DeathBringer.Api
{
    public class Startup
    {
        /// <summary>
        /// Configurazione di ASP.NET
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Construttore e injection di IConfiguration
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            //Ttaccio la configurazione
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ////Abilitazione CORS
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("CorsPolicy",
            //        builder => builder.AllowAnyOrigin()
            //            .AllowAnyMethod()
            //            .AllowAnyHeader()
            //            .AllowCredentials());
            //});

            //Aggiungo l'autentications basic e il default di schema
            services
                .AddAuthentication(o => o.DefaultScheme = BasicAuthenticationOptions.Scheme)
                .AddBasicAuthentication();

            //Aggiungo MVC
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //Validazione argomenti
            if (app == null) throw new ArgumentNullException(nameof(app));
            if (env == null) throw new ArgumentNullException(nameof(env));

            //Se siamo in modalità "dev"
            if (env.IsDevelopment())
            {
                //Abilito la pagina delle eccezioni
                app.UseDeveloperExceptionPage();
            }

            //Abilito CORS
            //app.UseCors("CorsPolicy");

            //Utilizzo l'autenticazione
            app.UseAuthentication();

            //Utilizzo il pattern MVC
            app.UseMvc();
        }
    }
}
