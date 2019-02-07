using DeathBringer.Terminal.BaseClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DeathBringer.Core.ServiceLayers
{
    class ProdottiServiceLayer
    {
        public IList<Prodotto> FetchProdotti()
        {
            throw new NotImplementedException();
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
