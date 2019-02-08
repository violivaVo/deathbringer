﻿using DeathBringer.Terminal.BaseClasses;
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
            if (id <= 0) return null;
            return ApplicationStorage.Prodotti.SingleOrDefault(n => n.Id == id);
        }

        public IList<ValidationResult> InsertProdotto(string name, string descr)
        {
            IList<ValidationResult> validations = new List<ValidationResult>();
            if (string.IsNullOrWhiteSpace(name))
            {
                validations.Add(new ValidationResult($"Il nome è obbligatorio"));
                return validations;
            }
            var nuovoProdotto = new Prodotto
            {
                Id = GeneratoreId.GeneraNuovoIdentificatore<Prodotto>(ApplicationStorage.Prodotti),
                Nome = name,
                Descrizione = descr
            };
            ApplicationStorage.Prodotti.Add(nuovoProdotto);
            return validations;
        }

        public IList<ValidationResult> UpdateProdotto(int id, string name, string description)
        {
            IList<ValidationResult> validations = new List<ValidationResult>();

            var Prodotto = GetProdotto(id);

            if (Prodotto == null)
            {
                validations.Add(new ValidationResult("Il codice del prodotto inserito non è valido !"));
                return validations;
            }
            else if (string.IsNullOrWhiteSpace(name))
            {
                validations.Add(new ValidationResult("Il nome non è valido !"));
                return validations;
            }

            Prodotto.Nome = name;
            Prodotto.Descrizione = description;

            return validations;
        }


        public IList<ValidationResult> DeleteProdotto(int id)
        {
            List<ValidationResult> validations = new List<ValidationResult>();
            var prodottoEsistente = GetProdotto(id);
            if (prodottoEsistente == null)
            {
                validations.Add(new ValidationResult("id non trovato"));
                return validations;
            }
            ApplicationStorage.Prodotti.Remove(prodottoEsistente);
            return validations;

        }
    }
}