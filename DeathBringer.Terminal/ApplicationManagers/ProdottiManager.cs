using DeathBringer.Core.ServiceLayers;
using DeathBringer.Terminal.BaseClasses;
using DeathBringer.Terminal.Data;
using DeathBringer.Terminal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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
                Id = GeneratoreId.GeneraNuovoIdentificatore<Prodotto>(ApplicationStorage.Prodotti),
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

        internal static void ElencoProdotti()
        {
            //recupera quelli in memoria
            var prodottiRecuperatiDallaMemoria = ApplicationStorage.Prodotti;
            // cicla sulle categorie in memoria
            for (int i = 0; i < prodottiRecuperatiDallaMemoria.Count; i++)
            {
                // Visualizza dati Utente
                Console.WriteLine(
                    $"nome: {prodottiRecuperatiDallaMemoria[i].Nome}, " +    //accedo all'i-esimo elemento della lista, e prendo il suo Nome
                    $"descrizione: {prodottiRecuperatiDallaMemoria[i].Descrizione}, "
                    );
            }
        }

        internal static void EliminaProdotto()
        {
            Console.WriteLine("[ ---------------------------- ]");
            Console.WriteLine("[ Elimina prodotto esistente ]");

            Console.Write("Inserisci id prodotto da eliminare: ");
            Prodotto prodottoDaProcessare = ReadProdottoFromConsole();

            //Rimozione della categoria dalla lista
            if (prodottoDaProcessare == null)
            {
                return;
            }
            else
            {
                ApplicationStorage.Prodotti.Remove(prodottoDaProcessare);
                Console.WriteLine("Il prodotto è stato cancellato!!!");
            }
        }

        internal static void ModificaProdotto()
        {
            Console.WriteLine("[ ---------------------------- ]");
            Console.WriteLine("[ Modifica prodotto esistente ]");

            Console.Write("Inserisci id prodotto da modificare: ");
            Prodotto prodottoDaProcessare = ReadProdottoFromConsole();

            if (prodottoDaProcessare == null)
            {
                return;
            }
            else
            {
                //Richiedo il nuovo nome
                Console.Write(" => nuovo nome: ");
                var nuovoNome = Console.ReadLine();
                Console.Write(" => nuovo descr: ");
                var nuovaDescrizione = Console.ReadLine();
                

                //Assegnamento ad oggetto esistente
                prodottoDaProcessare.Nome = nuovoNome;
                prodottoDaProcessare.Descrizione = nuovaDescrizione;

                Console.WriteLine("La modifica è stata completata!");
            }
        }

        private static Prodotto ReadProdottoFromConsole()
        {
            var id = Console.ReadLine();
            int idIntero;

            if (!int.TryParse(id, out idIntero))
            {
                Console.WriteLine("Il valore inserito non è valido!");
                return null;
            }

            else
            {  
                Prodotto prodottoEsistente = ApplicationStorage.Prodotti
                    .SingleOrDefault(prodottoCorrente => prodottoCorrente.Id == idIntero);

                if (prodottoEsistente == null)
                {
                    Console.WriteLine("Il prodotto selezionato non esiste!");
                    return null;
                }
                else
                {
                    Console.WriteLine("Il prodotto selezionato esiste!");
                    return prodottoEsistente;
                }
            }

        }
    }
}


