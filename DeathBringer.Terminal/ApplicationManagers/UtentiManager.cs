using DeathBringer.Terminal.Data;
using DeathBringer.Terminal.Entities;
using System;
using System.Linq;

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
            throw new NotImplementedException();
        }

        private static void CreaUtente()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        private static void ElencoUtenti()
        {
            throw new NotImplementedException();
        }
    }
}
