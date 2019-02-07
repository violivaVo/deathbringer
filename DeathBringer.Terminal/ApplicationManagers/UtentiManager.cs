using DeathBringer.Terminal.Data;
<<<<<<< HEAD
=======
using DeathBringer.Terminal.Entities;
>>>>>>> ca94fb95179ee032f8aeea94e4af9ff38c19873a
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
            Console.WriteLine("---- Inserisce il tuo username :");
            String UserNameIn = Console.ReadLine();
            Console.WriteLine("---- Inserisce il tuo password :");
            String PasswordIn = Console.ReadLine();
            if ((UserNameIn == null) || (PasswordIn == null))
            {
                Console.WriteLine("Dati entrati non validi");
            }else
            {
                for(int i = 0; i < ApplicationStorage.Utenti.Count; i++)
                {
                    if((ApplicationStorage.Utenti[i].Username == UserNameIn) && (ApplicationStorage.Utenti[i].Password == PasswordIn))
                    {
                        Console.WriteLine("*** successo dell'autenticazione *****");
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("!!!!!! errore di autenticazione : verifica i tue dati!!!!");
                        Console.ReadLine();
                    }
                }
            }
            Console.WriteLine();

        }

        private static void CreaUtente()
        {
            Console.WriteLine("Creazione nuovo utente");
            Console.WriteLine(" => Nome : ");
            var nome = Console.ReadLine();
            Console.WriteLine(" => Cognome : ");
            var cognome = Console.ReadLine();

           
            Utente cat = new Utente //invece di mettere parentesi tonde, metto parentesi graffe e ad ogni variabile assegno quello che voglio, separate da virgole, e 
                                           //; dopo la graffa
            {
                Id = GeneraNuovoUtente(), //metodo per poter richiamarlo quando modifico
                Nome = nome,
                Cognome = cognome
            };

            ApplicationStorage.Utenti.Add(cat);

            Console.WriteLine($"Inserito nuovo utente {cat.Nome}!"); //oppure concateni, ya know, ma conviene questo modo moderno
            Console.ReadLine();
        }

        private static int GeneraNuovoUtente()
        {
            //verifico quanti ce ne sono in archivio
            var elementiEsistenti = ApplicationStorage.Utenti.Count;
            //se non ne ho, il valore base è 1
            if (elementiEsistenti == 0)
            {
                return 1;
            }
            else
            {   //devo cercare l'elemento con Id maggiore
                int idMaggiore = 0;
                for (var i = 0; i < ApplicationStorage.Utenti.Count; i++)
                {
                    if (ApplicationStorage.Utenti[i].Id > idMaggiore)
                    {
                        idMaggiore = ApplicationStorage.Utenti[i].Id;
                    }

                }

                return idMaggiore + 1;
                // al posto del for qui sopra avrei potuto mette -> idMaggiore = ApplicationStorage.Categorie.Max(elementiEsistenti e => e.Id); (AVENDO MESSO SYSTEM LINQ)
                //questa cosa usa LINQ (??)
            }
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
            throw new NotImplementedException();
        }
    }
}
