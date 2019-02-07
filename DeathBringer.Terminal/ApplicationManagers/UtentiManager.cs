using System;
using System.Collections.Generic;
using System.Text;

namespace DeathBringer.Terminal.ApplicationManagers
{
   public class UtentiManager
    {
        public static void VisualizzaMenu()
        {
            Console.WriteLine("*****Gestione utenti******");
            Console.WriteLine("1 - Inserisci Utente");
            Console.WriteLine("2 - Modifica Utente");
            Console.WriteLine("3 - Rimuovi Utente");
            Console.WriteLine("4 - Elenco Utenti");
            Console.WriteLine("5 - Login");
            Console.ReadLine();
        }
    }
}
