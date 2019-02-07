﻿using DeathBringer.Terminal.BaseClasses;
using DeathBringer.Terminal.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace DeathBringer.Core.ServiceLayers
{
    public class ProdottiServiceLayer
    {
        public IList<Prodotto> FetchProdotti()
        {
            return ApplicationStorage.Prodotti
                .OrderBy(e => e.Nome)
                .ThenBy(e => e.Brand)
                .ToList();
        }

        public Prodotto GetProdotto(int id)
        {
            if (id <= 0) return null;
            return ApplicationStorage.Prodotti.SingleOrDefault(n => n.Id == id);
        }

        public IList<ValidationResult> InsertProdotto(string name, string description)
        {
            //Preparo la lista vuota che è simbolo di successo dell'operazione
            IList<ValidationResult> validations = new List<ValidationResult>();

            //Se il nome (che è OBBLIGATORIO) è vuoto o nullo, esco
            if (string.IsNullOrWhiteSpace(name))
            {
                //Aggiungo il messaggio con la spiegazione ed esco
                validations.Add(new ValidationResult($"Il nome è obbligatorio"));
                return validations;
            }

            //Creazione dell'oggetto (classe)
            var nuovoProdotto = new Prodotto
            {
                Id = GeneratoreId.GeneraNuovoIdentificatore<Prodotto>(ApplicationStorage.Prodotti),
                Nome = name,
                Descrizione = description,
                DataCreazioneRecord = DateTime.Now,
                DataUltimaModifica = DateTime.Now,
                UtenteCreazioneRecord = "anonymous",
                UtenteUltimaModificaRecord = "anonymous"
            };

            //Aggiunta nella lista generale
            ApplicationStorage.Prodotti.Add(nuovoProdotto);

            //Mando in uscita le validazioni (VUOTE) per segnalare che è tutto ok
            return validations;
        }

        public IList<ValidationResult> UpdateProdotto(int id, string name, string description)
        {
            throw new NotImplementedException();
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