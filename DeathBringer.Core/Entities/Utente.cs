using System;
using System.Collections.Generic;
using System.Text;
using DeathBringer.Terminal.BaseClasses;

namespace DeathBringer.Terminal.Entities
{
    public class Utente: EntityBase
    {
        public string Username { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Email { get; set; }
        public string Indirizzo { get; set; }
        public string Civico { get; set; }
        public int Cap { get; set; }
        public string Citta { get; set; }
        public string Password { get; set; }
        public bool IsAdministrator { get; set; }
    }
}
