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
    public class EntityFrameworkCategoriaRepository : ICategoriaRepository
    {
        /// <summary>
        /// Contesto di EntityFramework
        /// </summary>
        private DeathBringerDbContext Context { get; }

        /// <summary>
        /// Construttore
        /// </summary>
        public EntityFrameworkCategoriaRepository()
        {
            //Inizializzazione del contesto di lavoro
            Context = new DeathBringerDbContext();
        }
        
        /// <summary>
        /// Eseguo la creazione di un entity
        /// </summary>
        /// <param name="entity">Categoria</param>
        /// <returns>Ritorna la lista delle validazioni fallite</returns>
        public IList<ValidationResult> Crea(Categoria entity)
        {
            //Validazione argomento
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            //Eseguo la validazione dell'entità: se non passo, esco
            var validations = ValidationUtils.Validate(entity);
            if (validations.Count > 0)
                return validations;

            //Aggiungo al context
            Context.Categorie.Add(entity);

            //Eseguo il "Commit"
            Context.SaveChanges();

            //Ritorno la lista delle validazioni (vuote)
            return validations;
        }

        /// <summary>
        /// Elimina l'entity specificato
        /// </summary>
        /// <param name="entity">Categoria da eliminare</param>
        /// <returns>Ritorna la lista delle validazioni fallite</returns>
        public IList<ValidationResult> Elimina(Categoria entity)
        {
            //Validazione argomento
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            //Aggiungo al context
            Context.Categorie.Remove(entity);

            //Eseguo il "Commit"
            Context.SaveChanges();

            //Ritorno la lista delle validazioni (vuote)
            return new List<ValidationResult>();
        }

        /// <summary>
        /// Modifica l'entity specificato
        /// </summary>
        /// <param name="entity">Categoria da modificare</param>
        /// <returns>Ritorna la lista delle validazioni fallite</returns>
        public IList<ValidationResult> Modifica(Categoria entity)
        {
            //Validazione argomento
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            //Eseguo la validazione dell'entità: se non passo, esco
            var validations = ValidationUtils.Validate(entity);
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
        public Categoria GetById(int id)
        {
            //Semplicemente ritorno la lista sul Context
            return Context.Categorie
                .SingleOrDefault(e => e.Id == id);
        }

        /// <summary>
        /// Ritorna la lista di tutti gli elementi
        /// </summary>
        /// <returns>Ritorna un elenco</returns>
        public IList<Categoria> Fetch()
        {
            //Ritorno la lista ordinata per nome
            return Context.Categorie
                .OrderBy(e => e.Nome)
                .ToList();
        }
    }
}
