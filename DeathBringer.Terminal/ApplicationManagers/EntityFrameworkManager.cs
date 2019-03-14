using DeathBringer.Terminal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yred.Authentication.Relationals.Data.Contexts;

namespace DeathBringer.Terminal.ApplicationManagers
{
    public static class EntityFrameworkManager
    {
        internal static void VisualizzaMenu()
        {
            string selezione = null;

            while (selezione != "exit")
            {
                //visualizzo a terminale l'elenco delle funzione del mio programma
                Console.WriteLine("**********************");
                Console.WriteLine(" EntityFramework Manager ");
                Console.WriteLine("**********************");
                Console.WriteLine();
                Console.WriteLine("1 - Carica utenti");
                Console.WriteLine("2 - Carica utenti (ignorante)");
                Console.WriteLine("3 - Proiezione utenti");
                Console.WriteLine("4 - Crea categoria");
                Console.WriteLine("exit => uscita");
                Console.WriteLine();
                Console.Write(" selezione: ");

                //leggo la selezione
                selezione = Console.ReadLine();

                //seleziona la funzione giusta
                switch (selezione)
                {
                    case "1":
                        LoadUtenti();
                        break;
                    case "2":
                        LoadUtentiIgnorante();
                        break;
                    case "3":
                        LoadUtentiConProiezione();
                        break;
                    case "4":
                        CreaCategoria();
                        break;
                    default:  //equivale all'else, ossia se non  si è trattato di nessuno dei casi sovraindicati, di suo fai fare questo default
                        Console.WriteLine("Selezione non valida!");
                        break;
                }
            }
        }

        public static void LoadUtenti()
        {
            //Crea istanza di DbContext
            DeathBringerDbContext context = new DeathBringerDbContext();

            //Chiedo all'utente lo username da cercare
            Console.Write("Username da cercare: ");
            var usernameDaCercare = Console.ReadLine();

            //SELECT * FROM tabella_Utenti
            //WHERE Username = 'mario.rossi'

            //Recupero solo gli utenti con username "mario.rossi"
            IList<Utente> result = context.Utenti
                .Where(u => u.UserName == usernameDaCercare)
                .ToList();
            Console.WriteLine($" => trovati: {result.Count} elementi...");
            Console.WriteLine();

            //Visualizza risultato
            foreach (Utente currentUser in result)
            {
                Console.WriteLine($"username : {currentUser.UserName}");
                Console.WriteLine($"id       : {currentUser.Id}");
                Console.WriteLine($"nome     : {currentUser.Nome}");
                Console.WriteLine($"cognome  : {currentUser.Cognome}");
            }
        }

        public static void LoadUtentiIgnorante()
        {
            //Crea istanza di DbContext
            DeathBringerDbContext context = new DeathBringerDbContext();

            //Chiedo all'utente lo username da cercare
            Console.Write("Testo da cercare: ");
            var testoDaCercare = Console.ReadLine();

            //SELECT * FROM tabella_Utenti
            //WHERE 
            //(Username IS NOT NULL AND Username LIKE 'ma') OR
            //Email LIKE 'ma' OR
            //Nome LIKE 'ma' OR
            //Cognome LIKE 'ma' OR
            //Citta LIKE 'ma' OR
            //Indirizzo LIKE 'ma'

            //Recupero solo gli utenti con username "mario.rossi"
            IList<Utente> result = context.Utenti
                .Where(u => 
                    (
                        u.UserName != null && 
                        u.UserName.IndexOf(testoDaCercare, StringComparison.InvariantCultureIgnoreCase) >= 0
                    )
                    ||
                    (
                        u.Email != null &&
                        u.Email.IndexOf(testoDaCercare, StringComparison.InvariantCultureIgnoreCase) >= 0
                    )
                    ||
                    (
                        u.Nome != null &&
                        u.Nome.IndexOf(testoDaCercare, StringComparison.InvariantCultureIgnoreCase) >= 0
                    )
                    ||
                    (
                        u.Cognome != null &&
                        u.Cognome.IndexOf(testoDaCercare, StringComparison.InvariantCultureIgnoreCase) >= 0
                    )
                    ||
                    (
                        u.Citta != null &&
                        u.Citta.IndexOf(testoDaCercare, StringComparison.InvariantCultureIgnoreCase) >= 0
                    )
                    ||
                    (
                        u.Indirizzo != null &&
                        u.Indirizzo.IndexOf(testoDaCercare, StringComparison.InvariantCultureIgnoreCase) >= 0
                    )
                )
                .ToList();
            Console.WriteLine($" => trovati: {result.Count} elementi...");
            Console.WriteLine();

            //Visualizza risultato
            foreach (Utente currentUser in result)
            {
                Console.WriteLine($"username : {currentUser.UserName}");
                Console.WriteLine($"id       : {currentUser.Id}");
                Console.WriteLine($"nome     : {currentUser.Nome}");
                Console.WriteLine($"cognome  : {currentUser.Cognome}");
            }
        }

        public static void LoadUtentiConProiezione()
        {
            //Crea istanza di DbContext
            DeathBringerDbContext context = new DeathBringerDbContext();

            //Chiedo all'utente lo username da cercare
            Console.Write("Username da cercare: ");
            var usernameDaCercare = Console.ReadLine();

            //SELECT Username as NomeUtente, (Cap + 10) as NumeroDiScarpe 
            //FROM tabella_Utenti
            //WHERE Username = 'mario.rossi'

            //LINQ

            //Recupero solo gli utenti con username "mario.rossi"
            var result = context.Utenti
                .Where(u => u.UserName == usernameDaCercare)
                .Select(u => new
                {
                    NomeUtente = u.UserName, 
                    NumeroDiScarpe = u.Cap + 10
                })
                .ToList();
            Console.WriteLine($" => trovati: {result.Count} elementi...");
            Console.WriteLine();

            //Visualizza risultato
            foreach (var currentProjection in result)
            {
                Console.WriteLine($"nome utente      : {currentProjection.NomeUtente}");
                Console.WriteLine($"numero di scarpe : {currentProjection.NumeroDiScarpe}");
            }
        }

        public static void CreaCategoria()
        {
            //Crea istanza di DbContext
            DeathBringerDbContext context = new DeathBringerDbContext();

            //Chiedo all'utente lo username da cercare
            Console.Write("Nome : ");
            var nome = Console.ReadLine();
            Console.Write("Descrizione: ");
            var descrizione = Console.ReadLine();

            //INSERT INTO tabella_Categorie
            //VALUES (...)'

            //Creazione entità
            var categoria = new Categoria
            {
                Nome = nome, 
                Descrizione = descrizione
            };

            //Aggiunta sul DbContext
            context.Categorie.Add(categoria);

            //Salvataggio
            context.SaveChanges();

            //Cerifica che ci sia
            var result = context.Categorie
                .SingleOrDefault(c => c.Nome == nome);
            var feedback = result == null ? "FAILED" : "OK";
            Console.WriteLine($" => risultato ricerca: {feedback}");
        }
    }
}
