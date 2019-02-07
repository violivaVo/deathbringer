using DeathBringer.Terminal.Data;
using DeathBringer.Terminal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeathBringer.Terminal.ApplicationManagers
{
    public class UtentiManager
    {
        static string selezioneU = null;
        public static void VisualizzaMenu()
        {
            while (selezioneU != "exit")
            {

                //visualizzo a terminale l'elenco delle funzione del mio programma
                Console.WriteLine("******");
                Console.WriteLine("Utenti Manager");
                Console.WriteLine("*********************************");
                Console.WriteLine();
                Console.WriteLine("1 - Crea Utente");
                Console.WriteLine("2 - Modifica Utente");
                Console.WriteLine("3 - Cancella Utente");
                Console.WriteLine("4 - Elenco Utenti");
                Console.WriteLine("5 - Login");
                Console.WriteLine("exit => uscita");
                Console.WriteLine();
                Console.Write(" selezione: ");

                //leggo la selezione
                selezioneU = Console.ReadLine();  //nota (NON PIù, POI L'HO DEF. PRIMA) il tipo var: sta avvenendo un'assegnazione diretta, quindi non è necessario indicare prima il suo tipo ( è un pigliatutto)
                                                 //utile specialmente quando ci sono tipi complessi lunghi, o anche molti (troppi) tipi

                //seleziona la funzione giusta
                switch (selezioneU)
                {
                    case "1":
                        CreaUtente();
                        break;
                    case "2":
                        ModificaUtente();
                        break;
                    case "3":
                        EliminaUtente();
                        break;
                    case "4":
                        ElencoUtenti();
                        break;
                    case "5":
                        Login();
                        break;
                    default:  //equivale all'else, ossia se non  si è trattato di nessuno dei casi sovraindicati, di suo fai fare questo default
                        Console.WriteLine("Selezione non valida!");
                        break;
                }

            }
        }

        private static void Login()
        {
            Console.WriteLine("---- Inserisce il tuo username :");
            String UserNameIn = Console.ReadLine();
            Console.WriteLine("---- Inserisce il tuo password :");
            String PasswordIn = Console.ReadLine();
            if ((UserNameIn == null) || (PasswordIn == null))
            {
                Console.WriteLine("Dati entrati non validi");
            }else
            {
                for(int i = 0; i < ApplicationStorage.Utenti.Count; i++)
                {
                    if((ApplicationStorage.Utenti[i].Username == UserNameIn) && (ApplicationStorage.Utenti[i].Password == PasswordIn))
                    {
                        Console.WriteLine("*** successo dell'autenticazione *****");
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("!!!!!! errore di autenticazione : verifica i tue dati!!!!");
                        Console.ReadLine();
                    }
                }
            }
            Console.WriteLine();

        }

        private static void CreaUtente()
        {
            Console.WriteLine("Creazione nuovo utente");
            Console.WriteLine(" => Nome : ");
            var nome = Console.ReadLine();
            Console.WriteLine(" => Cognome : ");
            var cognome = Console.ReadLine();

           
            Utente cat = new Utente //invece di mettere parentesi tonde, metto parentesi graffe e ad ogni variabile assegno quello che voglio, separate da virgole, e 
                                           //; dopo la graffa
            {
                Id = GeneraNuovoUtente(), //metodo per poter richiamarlo quando modifico
                Nome = nome,
                Cognome = cognome
            };

            ApplicationStorage.Utenti.Add(cat);

            Console.WriteLine($"Inserito nuovo utente {cat.Nome}!"); //oppure concateni, ya know, ma conviene questo modo moderno
            Console.ReadLine();
        }

        private static int GeneraNuovoUtente()
        {
            //verifico quanti ce ne sono in archivio
            var elementiEsistenti = ApplicationStorage.Utenti.Count;
            //se non ne ho, il valore base è 1
            if (elementiEsistenti == 0)
            {
                return 1;
            }
            else
            {   //devo cercare l'elemento con Id maggiore
                int idMaggiore = 0;
                for (var i = 0; i < ApplicationStorage.Utenti.Count; i++)
                {
                    if (ApplicationStorage.Utenti[i].Id > idMaggiore)
                    {
                        idMaggiore = ApplicationStorage.Utenti[i].Id;
                    }

                }

                return idMaggiore + 1;
                // al posto del for qui sopra avrei potuto mette -> idMaggiore = ApplicationStorage.Categorie.Max(elementiEsistenti e => e.Id); (AVENDO MESSO SYSTEM LINQ)
                //questa cosa usa LINQ (??)
            }
        }
    

        private static void ModificaUtente()
        {

            Console.WriteLine("[ ---------------------------- ]");
            Console.WriteLine("[ Modifica utente esistente ]");

            Console.Write("Inserisci id Utente da modificare: ");
            Utente utenteDaProcessare = ReadUtenteFromConsole();

            if (utenteDaProcessare == null)
            {
                return;
            }
            else
            {
                //Richiedo il nuovo nome
                Console.Write(" => nuovo username: ");
                var nuovoUsername = Console.ReadLine();
                Console.Write(" => nuovo nome: ");
                var nuovoNome = Console.ReadLine();
                Console.Write(" => nuovo cognome: ");
                var nuovoCognome = Console.ReadLine();
                Console.Write(" => nuova email: ");
                var nuovaEmail = Console.ReadLine();
                Console.Write(" => nuovo indirizzo: ");
                var nuovoIndirizzo = Console.ReadLine();
                Console.Write(" => nuovo civico: ");
                var nuovoCivico = Console.ReadLine();
                Console.Write(" => nuovo cap: ");
                var nuovoCap = Console.ReadLine();
                Console.Write(" => nuova città: ");
                var nuovaCitta = Console.ReadLine();
                Console.Write(" => nuova password: ");
                var nuovaPassword = Console.ReadLine();

                //Assegnamento ad oggetto esistente
                utenteDaProcessare.Username = nuovoUsername;
                utenteDaProcessare.Nome = nuovoNome;
                utenteDaProcessare.Cognome = nuovoCognome;
                utenteDaProcessare.Email = nuovaEmail;
                utenteDaProcessare.Indirizzo = nuovoIndirizzo;
                utenteDaProcessare.Civico = nuovoCivico;
              


                Console.WriteLine("La modifica è stata completata!");
            }
        }

        private static Utente ReadUtenteFromConsole()
        {
            var id = Console.ReadLine();
            int idIntero;

            if (!int.TryParse(id, out idIntero))
            {
                Console.WriteLine("Il valore inserito non è valido!");
                return null;
            }

            else
            {  //trovo l'utente che voglio modificare
                Utente utenteEsistente = ApplicationStorage.Utenti
                    .SingleOrDefault(utenteCorrente => utenteCorrente.Id == idIntero);

                if (utenteEsistente == null)
                {
                    Console.WriteLine("L'utente selezionato non esiste!");
                    return null;
                }
                else
                {
                    Console.WriteLine("L'utente selezionato esiste!");
                    return utenteEsistente;
                }
            }

        }

        private static void EliminaUtente()
        {
            Console.WriteLine("[ ---------------------------- ]");
            Console.WriteLine("[ Elimina utente esistente ]");

            Console.Write("Inserisci id utente da eliminare: ");
            Utente utenteDaProcessare = ReadUtenteFromConsole();

            //Rimozione della categoria dalla lista
            if (utenteDaProcessare == null)
            {
                return;
            }
            else
            {
                ApplicationStorage.Utenti.Remove(utenteDaProcessare);
                Console.WriteLine("L'utente è stato cancellato!!!");
            }
        }

        

       

        private static void ElencoUtenti()
        {
            //recupera quelli in memoria
            var UtentiRecuperatiDallaMemoria = ApplicationStorage.Utenti;
            // cicla sulle categorie in memoria
            for (int i = 0; i < UtentiRecuperatiDallaMemoria.Count; i++)
            {
                // Visualizza dati Utente
                Console.WriteLine(
                    $"nome: {UtentiRecuperatiDallaMemoria[i].Nome}, " +    //accedo all'i-esimo elemento della lista, e prendo il suo Nome
                    $"Cognome: {UtentiRecuperatiDallaMemoria[i].Cognome}, " +
                    $"Email: {UtentiRecuperatiDallaMemoria[i].Email}, " +
                    $"Indirizzo: {UtentiRecuperatiDallaMemoria[i].Indirizzo}, " +
                    $"Civico: {UtentiRecuperatiDallaMemoria[i].Civico}, " +
                    $"Citta: {UtentiRecuperatiDallaMemoria[i].Citta}, " +
                    $"Cap: {UtentiRecuperatiDallaMemoria[i].Cap}, "
                    );
            }
        // visualizza i nomi, Cognome, Email, Indirizzo, Civico, Citta e Cap 
    }
    }
}
