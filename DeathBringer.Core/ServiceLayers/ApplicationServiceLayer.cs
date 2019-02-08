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
        public event EventHandler<string> CategorieSaved;
        public event EventHandler<string> UtentiSaved;
        public event EventHandler<string> ProdottiSaved;

        public ApplicationServiceLayer()
        {
            //Aggancio il delegato sull'evento
            ApplicationStorage.DatabaseSaved += OnDatabaseSaved;
        }

        private void OnDatabaseSaved(object sender, string e)
        {
            //Seleziono l'evento da lanciare a seconda della stringa
            switch (e)
            {
                case nameof(Categoria):
                    if (CategorieSaved != null)
                        CategorieSaved(this, "Database categorie salvato!");
                    break;
                case nameof(Utente):
                    if (UtentiSaved != null)
                        UtentiSaved(this, "Salvato utente");
                    break;
                case nameof(Prodotto):
                    if (ProdottiSaved != null)
                        ProdottiSaved(this, "Salvato prodotto");
                    break;
                default:
                    throw new NotSupportedException($"Valore {e} non supportato");
            }
        }

        public IList<Categoria> FetchCategorie()
        {
            //Ritorno semplicemente il contenuto dell'archivio
            return ApplicationStorage.Categorie
                .OrderBy(e => e.Nome)
                .ThenBy(e => e.Descrizione)
                .ToList();
        }

        /// <summary>
        /// Recupera una singola categoria per id
        /// </summary>
        /// <param name="id">Id categoria</param>
        /// <returns>Ritorna la categoria o null</returns>
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

        public IList<ValidationResult> InsertUtente(string name, string surname)
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
            var nuovoUtente = new Utente
            {
                Id = GeneratoreId.GeneraNuovoIdentificatore<Utente>(ApplicationStorage.Utenti),
                Nome = name,
                Cognome = surname,
                DataCreazioneRecord = DateTime.Now,
                DataUltimaModifica = DateTime.Now,
                UtenteCreazioneRecord = "anonymous",
                UtenteUltimaModificaRecord = "anonymous"
            };
            //Aggiunta nella lista generale
            ApplicationStorage.Utenti.Add(nuovoUtente);

            //Mando in uscita le validazioni (VUOTE) per segnalare che è tutto ok
            return validations;
        }

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

        public IList<ValidationResult> DeleteUtente(int id)
        {
            //Cerco l'elemento in archivio
            var utenteEsistente = GetUtente(id);

            //Preparo la lista vuota che è simbolo di successo dell'operazione
            IList<ValidationResult> validations = new List<ValidationResult>();

            //Non ho trovato nulla
            if (utenteEsistente == null)
            {
                //Aggiungo il messaggio con la spiegazione ed esco
                validations.Add(new ValidationResult($"La categoria {id} non esiste"));
                return validations;
            }

            //Rimozione della categoria dallo storage
            ApplicationStorage.Utenti.Remove(utenteEsistente);

            //Mando in uscita le validazioni (VUOTE) per segnalare che è tutto ok
            return validations;
        }
    }
}
