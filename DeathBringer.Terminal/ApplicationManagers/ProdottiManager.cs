using DeathBringer.Core.ServiceLayers;
using DeathBringer.Terminal.BaseClasses;
using DeathBringer.Terminal.Entities;
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

            while (selection != "exit")
            {
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

            }



        }
        internal static void InserimentoProdotto()
        {
            var Layer = new ProdottiServiceLayer();
            IList<Prodotto> prodotti = Layer.FetchProdotti();
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


