using DeathBringer.Core.ServiceLayers;
using System;
using System.Threading;

namespace DeathBringer.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Inizializzazione del layer applicativo
            Console.WriteLine("Inizializzazione del layer applicativo...");
            ApplicationServiceLayer layer = new ApplicationServiceLayer();

            //Generatore di un elemento random
            Console.WriteLine("Inizializzazione del random...");
            Random random = new Random();

            //Lista dei nomi di categoria
            var nomi = new string[] { "Frutta", "Verdura", "Libri", "Elettronica", "Consumabili", "Televisori", "Notebook", "Smartphones" };

            //Iterazione a ciclo continuo
            while (true)
            {
                //Randomizzazione di un nome di categoria
                Console.WriteLine("Selezione random di un nome");
                var nomeRandom = nomi[random.Next(nomi.Length - 1)];

                //Creazione di una categoria
                Console.WriteLine("Creazione di una categoria....");
                var validations = layer.InsertCategoria(nomeRandom, "questa non è importante");

                //Se ho validazioni fallite, esco
                if (validations.Count > 0)
                {
                    //Segnalazione ed uscita
                    Console.WriteLine("Validazione errata: uscita!");
                    return;
                }

                //Conferma 
                Console.WriteLine("Categoria inserita! Sleeping per 5 secondi...");
                Thread.Sleep(5000);
            }
        }
    }
}
