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
            while (selection != "exit");
        
            Console.WriteLine("**********************");
            Console.WriteLine(" Categoria Manager ");
            Console.WriteLine("**********************");
            Console.WriteLine();
            Console.WriteLine("A - Crea Categoria");
            Console.WriteLine("B - Modifica Categoria");
            Console.WriteLine("C - Cancella Categoria");
            Console.WriteLine("D - Elenco Categorie");
            Console.WriteLine("exit => uscita");
            Console.WriteLine();
            Console.Write(" selezione: ");

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