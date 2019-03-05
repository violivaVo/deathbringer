using DeathBringer.Core.Data;
using DeathBringer.Terminal.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DeathBringer.Core.Mocks
{
    public class MockUtenteRepository : IUtenteRepository
    {
        //Lista statica per mock
        private static Lazy<IList<Utente>> Utenti = new Lazy<IList<Utente>>(Initialize);

        private static IList<Utente> Initialize()
        {
            //Creazione lista utenti fittizi
            return new List<Utente>
            {
                new Utente
                {
                    Username = "mario.rossi",
                    Email = "mario@icubed.it",
                    Password = "paperino",
                    Cognome ="Rossi",
                    Nome = "Mario"
                },
                new Utente
                {
                    Username = "giuseppe.verdi",
                    Email = "giuseppe@icubed.it",
                    Password = "paperino",
                    Cognome = "Verdi", 
                    Nome = "Giuseppe"
                },
                new Utente
                {
                    Username = "antonio.bianchi",
                    Email = "antonio@icubed.it",
                    Password = "paperino",
                    Cognome = "Antonio",
                    Nome = "Bianchi"
                }
            };
        }

        public IList<ValidationResult> Crea(Utente utente)
        {
            //Semplice aggiunta
            Utenti.Value.Add(utente);

            //Ritorno senza errori
            return new List<ValidationResult>();
        }

        public IList<ValidationResult> Elimina(Utente utente)
        {
            //Rimozione
            Utenti.Value.Remove(utente);

            //Ritorno senza errori
            return new List<ValidationResult>();
        }

        public IList<Utente> FetchAllUtenti()
        {
            //Ritorna la lista statica
            return Utenti.Value;
        }

        public Utente GetById(int id)
        {
            //Ritorno l'utente con id (se esiste)
            return Utenti.Value.SingleOrDefault(e => e.Id == id);
        }

        public IList<ValidationResult> Modifica(Utente utente)
        {
            //Predisponsizione a nessun errore
            var validations = new List<ValidationResult>();

            //Rimpiazza
            var existing = Utenti.Value.SingleOrDefault(e => e.Id == utente.Id);
            if (existing == null)
                return validations;
            Utenti.Value.Remove(existing);
            Utenti.Value.Add(utente);
            return validations;
        }
    }
}
