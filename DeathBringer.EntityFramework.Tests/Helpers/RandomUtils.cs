using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeathBringer.EntityFramework.Tests.Helpers
{
    /// <summary>
    /// Contiene 
    /// </summary>
    public class RandomUtils
    {
        private static Random _Random = new Random();

        /// <summary>
        /// Genera una stringa random
        /// </summary>
        /// <param name="digits">Numero di digits</param>
        /// <param name="random">Random seed (opzionale)</param>
        /// <returns>Ritorna una stringa</returns>
        public static string GenerateString(int digits, Random random = null)
        {
            //Validazione argomenti
            if (digits <= 0) throw new ArgumentOutOfRangeException(nameof(digits));

            //Creo un oggetto random da inizializzare
            random = random ?? new Random();

            //Genero un buffer di appoggio e randomizzo
            byte[] buffer = new byte[digits / 2];
            random.NextBytes(buffer);

            //Eseguo la conversione del valore in esadecimale
            string result = string.Concat(buffer.Select(x => x.ToString("X2")).ToArray());

            //Se è del numero di cifre giusto, ritorno
            if (digits % 2 == 0)
                return result;

            //Se non è del numero di cifre giusto, aggiungo "0"
            return result + random.Next(16).ToString("X");
        }
    }
}
