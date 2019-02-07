using DeathBringer.Terminal.Data;
using DeathBringer.Terminal.Entities;
using System;
using System.Collections.Generic;
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

    }
}
