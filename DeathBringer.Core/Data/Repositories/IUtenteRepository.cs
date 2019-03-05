using DeathBringer.Terminal.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DeathBringer.Core.Data
{
    /// <summary>
    /// Interfaccia per un repository di utente
    /// </summary>
    public interface IUtenteRepository
    {
        /// <summary>
        /// Ritorna la lista di tutti gli utenti nel sistema
        /// </summary>
        /// <returns>Ritorna la lista degli utenti</returns>
        IList<Utente> FetchAllUtenti();

        /// <summary>
        /// Ritorna un elemento corrispondente all'id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Ritorna un elemento o null</returns>
        Utente GetById(int id);

        /// <summary>
        /// Eseguo la creazione di un utente
        /// </summary>
        /// <param name="utente">Utente</param>
        /// <returns>Ritorna la lista delle validazioni fallite</returns>
        IList<ValidationResult> Crea(Utente utente);

        /// <summary>
        /// Modifica l'utente specificato
        /// </summary>
        /// <param name="utente">Utente da modificare</param>
        /// <returns>Ritorna la lista delle validazioni fallite</returns>
        IList<ValidationResult> Modifica(Utente utente);

        /// <summary>
        /// Elimina l'utente specificato
        /// </summary>
        /// <param name="utente">Utente da eliminare</param>
        /// <returns>Ritorna la lista delle validazioni fallite</returns>
        IList<ValidationResult> Elimina(Utente utente);        
    }
}
