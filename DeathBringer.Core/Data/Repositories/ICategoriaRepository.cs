using DeathBringer.Terminal.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DeathBringer.Core.Data
{
    /// <summary>
    /// Interfaccia di repository di una categoria
    /// </summary>
    public interface ICategoriaRepository
    {
        /// <summary>
        /// Ritorna la lista di tutti gli elementi
        /// </summary>
        /// <returns>Ritorna un elenco</returns>
        IList<Categoria> Fetch();

        /// <summary>
        /// Ritorna un elemento usando l'id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Ritorna un elemento o null</returns>
        Categoria GetById(int id);

        /// <summary>
        /// Eseguo la creazione di un elemento
        /// </summary>
        /// <param name="entity">Entità da creare</param>
        /// <returns>Ritorna la lista delle validazioni fallite</returns>
        IList<ValidationResult> Crea(Categoria entity);

        /// <summary>
        /// Modifica un elemento esistente
        /// </summary>
        /// <param name="entity">Elemento da modificare</param>
        /// <returns>Ritorna la lista delle validazioni fallite</returns>
        IList<ValidationResult> Modifica(Categoria entity);

        /// <summary>
        /// Elimina un elemento esistente
        /// </summary>
        /// <param name="entity">Elemento da eliminare</param>
        /// <returns>Ritorna la lista delle validazioni fallite</returns>
        IList<ValidationResult> Elimina(Categoria entity);                
    }
}
