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
    public class CategoriaManager
    {
        internal static void VisualizzaMenu()
        {
            string selezione = null;

            while (selezione != "exit")
            {

                //visualizzo a terminale l'elenco delle funzione del mio programma
                Console.WriteLine("**********************");
                Console.WriteLine(" Categoria Manager ");
                Console.WriteLine("**********************");
                Console.WriteLine();
                Console.WriteLine("1 - Crea Categoria");
                Console.WriteLine("2 - Modifica Categoria");
                Console.WriteLine("3 - Cancella Categoria");
                Console.WriteLine("4 - Elenco Categorie");
                Console.WriteLine("exit => uscita");
                Console.WriteLine();
                Console.Write(" selezione: ");

                //leggo la selezione
                selezione = Console.ReadLine();  //nota (NON PIù, POI L'HO DEF. PRIMA) il tipo var: sta avvenendo un'assegnazione diretta, quindi non è necessario indicare prima il suo tipo ( è un pigliatutto)
                                                 //utile specialmente quando ci sono tipi complessi lunghi, o anche molti (troppi) tipi

                //seleziona la funzione giusta
                switch (selezione)
                {
                    case "1":
                        CreaCategoria();
                        break;
                    case "2":
                        ModificaCategoria();
                        break;
                    case "3":
                        EliminaCategoria();
                        break;
                    case "4":
                        ElencoCategorie();
                        break;
                    default:  //equivale all'else, ossia se non  si è trattato di nessuno dei casi sovraindicati, di suo fai fare questo default
                        Console.WriteLine("Selezione non valida!");
                        break;
                }

            }



        }

        private static void ModificaCategoria()
        {
            Console.WriteLine("[ ---------------------------- ]");
            Console.WriteLine("[ Modifica categoria esistente ]");

            Console.Write("Inserisci id categoria da modificare: ");
            Categoria categoriaDaProcessare = ReadCategoriaFromConsole();

            //Se non esiste, non faccio null
            if (categoriaDaProcessare == null)
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
                categoriaDaProcessare.Nome = nuovoNome;
                categoriaDaProcessare.Descrizione = nuovaDesc;
                ApplicationStorage.SaveCategorie();
                Console.WriteLine("La modifica è stata fatta!");
            }
        }



        private static void EliminaCategoria()
        {
            Console.WriteLine("[ ---------------------------- ]");
            Console.WriteLine("[ Elimina categoria esistente ]");

            Console.Write("Inserisci id categoria da eliminare: ");
            Categoria categoriaDaProcessare = ReadCategoriaFromConsole();

            //Rimozione della categoria dalla lista
            if (categoriaDaProcessare == null)
            {
                return;
            }
            else
            {
                ApplicationStorage.Categorie.Remove(categoriaDaProcessare);
                Console.WriteLine("L'elemento è stato cancellato!!!");
            }
        }

        private static Categoria ReadCategoriaFromConsole()
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
                Categoria categoriaEsistente = ApplicationStorage.Categorie
                    .SingleOrDefault(categoriaCorrente => categoriaCorrente.Id == idIntero);

                if (categoriaEsistente == null)
                {
                    Console.WriteLine("L'elemento richiesto non è stato trovato!");
                    return null;
                }
                else
                {
                    Console.WriteLine("L'elemento richiesto esiste!");
                    return categoriaEsistente;
                }
            }
        }



        private static void ElencoCategorie()   //lo definisce private, perché a priori non è necessario che sia public o internal, viene richiamato solo qui sopra
        {
            //recupera quelle in memoria
            ApplicationServiceLayer layer = new ApplicationServiceLayer();
            var validazioni = layer.FetchCategorie();
            var categorieRecuperateDallaMemoria = ApplicationStorage.Categorie;


            // cicla sulle categorie in memoria

            for (int i = 0; i < categorieRecuperateDallaMemoria.Count; i++) //considera che categorieBLABLA è una lista, quindi ha un Count; inoltre il primo indice è 0, come in java
            {
                //visualizza nomi e descrizioni
                Console.WriteLine(
                    $"nome: {categorieRecuperateDallaMemoria[i].Nome}, " +    //accedo all'i-esimo elemento della lista, e prendo il suo Nome
                    $"descr: {categorieRecuperateDallaMemoria[i].Descrizione}, " + //accedo all'i-esimo elemento della lista, e prendo la sua Descrizione
                    $"id: {categorieRecuperateDallaMemoria[i].Id}, ");
            }
            // visualizza i nomi e le descrizioni
        }

        private static void CreaCategoria()
        {
            Console.WriteLine("Creazione nuova Categoria");
            // chiediamo il nome della categoria E DESCRIZIONE
            Console.WriteLine(" => nome : ");
            var nome = Console.ReadLine();
            Console.WriteLine(" => descrizione : ");
            var descr = Console.ReadLine();

            //Istanzio il layer di lavoro
            ApplicationServiceLayer layer = new ApplicationServiceLayer();

            //Aggancio l'evento di salvataggio della categoria e mostro a video
            layer.CategorieSaved += (o, a) => { Console.WriteLine(a); };

            //Creazione categorie
            var validazioni = layer.InsertCategoria(nome, descr);

            //Se non ho errori, confermo
            if (validazioni.Count == 0)
            {
                //Visualizza conferma
                Console.WriteLine($"Creata categoria {nome}!");
                return;
            }

            //Itero i messaggi di errore
            foreach (var current in validazioni)
                Console.WriteLine($"Errore: {current.ErrorMessage}");

            //Resto in attesa
            Console.ReadLine();
        }
    }
}


