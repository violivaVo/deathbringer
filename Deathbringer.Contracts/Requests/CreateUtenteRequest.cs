using System.ComponentModel.DataAnnotations;

namespace Deathbringer.Contracts.Requests
{
    /// <summary>
    /// Request per create utente
    /// </summary>
    public class CreateUtenteRequest
    {
        /// <summary>
        /// Username
        /// </summary>
        [Required]
        [StringLength(255)]
        public string UserName { get; set; }

        /// <summary>
        /// Nome
        /// </summary>
        [Required]
        [StringLength(255)]
        public string Nome { get; set; }

        /// <summary>
        /// Cognome
        /// </summary>
        [Required]
        [StringLength(255)]
        public string Cognome { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        /// <summary>
        /// Indirizzo
        /// </summary>
        [StringLength(255)]
        public string Indirizzo { get; set; }

        /// <summary>
        /// Numero civico
        /// </summary>
        [StringLength(255)]
        public string Civico { get; set; }

        /// <summary>
        /// Codice avviamento postale
        /// </summary>
        [StringLength(255)]
        public int Cap { get; set; }

        /// <summary>
        /// Città di residenza
        /// </summary>
        [StringLength(255)]
        public string Citta { get; set; }

        /// <summary>
        /// Flag amministratore
        /// </summary>
        [Required]
        public bool? IsAdministrator { get; set; }
    }
}
