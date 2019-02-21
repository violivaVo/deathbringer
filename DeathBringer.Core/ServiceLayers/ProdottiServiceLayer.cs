using DeathBringer.Terminal.BaseClasses;
using DeathBringer.Terminal.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DeathBringer.Core.ServiceLayers
{
    public class ProdottiServiceLayer
    {        
        public IList<Prodotto> FetchProdotti()
        {
            ApplicationStorage.LoadProdotti();
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

            //Carico dal disco
            ApplicationStorage.LoadProdotti();

            //Creazione dell'oggetto (classe)
            var nuovaProdotto = new Prodotto
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
            ApplicationStorage.Prodotti.Add(nuovaProdotto);

            //Salvo sul disco
            ApplicationStorage.SaveProdotti();

            //Mando in uscita le validazioni (VUOTE) per segnalare che è tutto ok
            return validations;
        }

        public IList<ValidationResult> UpdateProdotto(int id, string name, string description)
        {
            //Preparo la lista vuota che è simbolo di successo dell'operazione
            IList<ValidationResult> validations = new List<ValidationResult>();

            //Carico dal disco
            ApplicationStorage.LoadProdotti();

            //Recupero della categoria esistente
            var prodottoEsistente = GetProdotto(id);

            //Se non esiste, messaggio
            if (prodottoEsistente == null)
            {
                //Aggiungo il messaggio con la spiegazione ed esco
                validations.Add(new ValidationResult($"il prodotto {id} non esiste"));
                return validations;
            }

            //Se il nome (che è OBBLIGATORIO) è vuoto o nullo, esco
            if (string.IsNullOrWhiteSpace(name))
            {
                //Aggiungo il messaggio con la spiegazione ed esco
                validations.Add(new ValidationResult($"Il nome è obbligatorio"));
                return validations;
            }

            //Aggiornamento entità
            prodottoEsistente.Nome = name;
            prodottoEsistente.Descrizione = description;

            //Salvo sul disco
            ApplicationStorage.SaveProdotti();

            //Mando in uscita le validazioni (VUOTE) per segnalare che è tutto ok
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
