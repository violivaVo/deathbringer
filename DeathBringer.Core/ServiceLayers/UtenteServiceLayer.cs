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
        public Utente UtenteEsistente { get; private set; }

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
        public IList<ValidationResult> DeleteUtente(int id)
            {
                //Cerco l'elemento in archivio
                var utenteEsistente = GetUtente(id);

                //Preparo la lista vuota che è simbolo di successo dell'operazione
                IList<ValidationResult> validations = new List<ValidationResult>();

                //Non ho trovato nulla
                if (utenteEsistente == null)
                {
                    //Aggiungo il messaggio con la spiegazione ed esco
                    validations.Add(new ValidationResult($"La categoria {id} non esiste"));
                    return validations;
                }

                //Rimozione della categoria dallo storage
                ApplicationStorage.Utenti.Remove(UtenteEsistente);

                //Mando in uscita le validazioni (VUOTE) per segnalare che è tutto ok
                return validations;
        }
        



    }
}
