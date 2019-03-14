using DeathBringer.Core.Data;
using DeathBringer.Core.Data.Helpers;
using DeathBringer.Terminal.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Yred.Authentication.Relationals.Data.Contexts;

namespace DeathBringer.EntityFramework.Data.Repositories
{
    public class EntityFrameworkUtenteRepository : IUtenteRepository
    {
        /// <summary>
        /// Contesto di EntityFramework
        /// </summary>
        private DeathBringerDbContext Context { get; }

        /// <summary>
        /// Construttore
        /// </summary>
        public EntityFrameworkUtenteRepository()
        {
            //Inizializzazione del contesto di lavoro
            Context = new DeathBringerDbContext();
        }

        /// <summary>
        /// Ritorna la lista di tutti gli utenti nel sistema
        /// </summary>
        /// <returns>Ritorna la lista degli utenti</returns>
        public IList<Utente> FetchAllUtenti()
        {
            //Semplicemente ritorno la lista sul Context
            return Context.Utenti
                .OrderBy(e => e.UserName)
                .ThenBy(e => e.Nome)
                .ToList();
        }

        /// <summary>
        /// Eseguo la creazione di un utente
        /// </summary>
        /// <param name="utente">Utente</param>
        /// <returns>Ritorna la lista delle validazioni fallite</returns>
        public IList<ValidationResult> Crea(Utente utente)
        {
            //Validazione argomento
            if (utente == null) throw new ArgumentNullException(nameof(utente));

            //Eseguo la validazione dell'entità: se non passo, esco
            var validations = ValidationUtils.Validate(utente);
            if (validations.Count > 0)
                return validations;

            //Aggiungo al context
            Context.Utenti.Add(utente);

            //Eseguo il "Commit"
            Context.SaveChanges();

            //Ritorno la lista delle validazioni (vuote)
            return validations;
        }

        /// <summary>
        /// Elimina l'utente specificato
        /// </summary>
        /// <param name="utente">Utente da eliminare</param>
        /// <returns>Ritorna la lista delle validazioni fallite</returns>
        public IList<ValidationResult> Elimina(Utente utente)
        {
            //Validazione argomento
            if (utente == null) throw new ArgumentNullException(nameof(utente));

            //Aggiungo al context
            Context.Utenti.Remove(utente);

            //Eseguo il "Commit"
            Context.SaveChanges();

            //Ritorno la lista delle validazioni (vuote)
            return new List<ValidationResult>();
        }

        /// <summary>
        /// Modifica l'utente specificato
        /// </summary>
        /// <param name="utente">Utente da modificare</param>
        /// <returns>Ritorna la lista delle validazioni fallite</returns>
        public IList<ValidationResult> Modifica(Utente utente)
        {
            //Validazione argomento
            if (utente == null) throw new ArgumentNullException(nameof(utente));

            //Eseguo la validazione dell'entità: se non passo, esco
            var validations = ValidationUtils.Validate(utente);
            if (validations.Count > 0)
                return validations;

            //Non ho bisogno di modificare perchè l'entità 
            //è già tracciata dal Context di Entity Framework

            //Eseguo il "Commit"
            Context.SaveChanges();

            //Ritorno la lista delle validazioni (vuote)
            return validations;
        }

        /// <summary>
        /// Ritorna un elemento corrispondente all'id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Ritorna un elemento o null</returns>
        public Utente GetById(int id)
        {
            //Semplicemente ritorno la lista sul Context
            return Context.Utenti
                .SingleOrDefault(e => e.Id == id);
        }
    }
}
