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
            throw new Exception();
        }

        public Prodotto GetProdotto(int id)
        {
            throw new Exception();
        }

        public IList<ValidationResult> InsertProdotto(string name, string description)
        {
            throw new Exception();
        }





        public IList<ValidationResult> UpdateProdotto(int id, string name, string description)
        {
            throw new Exception();
        }

        public IList<ValidationResult> DeleteProdotto(int id)
        {
            throw new Exception();
        }
    }
}
