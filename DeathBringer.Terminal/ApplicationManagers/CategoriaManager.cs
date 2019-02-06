using DeathBringer.Terminal.Data;
using DeathBringer.Terminal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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
                        EliminaCategoria();
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

        }


        private static void EliminaCategoria()
        {
            Console.WriteLine("[Elimina categoria esistente]");

            Console.Write("Inserisci id categoria da eliminare: ");
            var id = Console.ReadLine();

            //non ti puoi fidare del'utente, potrebbe mette qualcosa che non va bene(ma molto molto non bene)

            //predispongo una variabile per il valore intero
            int idIntero;
            //tento di convertire la stringa a intero e se possibile inserisco il valore intero in idIntero
            //se la conversione riesce ottengo "true" dalla funz TryParse, altrimenti false
            if (!int.TryParse(id, out idIntero)) //questo tryparse cerca di capire cosa stia nell'int, quindi dà true o false a seconda che trovi idIntero o meno
            {
                //mostro messaggio utente
                Console.Write("Il valore inserito non è valido!");
                return;    //quindi non posso andare avanti
            }

            else
            {
                Categoria categoriaEsistente = ApplicationStorage.Categorie
                .SingleOrDefault(e => e.Id == idIntero);

                //se il numero è valido ma non ho trovato l'elemento
                if (categoriaEsistente == null)
                {
                    Console.Write("Nessun elemento trovato");
                    return;
                }
                else
                {
                    ApplicationStorage.Categorie.Remove(categoriaEsistente);  //il Remove prende come argomento un tipo Categoria
                    Console.Write("L'elemento è stato cancellato!");
                }
            }
        }

        private static void CancellaCategoria()
        {
            throw new NotImplementedException();
        }


        private static void ElencoCategorie()   //lo definisce private, perché a priori non è necessario che sia public o internal, viene richiamato solo qui sopra
        {
            //recupera quelle in memoria
            var categorieRecuperateDallaMemoria = ApplicationStorage.Categorie;


            // cicla sulle categorie in memoria

            for (int i = 0; i < categorieRecuperateDallaMemoria.Count; i++) //considera che categorieBLABLA è una lista, quindi ha un Count; inoltre il primo indice è 0, come in java
            {
                //visualizza nomi e descrizioni
                Console.WriteLine(
                    $"nome: {categorieRecuperateDallaMemoria[i].Nome}, " +    //accedo all'i-esimo elemento della lista, e prendo il suo Nome
                    $"descr: {categorieRecuperateDallaMemoria[i].Descrizione}, "+ //accedo all'i-esimo elemento della lista, e prendo la sua Descrizione
                    $"id: {categorieRecuperateDallaMemoria[i].Id}, ");
        }
            // visualizza i nomi e le descrizioni
        }

        private static void CreaCategoria()
        {
            Console.WriteLine("Creazione nuova Categoria");
            // chiediamo il nome della categoria E DESCRIZIONE
            Console.WriteLine(" => nome : ");
            var nome = Console.ReadLine();
            Console.WriteLine(" => descrizione : ");
            var descr = Console.ReadLine();


            //creazione categoria
            Categoria cat = new Categoria  //invece di mettere parentesi tonde, metto parentesi graffe e ad ogni variabile assegno quello che voglio, separate da virgole, e 
                                           //; dopo la graffa

            {
                Id = GeneraNuovoIdentificatore(), //metodo per poter richiamarlo quando modifico
                Nome = nome,
                Descrizione = descr
            };

            ApplicationStorage.Categorie.Add(cat);

            Console.WriteLine($"Creata categoria {cat.Nome}!"); //oppure concateni, ya know, ma conviene questo modo moderno
            Console.ReadLine();
        }

        private static int GeneraNuovoIdentificatore() //non è void perché questa funz. a diff. delle altre mi ritorna qualcosa, un int
        {
            //verifico quanti ce ne sono in archivio
            var elementiEsistenti = ApplicationStorage.Categorie.Count;
             //se non ne ho, il valore base è 1
             if (elementiEsistenti == 0)
            {
                return 1;
            }
            else
            {   //devo cercare l'elemento con Id maggiore
                int idMaggiore = 0;
                for (var i = 0; 1 < ApplicationStorage.Categorie.Count; i++)
                {
                    if (ApplicationStorage.Categorie[i].Id > idMaggiore)
                    {
                        idMaggiore = ApplicationStorage.Categorie[i].Id;  
                    }

                }

                return idMaggiore+1 ;
                // al posto del for qui sopra avrei potuto mette -> idMaggiore = ApplicationStorage.Categorie.Max(elementiEsistenti e => e.Id); (AVENDO MESSO SYSTEM LINQ)
                //questa cosa usa LINQ (??)
            }
        }
    }

}


