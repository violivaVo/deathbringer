using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeathBringer.Api.Models
{
    public class UtenteContract
    {
        public string Username { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Email { get; set; }
        public string Indirizzo { get; set; }
        public string Civico { get; set; }
        public int Cap { get; set; }
        public string Citta { get; set; }
        public bool IsAdministrator { get; set; }
    }
}
