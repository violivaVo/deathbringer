using DeathBringer.Terminal.BaseClasses;
using DeathBringer.Terminal.Data;
using DeathBringer.Terminal.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace DeathBringer.Core.ServiceLayers
{
    public class ApplicationServiceLayer
    {
        public IList<Categoria> FetchCategorie()
        {
            //Ritorno semplicemente il contenuto dell'archivio
            return ApplicationStorage.Categorie
                .OrderBy(e => e.Nome)
                .ThenBy(e => e.Descrizione)
                .ToList();
        }

        public Categoria GetCategoria(int id)
        {
            //Validazione argomento
            if (id <= 0)
                return null;

            //Prendo l'unico elemento con id specificato
            return ApplicationStorage.Categorie                
                .SingleOrDefault(e => e.Id == id);
        }

        public IList<ValidationResult> InsertCategoria(string name, string description)
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
            var nuovaCategoria = new Categoria
            {
                Id = GeneratoreId.GeneraNuovoIdentificatore<Categoria>(ApplicationStorage.Categorie), 
                Nome = name, 
                Descrizione = description, 
                DataCreazioneRecord = DateTime.Now, 
                DataUltimaModifica = DateTime.Now, 
                UtenteCreazioneRecord = "anonymous", 
                UtenteUltimaModificaRecord = "anonymous"
            };

            //Aggiunta nella lista generale
            ApplicationStorage.Categorie.Add(nuovaCategoria);

            //Mando in uscita le validazioni (VUOTE) per segnalare che è tutto ok
            return validations;
        }

        public IList<ValidationResult> UpdateCategoria(int id, string name, string description)
        {
            //Quando cerco non ho nulla

            //Se i campi richiesti non sono compilati

            throw new NotImplementedException();
        }

        public IList<ValidationResult> DeleteCategoria(int id)
        {
            //Cerco l'elemento in archivio
            var categoriaEsistente = GetCategoria(id);

            //Preparo la lista vuota che è simbolo di successo dell'operazione
            IList<ValidationResult> validations = new List<ValidationResult>();

            //Non ho trovato nulla
            if (categoriaEsistente == null)
            {
                //Aggiungo il messaggio con la spiegazione ed esco
                validations.Add(new ValidationResult($"La categoria {id} non esiste"));
                return validations;
            }

            //Rimozione della categoria dallo storage
            ApplicationStorage.Categorie.Remove(categoriaEsistente);

            //Mando in uscita le validazioni (VUOTE) per segnalare che è tutto ok
            return validations;
        }

        
    }
}
