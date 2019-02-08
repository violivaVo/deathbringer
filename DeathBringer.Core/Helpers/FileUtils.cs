using DeathBringer.Terminal.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DeathBringer.Core.Helpers
{
    /// <summary>
    /// Contiene utilità per i files
    /// </summary>
    public static class FileUtils
    {
        /// <summary>
        /// Compone il nome e percorso del file database sulla base dell'entità
        /// </summary>
        /// <typeparam name="TEntity">Tipo dell'entità</typeparam>
        /// <returns>Ritorna il percorso</returns>
        public static string ComposeFileName<TEntity>()
            where TEntity: IEntity
        {
            //Recupero il nome dell'entità di cui si vuole il file
            var entityName = typeof(TEntity).Name.ToLower();

            //Calcolo del percorso del Desktop
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            //Creazione nome file
            var file = $"deathbringer-{entityName}.json";

            //Composizione del file
            return Path.Combine(path, file);
        }
    }
}
