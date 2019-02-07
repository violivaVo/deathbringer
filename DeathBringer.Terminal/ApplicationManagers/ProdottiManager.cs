using System;
using System.Collections.Generic;
using System.Text;

namespace DeathBringer.Terminal.ApplicationManagers
{
    public class ProdottiManager
    {
        internal static void Indice()
        {
            string selection = null;

            while (selection != "exit")
            {
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

            }
           
            switch(selection)
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
        internal static void InserimentoProdotto()
        {

        }
        internal static void ElencoProdotti()
        {

        }
        internal static void EliminaProdotto()
        {

        }
        internal static void ModificaProdotto()
        {

        }
    }
}
