using System.ComponentModel.DataAnnotations;
using DeathBringer.Terminal.BaseClasses;

namespace DeathBringer.Terminal.Entities
{
    public class Utente: EntityBase
    {
        [Required]
        [StringLength(255)]
        public string Username { get; set; }

        [Required]
        [StringLength(255)]
        public string Nome { get; set; }

        [Required]
        [StringLength(255)]
        public string Cognome { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        public string Indirizzo { get; set; }

        public string Civico { get; set; }

        public int Cap { get; set; }

        public string Citta { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        [Required]
        public bool IsAdministrator { get; set; }
    }
}
