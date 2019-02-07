using DeathBringer.Terminal.Data;
using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }

        private static void ModificaUtente()
        {
            throw new NotImplementedException();
        }

        private static void EliminaUtente()
        {
            throw new NotImplementedException();
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
