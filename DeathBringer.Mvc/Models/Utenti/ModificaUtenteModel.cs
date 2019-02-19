using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeathBringer.Mvc.Models.Utenti
{
    public class ModificaUtenteModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Email { get; set; }
        public string Indirizzo { get; set; }
        public string Civico { get; set; }
        public int Cap { get; set; }
        public string Citta { get; set; } // i caratteri accentati non sono accettati
        public string Password { get; set; }
        public bool IsAdministrator { get; set; } //permessi utente, è un admin?

        public bool? IsModifica { get; set; }
    }
}
