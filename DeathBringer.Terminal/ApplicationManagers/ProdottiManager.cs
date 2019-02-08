using DeathBringer.Core.ServiceLayers;
using DeathBringer.Terminal.BaseClasses;
using DeathBringer.Terminal.Data;
using DeathBringer.Terminal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeathBringer.Terminal.ApplicationManagers
{
    public class ProdottiManager
    {
        private static Categoria categoriaClasse;

        internal static void Indice()
        {
            string selection = null;
            Console.WriteLine("*********************");
            Console.WriteLine("Gestione dei Prodotti");
            Console.WriteLine("*********************");
            Console.WriteLine();
            Console.WriteLine("1 - Inserimento prodotto");
            Console.WriteLine("2 - Elenco prodotti");
            Console.WriteLine("3 - Elimina prodotto");
            Console.WriteLine("4 - Modifica prodotti");
            Console.WriteLine("Inserire 'exit' per chiudere il programma.");
            Console.WriteLine();
            Console.Write(" Opzione selezionata: ");
            selection = Console.ReadLine();

            while (selection != "exit")
            {
                switch (selection)
                {
                    case "1":
                        InserimentoProdotto();
                        break;
                    case "2":
                        ElencoProdotti();
                        break;
                    case "3":
                        EliminaProdotto();
                        break;
                    case "4":
                        ModificaProdotto();
                        break;
                    default:
                        Console.WriteLine("Scelta non valida !");
                        break;

                }

            }



        }
        internal static void InserimentoProdotto()
        {
            var Layer = new ProdottiServiceLayer();
            IList<Prodotto> prodotti = Layer.FetchProdotti();
            Console.WriteLine("Creazione nuovo prodotto");
            Console.WriteLine(" => nome : ");
            var nome = Console.ReadLine();
            Console.WriteLine(" => categoria : ");
            var categoryString = Console.ReadLine();
            int m = ApplicationStorage.Prodotti.Count;
            for (int i = 0; i < m; i++)
            {
                string category = ApplicationStorage.Categorie[i].Nome;
                Categoria categoriaClasse = new Categoria();
                if (category == categoryString)
                    categoriaClasse = ApplicationStorage.Categorie[i];
            };

            Console.WriteLine("Data Produzione: ");
            var dataProduzioneString = Console.ReadLine();
            DateTime dataProduzione = Convert.ToDateTime(dataProduzioneString);
            Console.WriteLine("Inserisci descrizione: ");
            string descrizione = Console.ReadLine();
            Console.WriteLine("Inserisci brand: ");
            string brand = Console.ReadLine();

            Prodotto nuovoProdotto = new Prodotto()
            {
                Id = GeneraNuovoIdentificatore(),
                Nome = nome,
                CategoriaAppartenenza = categoriaClasse,
                DataProduzione = dataProduzione,
                Descrizione = descrizione,
                Brand = brand
            };

            ApplicationStorage.Prodotti.Add(nuovoProdotto);

            Console.WriteLine($"Creato prodotto {nuovoProdotto.Nome}!"); //oppure concateni, ya know, ma conviene questo modo moderno
            Console.ReadLine();


        }

        private static int GeneraNuovoIdentificatore()
        {
            throw new NotImplementedException();
        }

        internal static void ElencoProdotti()
        {

        }
        internal static void EliminaProdotto()
        {

        }
        internal static void ModificaProdotto()
        {

        }
    }
}


