using DeathBringer.Core.ServiceLayers;
using DeathBringer.Terminal.BaseClasses;
using DeathBringer.Terminal.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeathBringer.Terminal.ApplicationManagers
{
    public class ProdottiManager
    {
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

            if (selection != "exit")
            {
                Indice();
            }



        }

        private static void ModificaProdotto()
        {
            
            
            
            Console.WriteLine("[ ---------------------------- ]");
            Console.WriteLine("[ Modifica Prodotto esistente ]");

            Console.Write("Inserisci id Prodotto da modificare: ");
            Prodotto prodottoDaProcessare = ReadProdottoFromConsole();

            //Se non esiste, non faccio null
            if (prodottoDaProcessare == null)
            {
                return;
            }
            else
            {
                //Richiedo il nuovo nomoe
                Console.Write(" => nuovo nome: ");
                var nuovoNome = Console.ReadLine();
                Console.Write(" => nuova desc: ");
                var nuovaDesc = Console.ReadLine();

                //Assegnamento ad oggetto esistente
               
                prodottoDaProcessare.Nome = nuovoNome;
                prodottoDaProcessare.Descrizione = nuovaDesc;
                
                ProdottiServiceLayer layer = new ProdottiServiceLayer();
                ApplicationStorage.SaveProdotti();
                Console.WriteLine("La modifica è stata fatta!");
            }
        }

        internal static void InserimentoProdotto()
        {

            Console.WriteLine("Creazione nuova Prodotto");
            // chiediamo il nome della categoria E DESCRIZIONE
            Console.WriteLine(" => nome : ");
            var nome = Console.ReadLine();
            Console.WriteLine(" => descrizione : ");
            var descr = Console.ReadLine();

            //Istanzio il layer di lavoro
            ProdottiServiceLayer layer = new ProdottiServiceLayer();

            //Aggancio l'evento di salvataggio della categoria e mostro a video
            //layer.CategorieSaved += (o, a) => { Console.WriteLine(a); };

            //Creazione categorie
            var validazioni = layer.InsertProdotto(nome, descr);

            //Se non ho errori, confermo
            if (validazioni.Count == 0)
            {
                //Visualizza conferma
                Console.WriteLine($"Creata prodotto {nome}!");
            }
            //Itero i messaggi di errore
            foreach (var current in validazioni)
                Console.WriteLine($"Errore: {current.ErrorMessage}");
            //Resto in attesa
            Console.ReadLine();
        }
        internal static void ElencoProdotti()
        {
            ProdottiServiceLayer layer = new ProdottiServiceLayer();
            var validazioni = layer.FetchProdotti();
            //recupera quelle in memoria
            var prodottoRecuperateDallaMemoria = ApplicationStorage.Prodotti;
            

            // cicla sulle prodotto in memoria

            for (int i = 0; i < prodottoRecuperateDallaMemoria.Count; i++) //considera che prodotto BLABLA è una lista, quindi ha un Count; inoltre il primo indice è 0, come in java
            {
                //visualizza nomi e descrizioni
                Console.WriteLine(
                    $"nome: {prodottoRecuperateDallaMemoria[i].Nome}, " +    //accedo all'i-esimo elemento della lista, e prendo il suo Nome
                    $"descr: {prodottoRecuperateDallaMemoria[i].Descrizione}, " + //accedo all'i-esimo elemento della lista, e prendo la sua Descrizione
                    $"id: {prodottoRecuperateDallaMemoria[i].Id}, ");
            }
            // visualizza i nomi e le descrizioni

        }
        internal static void EliminaProdotto()
        {

            Console.WriteLine("[ ---------------------------- ]");
            Console.WriteLine("[ Elimina prodotto esistente ]");

            Console.Write("Inserisci id prodotto da eliminare: ");
            
                Console.WriteLine("[ ---------------------------- ]");
                Console.WriteLine("[ Elimina prodotto esistente ]");

                Console.Write("Inserisci id categoria da eliminare: ");
                Prodotto prodottoDaProcessare = ReadProdottoFromConsole();

                //Rimozione della categoria dalla lista
                if (prodottoDaProcessare == null)
                {
                    return;
                }
                else
                {
                    ApplicationStorage.Prodotti.Remove(prodottoDaProcessare);
                    Console.WriteLine("L'elemento è stato cancellato!!!");
                }
                prodottoDaProcessare = ReadProdottoFromConsole();

                //Rimozione della Prodotto dalla lista
                if (prodottoDaProcessare == null)
                {
                    return;
                }
                else
                {
                    ApplicationStorage.Prodotti.Remove(prodottoDaProcessare);
                    Console.WriteLine("L'elemento è stato cancellato!!!");
                }

            }

        private static Prodotto ReadProdottoFromConsole()

        {
                //Richiedo id da console
                var id = Console.ReadLine();

                //Predispongo una variabile per il valore intero
                int idIntero;

                //Tento di convertire la stringa a intero e, se possibile
                //inserisco il valore intero in "idIntero". Se la conversione
                //riesce, ottengo "true" dalla funzione "TryParse", altrimenti false
                if (!int.TryParse(id, out idIntero))
                {
                    //Mostro messaggio utente
                    Console.WriteLine("Il valore inserito non è valido!");
                    return null;
                }
                else
                {
                    //Cerco l'elemento in elenco
                    Prodotto prodottoEsistente = ApplicationStorage.Prodotti
                        .SingleOrDefault(prodottoCorrente => prodottoCorrente.Id == idIntero);

                    if (prodottoEsistente == null)
                    {
                        Console.WriteLine("L'elemento richiesto non è stato trovato!");
                        return null;
                    }
                    else
                    {
                        Console.WriteLine("L'elemento richiesto esiste!");
                        return prodottoEsistente;
                    }
                }
            
        }

    }
}