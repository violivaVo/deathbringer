using System;
using System.IO;
using System.Reflection;
using iCubed.Ragnarok.Api.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;

namespace DeathBringer.Api
{
    public class Startup
    {
        /// <summary>
        /// Configurazione di ASP.NET
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Application name
        /// </summary>
        public string ApplicationName { get; }

        /// <summary>
        /// Application version
        /// </summary>
        public string ApplicationVersion { get; }

        /// <summary>
        /// Construttore e injection di IConfiguration
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            //Traccio la configurazione
            Configuration = configuration;

            //Assegno la configurazione locale
            Configuration = configuration;

            //Definizione del nome e versione del sistema
            ApplicationName = Assembly.GetEntryAssembly().GetName().Name;
            ApplicationVersion = $"v{Assembly.GetEntryAssembly().GetName().Version.Major}" +
                                 $".{Assembly.GetEntryAssembly().GetName().Version.Minor}" +
                                 $".{Assembly.GetEntryAssembly().GetName().Version.Build}";
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //Abilitazione CORS
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            //Aggiungo l'autentications basic e il default di schema
            services
                .AddAuthentication(o => o.DefaultScheme = BasicAuthenticationOptions.Scheme)
                .AddBasicAuthentication();

            //Aggiungo MVC
            services.AddMvc();

            //Registro il generatore di Swagger
            services.AddSwaggerGen(c =>
            {
                //Informazioni di testata
                c.SwaggerDoc("v1", new Info
                {
                    Title = ApplicationName,
                    Version = ApplicationVersion
                });

                //Compongo il percorso del file XML
                string file = $"{typeof(Startup).Assembly.GetName().Name}.xml";
                string xmlPath = Path.Combine(PlatformServices
                    .Default.Application.ApplicationBasePath, file);

                //Includo i commenti XML
                c.IncludeXmlComments(xmlPath);
            });
        }

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
            app.UseCors("CorsPolicy");

            //Utilizzo l'autenticazione
            app.UseAuthentication();

            //Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(
                    "/swagger/v1/swagger.json", 
                    $"{ApplicationName} {ApplicationVersion}");
            });

            //Utilizzo il pattern MVC
            app.UseMvc();
        }
    }
}
