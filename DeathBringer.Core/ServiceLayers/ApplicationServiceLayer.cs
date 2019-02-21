using DeathBringer.Core.Data;
using DeathBringer.Terminal.BaseClasses;
using DeathBringer.Terminal.Data;
using DeathBringer.Terminal.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DeathBringer.Core.ServiceLayers
{
    public class ApplicationServiceLayer
    {
        public event EventHandler<string> CategorieSaved;
        public event EventHandler<string> UtentiSaved;
        public event EventHandler<string> ProdottiSaved;

        public IUtenteRepository UtenteRepository;

        public ApplicationServiceLayer()
        {
            UtenteRepository = DependencyInjectionContainer
                .Resolve<IUtenteRepository>();

            //Aggancio il delegato sull'evento
            ApplicationStorage.DatabaseSaved += OnDatabaseSaved;
        }

        /// <summary>
        /// Esegue l'autenticazione dell'utente con le credenziali
        /// ed emette l'utente se è andata a buon fine
        /// </summary>
        /// <param name="userName">Username</param>
        /// <param name="password">Password</param>
        /// <returns>Ritorna l'utente o null</returns>
        public Utente SignIn(string userName, string password)
        {
            //Validazione argomenti
            if (string.IsNullOrEmpty(userName)) throw new ArgumentNullException(nameof(userName));
            if (string.IsNullOrEmpty(password)) throw new ArgumentNullException(nameof(password));

            //Recupero l'utente usando username
            var user = GetUserByUsername(userName);

            //Non ho un utente, esco
            if (user == null)
                return null;

            //Se ho l'utente, controllo la password
            if (user.Password != password)
                return null;

            //Ritorno l''utente autenticato
            return user;
        }

        /// <summary>
        /// Ritorna un'utente sulla base del suo username
        /// </summary>
        /// <param name="userName">Username</param>
        /// <returns>Ritorna utente o null</returns>
        public Utente GetUserByUsername(string userName)
        {
            //Estrazione di tutti gli utenti
            var utenti = FetchUtenti();

            //Ritorno l'utente con username indicato
            return utenti.SingleOrDefault(u => u.Username == userName);
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


        /// <summary>
        /// Recupera lista delle categorie
        /// </summary>
        /// <returns></returns>
        public IList<Categoria> FetchCategorie()
        {
            //Carico dal disco
            ApplicationStorage.LoadCategorie();

            //Ritorno semplicemente il contenuto dell'archivio
            return ApplicationStorage.Categorie
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

            //Carico dal disco
            ApplicationStorage.LoadCategorie();

            //Prendo l'unico elemento con id specificato
            return ApplicationStorage.Categorie
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

            //Carico dal disco
            ApplicationStorage.LoadCategorie();

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

            //Salvo sul disco
            ApplicationStorage.SaveCategorie();

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

            //Carico dal disco
            ApplicationStorage.LoadCategorie();

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

            //Salvo sul disco
            ApplicationStorage.SaveCategorie();

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
            //Carico dal disco
            ApplicationStorage.LoadCategorie();

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

            //Salvo sul disco
            ApplicationStorage.SaveCategorie();

            //Mando in uscita le validazioni (VUOTE) per segnalare che è tutto ok
            return validations;
        }

        /// <summary>
        /// Ritorna una lista di tutti gli utenti del database
        /// </summary>
        /// <returns>Ritorna lista</returns>
        public IList<Utente> FetchUtenti()
        {
            return UtenteRepository.FetchAllUtenti();
        }

        public IList<ValidationResult> CreaUtente(string username,
            string password, string nome, string cognome, string email)
        {
            //TODO Aggiungere altre proprietà

            //Creo l'utente
            Utente entity = new Utente
            {
                Username = username,
                Password = password,
                Nome = nome,
                Cognome = cognome, 
                Email = email
            };

            //Creo usando il repository
            var validations = UtenteRepository.Crea(entity);
            return validations;
        }
    }
}
