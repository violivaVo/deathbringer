using DeathBringer.Terminal.BaseClasses;
using DeathBringer.Terminal.Data;
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
            List<ValidationResult> validations = new List<ValidationResult>();
            var prodottoEsistente = GetProdotto(id);
            if (prodottoEsistente==null)
            {
                validations.Add(new ValidationResult("id non trovato"));
                return validations;
            }
            ApplicationStorage.Prodotti.Remove(prodottoEsistente);
            return validations;
            throw new NotImplementedException();
        }
    }
}
