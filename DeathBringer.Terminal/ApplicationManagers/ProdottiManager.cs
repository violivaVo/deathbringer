using System;
using System.Collections.Generic;
using System.Text;

namespace DeathBringer.Terminal.ApplicationManagers
{
    public class ProdottiManager
    {
        public static void Indice()
        {
            string selection = null;

            Console.WriteLine("**********************");
            Console.WriteLine("  Menu Prodotti ");
            Console.WriteLine("**********************");
            Console.WriteLine();
            Console.WriteLine("A - Inserimento Prodotto");
            Console.WriteLine("B - Lista Prodotti");
            Console.WriteLine("C - Rimuovi Prodotto");
            Console.WriteLine("D - Modifica Prodotto");
            Console.WriteLine("exit => uscita");
            Console.WriteLine();
            Console.Write(" selezione: ");
            selection = Console.ReadLine();

            while (selection != "exit")
            {
                                
                switch (selection)
                {
                    case "A":
                        InserimentoProdotto();
                        break;
                    case "B":
                        ListaProdotti();
                        break;
                    case "C":
                        RimuoviProdotto();
                        break;
                    case "D":
                        ModificaProdotto();
                        break;
                    default:
                        Console.WriteLine("Selezone non valida!");
                        break;
                }
            }
        }

        private static void InserimentoProdotto()
        {

        }

        private static void ListaProdotti()
        {

        }

        private static void RimuoviProdotto()
        {

        }

        private static void ModificaProdotto()
        {

        }
    }
}