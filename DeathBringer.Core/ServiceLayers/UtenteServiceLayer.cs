using DeathBringer.Terminal.Data;
using DeathBringer.Terminal.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace DeathBringer.Core.ServiceLayers
{
    class UtenteServiceLayer
    {
        public IList<Utente> FetchUtenti()
        {
            return ApplicationStorage.Utenti
                .OrderBy(e => e.Nome)
                .ThenBy(e => e.Indirizzo)
                .ToList();
        }
        public Utente GetUtente(int id)
        {
            //Validazione argomento
            if (id <= 0)
                return null;

            //Prendo l'unico elemento con id specificato
            return ApplicationStorage.Utenti
                .SingleOrDefault(e => e.Id == id);
        }
        public IList<ValidationResult> UpdateUtente(int id, string username, string password, string nome, string cognome,
            string email, string citta, int cap, string indirizzo, string civico)
        {
            //Cerco l'elemento in archivio
            var utenteEsistente = GetUtente(id);

            //Preparo la lista vuota che è simbolo di successo dell'operazione
            IList<ValidationResult> validations = new List<ValidationResult>();
            
            //Non ho trovato nulla
            if (utenteEsistente == null)
            {
                //Aggiungo il messaggio con la spiegazione ed esco
                validations.Add(new ValidationResult($"L'utente {username} non esiste"));
                return validations;
            }

            //Se i campi richiesti non sono compilati
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(nome)
                || string.IsNullOrWhiteSpace(cognome) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(citta) || string.IsNullOrWhiteSpace(indirizzo) ||
                string.IsNullOrWhiteSpace(civico) || cap<=0 || cap>=100000)
            {
                //Aggiungo il messaggio con la spiegazione ed esco
                validations.Add(new ValidationResult("Hai mancato un campo di inserimento "));
                return validations;
            }
            utenteEsistente.Username = username;
            utenteEsistente.Password = password;
            utenteEsistente.Nome = nome;
            utenteEsistente.Cognome = cognome;
            utenteEsistente.Email = email;
            utenteEsistente.Citta = citta;
            utenteEsistente.Cap = cap;
            utenteEsistente.Indirizzo = indirizzo;
            utenteEsistente.Civico = civico;
                            
                //Mando in uscita le validazioni (VUOTE) per segnalare che è tutto ok
            return validations;

        }
    }
}
