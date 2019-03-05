using System.ComponentModel.DataAnnotations;

namespace Deathbringer.Contracts.Requests
{
    /// <summary>
    /// Request per singolo utente
    /// </summary>
    public class UtenteRequest
    {
        /// <summary>
        /// Id utente
        /// </summary>
        [Required]
        public int? UtenteId { get; set; }
    }
}
