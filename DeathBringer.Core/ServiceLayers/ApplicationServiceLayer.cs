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
        /// <summary>
        /// Recupera lista delle categorie
        /// </summary>
        /// <returns></returns>
        public IList<Categoria> FetchCategorie()
        {
            //Ritorno semplicemente il contenuto dell'archivio
            return ApplicationStorage.Utente
                .OrderBy(e => e.Nome)
                .ThenBy(e => e.Descrizione)
                .ToList();
        }

        /// <summary>
        /// Recupera una singla categoria per id
        /// </summary>
        /// <param name="id">Id categoria</param>
        /// <returns>Ritorna la categoria o null</returns>
        public Categoria GetCategoria(int id)
        {
            //Validazione argomento
            if (id <= 0)
                return null;

            //Prendo l'unico elemento con id specificato
            return ApplicationStorage.Utente                
                .SingleOrDefault(e => e.Id == id);
        }

        /// <summary>
        /// Inserisce una categoria e ritorna le validazioni
        /// </summary>
        /// <param name="name">Nome</param>
        /// <param name="description">Descrizione</param>
        /// <returns>Ritorna lista di validazioni</returns>
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
                Id = GeneratoreId.GeneraNuovoIdentificatore(ApplicationStorage.Utente), 
                Nome = name, 
                Descrizione = description, 
                DataCreazioneRecord = DateTime.Now, 
                DataUltimaModifica = DateTime.Now, 
                UtenteCreazioneRecord = "anonymous", 
                UtenteUltimaModificaRecord = "anonymous"
            };

            //Aggiunta nella lista generale
            ApplicationStorage.Utente.Add(nuovaCategoria);

            //Mando in uscita le validazioni (VUOTE) per segnalare che è tutto ok
            return validations;
        }

        /// <summary>
        /// Modifica una categoria esistente e ritorna le validazioni
        /// </summary>
        /// <param name="id">Id categoria</param>
        /// <param name="name">Nome</param>
        /// <param name="description">Descrizione</param>
        /// <returns>Ritorna lista di validazioni</returns>
        public IList<ValidationResult> UpdateCategoria(int id, string name, string description)
        {
            //Preparo la lista vuota che è simbolo di successo dell'operazione
            IList<ValidationResult> validations = new List<ValidationResult>();

            //Recupero della categoria esistente
            var categoriaEsistente = GetCategoria(id);

            //Se non esiste, messaggio
            if (categoriaEsistente == null)
            {
                //Aggiungo il messaggio con la spiegazione ed esco
                validations.Add(new ValidationResult($"La categoria {id} non esiste"));
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
            categoriaEsistente.Nome = name;
            categoriaEsistente.Descrizione = description;

            //Mando in uscita le validazioni (VUOTE) per segnalare che è tutto ok
            return validations;
        }

        /// <summary>
        /// Cancella una categoria esistente e ritorna le validazioni
        /// </summary>
        /// <param name="id">Id della categoria</param>
        /// <returns>Ritorna lista di validazioni</returns>
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
            ApplicationStorage.Utente.Remove(categoriaEsistente);

            //Mando in uscita le validazioni (VUOTE) per segnalare che è tutto ok
            return validations;
        }
    }
}
