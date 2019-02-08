using System;
using System.Collections.Generic;
using System.Text;

namespace DeathBringer.Terminal.ApplicationManagers
{
    public class MainMenu
    {
        public static void VisualizzaMenuPrincipale()
        {
            //segue quello che voglio fare con ciò

            //visualizzo a terminale l'elenco delle funzione del mio programma
            Console.WriteLine("******");
            Console.WriteLine("*DeathBringer - Menu Principale *");
            Console.WriteLine("*********************************");
            Console.WriteLine();
            Console.WriteLine("1 - Gestione Prodotti");
            Console.WriteLine("2 - Gestione Categorie");
            Console.WriteLine("3 - Gestione Utenti");
            Console.WriteLine("Premere un tasto per terminare");

            // permetto all'utente di scegliiere una funzione (un numero)

            Console.WriteLine();
            Console.Write("Selezione: ");
            string numeroSelezioneUtente = Console.ReadLine();
            Console.WriteLine(" Selezione eseguita:" + numeroSelezioneUtente);


            if (numeroSelezioneUtente == "1")
            {
                ProdottiManager.Indice();
            }

            else if (numeroSelezioneUtente == "2")
            {
                CategoriaManager.VisualizzaMenu();
            }


            else if (numeroSelezioneUtente == "3")
            {
                UtentiManager.VisualizzaMenu();
            }

            else
            {
                throw new InvalidOperationException("La selezione fatta non è gestita!");
            }
        }

    }
}
