using System;
using DeathBringer.Terminal.Entities;
using Microsoft.EntityFrameworkCore;

namespace Yred.Authentication.Relationals.Data.Contexts
{
    public class DeathBringerDbContext: DbContext
    {
        public DbSet<Utente> Utenti { get; set; }

        /// <summary>
        /// Raised during context configuration
        /// </summary>
        /// <param name="optionsBuilder">Options for builder</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //*** INSTRUCTIONS
            // Go on project folder where is located EntityFramework DbContext 
            // using command prompt (cmd or PowerShell) and type:
            //
            // > dotnet add package Microsoft.EntityFrameworkCore.Design
            // > dotnet restore
            //
            // If everything is installed correctly, you should see a confirm after
            // > dotnet ef
            //
            // Create an executable project (ex. .NET Core Console) that will be used 
            // as entry point for migrations (ex. DeathBringer.Terminal), then type:
            // 
            // > dotnet ef migrations add InitialMigration --startup-project ../DeathBringer.Terminal/DeathBringer.Terminal.csproj
            //
            // Generate SQL scripts using the following command: 
            //
            // > dotnet ef migrations script --startup-project ../DeathBringer.Terminal/DeathBringer.Terminal.csproj -o script.sql
            //
            // File "script.sql" will be generate on DeathBringer.Terminal folder

            //Arguments validation
            if (optionsBuilder == null) throw new ArgumentNullException(nameof(optionsBuilder));

            //Check is "Default connection string exists
            const string ConnectionString = "Server=tcp:maurobussini.database.windows.net,1433;" + 
                "Database=Kirey;User ID=AdminMauroBussini;Password=P@ssw0rd;"  + 
                "Trusted_Connection=False;Encrypt=True;Connection Timeout=30;MultipleActiveResultSets=true;";

            //Add SQL configuration
            optionsBuilder.UseSqlServer(ConnectionString);

            //Base configuration
            base.OnConfiguring(optionsBuilder);
        }

        /// <summary>
        /// Raised when model is going to be created
        /// </summary>
        /// <param name="modelBuilder">Model builder instance</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Mappo le entità
            modelBuilder.Entity<Utente>().ToTable("tabella_Utenti");
        }
    }
}
