using DeathBringer.Terminal.BaseClasses;
using DeathBringer.Terminal.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace DeathBringer.Core.ServiceLayers
{
    public  class ProdottiServiceLayer
    {
        public IList<Prodotto> FetchProdotti()
        {
            //Ritorno semplicemente il contenuto dell'archivio
            return ApplicationStorage.Prodotti
                .OrderBy(e => e.Nome)
                .ThenBy(e => e.Descrizione)
                .ToList();
        }

        public Prodotto GetProdotto(int id)
        {
            throw new NotImplementedException();
        }

        public IList<ValidationResult> InsertProdotto(string name, string description)
        {
            throw new NotImplementedException();
        }

        public IList<ValidationResult> UpdateProdotto(int id, string name, string description)
        {
            throw new NotImplementedException();
        }

        public IList<ValidationResult> DeleteProdotto(int id)
        {
            throw new NotImplementedException();
        }
    }
}
