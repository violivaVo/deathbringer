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
            Console.WriteLine("1 - Dati Esempio");
            Console.WriteLine("2 - Gestione Prodotti");
            Console.WriteLine("3 - Gestione Categorie");
            Console.WriteLine("4 - Gestione Utenti");
            Console.WriteLine("5 - Gestione Prodotti");
            Console.WriteLine("Premere un tasto per terminare");

            // permetto all'utente di scegliiere una funzione (un numero)

            Console.WriteLine();
            Console.Write("Selezione: ");
            string numeroSelezioneUtente = Console.ReadLine();
            Console.WriteLine(" Selezione eseguita:" + numeroSelezioneUtente);

            // leggo il numero e avvio la funzione scelta
            if (numeroSelezioneUtente == "1")
            {
                SampleCreazioneDati.CreaDatiEsempio();
            }
            else
            {
                if (numeroSelezioneUtente == "2")
                {
                    ProdottiManager.Indice();
                }

                else if (numeroSelezioneUtente == "3")
                {
                    CategoriaManager.VisualizzaMenu();
                }


                else if (numeroSelezioneUtente == "4")
                {
                    UtentiManager.VisualizzaMenu();
                }

                else
                {
                    if (numeroSelezioneUtente == "5")
                    {
                        ProdottiManager.Indice();
                    }

                    else
                    {
                        throw new InvalidOperationException("La selezione fatta non è gestita!");
                    }

                }

            }

        }
    }
}
