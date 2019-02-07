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
            throw new NotImplementedException();
        }

        private static void CreaUtente()
        {
           
        }

        private static void ModificaUtente()
        {
            throw new NotImplementedException();
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

        

        private static Utente ReadUtenteFromConsole()
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
                Utente utenteEsistente = ApplicationStorage.Utenti
                    .SingleOrDefault(utenteCorrente => utenteCorrente.Id == idIntero);

                if (utenteEsistente == null)
                {
                    Console.WriteLine("L'utente richiesto non è stato trovato!");
                    return null;
                }
                else
                {
                    Console.WriteLine("L'utente richiesto esiste!");
                    return utenteEsistente;
                }
            }
        }

        private static void ElencoUtenti()
        {
            throw new NotImplementedException();
        }
        
    }
}
