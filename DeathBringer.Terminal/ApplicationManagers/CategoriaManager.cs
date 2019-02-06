using DeathBringer.Terminal.Data;
using DeathBringer.Terminal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeathBringer.Terminal.ApplicationManagers
{
    public class CategoriaManager
    {
        internal static void VisualizzaMenu()
        {
            string selezione = null;

            while (selezione != "exit")
            {

                //visualizzo a terminale l'elenco delle funzione del mio programma
                Console.WriteLine("*****");
                Console.WriteLine("Categoria Manager");
                Console.WriteLine("*********************************");
                Console.WriteLine();
                Console.WriteLine("1 - Crea Categoria");
                Console.WriteLine("2 - Modifica Categoria");
                Console.WriteLine("3 - Cancella Categoria");
                Console.WriteLine("4 - Elenco Categorie");
                Console.WriteLine("exit => uscita");
                Console.WriteLine();
                Console.Write(" selezione: ");

                //leggo la selezione
                selezione = Console.ReadLine();  //nota (NON PIù, POI L'HO DEF. PRIMA) il tipo var: sta avvenendo un'assegnazione diretta, quindi non è necessario indicare prima il suo tipo ( è un pigliatutto)
                                                 //utile specialmente quando ci sono tipi complessi lunghi, o anche molti (troppi) tipi

                //seleziona la funzione giusta
                switch (selezione)
                {
                    case "1":
                        CreaCategoria();
                        break;
                    case "2":
                        ModificaCategoria();
                        break;
                    case "3":
                        CancellaCategoria();
                        break;
                    case "4":
                        ElencoCategorie();
                        break;
                    default:  //equivale all'else, ossia se non  si è trattato di nessuno dei casi sovraindicati, di suo fai fare questo default
                        Console.WriteLine("Selezione non valida!");
                        break;
                }

            }



        }



        private static void ModificaCategoria()
        {
            throw new NotImplementedException();
        }

        private static void CancellaCategoria()
        {
            throw new NotImplementedException();
        }

        private static void CreaCategoria()
        {
            throw new NotImplementedException();
        }



        private static void ElencoCategorie()   //lo definisce private, perché a priori non è necessario che sia public o internal, viene richiamato solo qui sopra
        {
            //recupera quelle in memoria
            var categorieRecuperateDallaMemoria = ApplicationStorage.Categorie;

            var cat1 = new Categoria { Nome = "Frutta", Descrizione = "questa è frutta" };
            categorieRecuperateDallaMemoria.Add(cat1);

            // cicla sulle categorie in memoria

            for (int i = 0; i < categorieRecuperateDallaMemoria.Count; i++) //considera che categorieBLABLA è una lista, quindi ha un Count; inoltre il primo indice è 0, come in java
            {
                //visualizza nomi e descrizioni
                Console.WriteLine(
                    $"nome: {categorieRecuperateDallaMemoria[i].Nome}, " +    //accedo all'i-esimo elemento della lista, e prendo il suo Nome
                    $"desc: {categorieRecuperateDallaMemoria[i].Descrizione}, "); //accedo all'i-esimo elemento della lista, e prendo la sua Descrizione

            }
            // visualizza i nomi e le descrizioni
        }
    }

  //  private static void CancellaCategoria()
    {
        throw new NotImplementedException();
    }

    private static void ModificaCategoria()
    {
        throw new NotImplementedException();
    }

    private static void CreaCategoria()
    {
        Console.WriteLine("Creazione nuova Categoria");
        // chiediamo il nome della categoria E DESCRIZIONE
        Console.WriteLine(" => nome : ");
        var nome = Console.ReadLine();
        Console.WriteLine(" => descrizione : ");
        var descr = Console.ReadLine();

        Categoria cat2 = new Categoria();
        cat2.Nome = nome;
        cat2.Descrizione = descr;

        //creazione categoria
        Categoria cat = new Categoria  //invece di mettere parentesi tonde, metto parentesi graffe e ad ogni variabile assegno quello che voglio, separate da virgole, e 
                                       //; dopo la graffa

        {
            Nome = nome,
            Descrizione = descr
        };
        Console.WriteLine($"Creata categoria {cat.Nome}!"); //oppure concateni, ya know, ma conviene questo modo moderno
        Console.ReadLine();
    }

}


