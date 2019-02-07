using DeathBringer.Terminal.BaseClasses;
using DeathBringer.Terminal.Data;
using DeathBringer.Terminal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DeathBringer.Terminal.ApplicationManagers
{
    class CreazioneProdotto
    {
        private static Categoria categoriaClasse;

        private static void CreaProdotto()
        {
            Console.WriteLine("Creazione nuovo prodotto");
            Console.WriteLine(" => nome : ");
            var nome = Console.ReadLine();
            Console.WriteLine(" => categoria : ");
            var categString = Console.ReadLine();
            int m = ApplicationStorage.Utente.Count;
            for (int i = 0; i < m; i++)
            {
                string category = ApplicationStorage.Utente[i].Nome;
                Categoria categoriaClasse = new Categoria();
                if (category == categString) categoriaClasse = ApplicationStorage.Utente[i];
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

            private static int GeneraNuovoIdentificatore() //non è void perché questa funz. a diff. delle altre mi ritorna qualcosa, un int
        {
            //verifico quanti ce ne sono in archivio
            var elementiEsistenti = ApplicationStorage.Utente.Count;
            //se non ne ho, il valore base è 1
            if (elementiEsistenti == 0)
            {
                return 1;
            }
            else
            {   //devo cercare l'elemento con Id maggiore
                int idMaggiore = 0;
                for (var i = 0; i < ApplicationStorage.Utente.Count; i++)
                {
                    if (ApplicationStorage.Utente[i].Id > idMaggiore)
                    {
                        idMaggiore = ApplicationStorage.Utente[i].Id;
                    }

                }

                return idMaggiore + 1;
                // al posto del for qui sopra avrei potuto mette -> idMaggiore = ApplicationStorage.Categorie.Max(elementiEsistenti e => e.Id); (AVENDO MESSO SYSTEM LINQ)
                //questa cosa usa LINQ (??)
            }
        }

    }
}
