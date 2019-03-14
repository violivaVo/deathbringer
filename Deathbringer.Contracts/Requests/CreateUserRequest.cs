using System.ComponentModel.DataAnnotations;

namespace Deathbringer.Contracts.Requests
{
    /// <summary>
    /// Request per create utente
    /// </summary>
    public class CreateUserRequest
    {
        /// <summary>
        /// Username
        /// </summary>
        [Required]
        [StringLength(255)]
        public string UserName { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        /// <summary>
        /// Nome
        /// </summary>
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        /// <summary>
        /// Cognome
        /// </summary>
        [Required]
        [StringLength(255)]
        public string Surname { get; set; }

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
        public string Address { get; set; }

        /// <summary>
        /// Numero civico
        /// </summary>
        [StringLength(255)]
        public string CivicNumber { get; set; }

        /// <summary>
        /// Codice avviamento postale
        /// </summary>
        public int? ZipCode { get; set; }

        /// <summary>
        /// Città di residenza
        /// </summary>
        [StringLength(255)]
        public string City { get; set; }

        /// <summary>
        /// Flag amministratore
        /// </summary>
        [Required]
        public bool? IsAdministrator { get; set; }
    }
}
