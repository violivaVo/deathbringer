using DeathBringer.Core.Data;
using DeathBringer.Core.Data.Helpers;
using DeathBringer.Terminal.BaseClasses;
using DeathBringer.Terminal.Data;
using DeathBringer.Terminal.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DeathBringer.Core.ServiceLayers
{
    public class ApplicationServiceLayer: IDisposable
    {
        #region Fields privati
        private bool _IsDisposed = false;
        private IUtenteRepository _UtenteRepository;
        private ICategoriaRepository _CategoriaRepository;
        #endregion

        #region Eventi pubblici
        public event EventHandler<string> CategorieSaved;
        public event EventHandler<string> UtentiSaved;
        public event EventHandler<string> ProdottiSaved;
        #endregion

        /// <summary>
        /// Costruttore
        /// </summary>
        public ApplicationServiceLayer()
        {
            //Inizializzazione dei repository
            _UtenteRepository = DependencyInjectionContainer.Resolve<IUtenteRepository>();
            _CategoriaRepository = DependencyInjectionContainer.Resolve<ICategoriaRepository>();

            //LEGACY Aggancio il delegato sull'evento
            //ApplicationStorage.DatabaseSaved += OnDatabaseSaved;
        }

        #region Categoria

        /// <summary>
        /// Ritorna una categoria sulla base dell'id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Ritorna una entity o null</returns>
        public Categoria GetCategoriaById(int id)
        {
            //Utilizzo il metodo sul repository
            return _CategoriaRepository.GetById(id);
        }

        /// <summary>
        /// Ritorna una lista di tutte le categorie
        /// </summary>
        /// <returns>Ritorna lista</returns>
        public IList<Categoria> FetchCategorie()
        {
            //Estraggo direttamente dal repository
            return _CategoriaRepository.Fetch();
        }

        /// <summary>
        /// Crea una categoria sullo storage
        /// </summary>
        /// <param name="entity">Categoria da creare</param>
        /// <returns>Ritorna la lista di validazioni</returns>
        public IList<ValidationResult> CreaCategoria(Categoria entity)
        {
            //Validazione argomenti
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            //Validazione
            var validations = ValidationUtils.Validate(entity);
            if (validations.Count > 0)
                return validations;

            //Creo usando il repository
            validations = _CategoriaRepository.Crea(entity);
            if (validations.Count > 0)
                return validations;

            //Se ho l'evento lo sollevo
            CategorieSaved?.Invoke(this, entity.Nome);

            //Ritorno le validazioni
            return validations;
        }

        /// <summary>
        /// Modifica una categoria sullo storage
        /// </summary>
        /// <param name="entity">Categoria da modificare</param>
        /// <returns>Ritorna la lista di validazioni</returns>
        public IList<ValidationResult> ModificaCategoria(Categoria entity)
        {
            //Validazione argomenti
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            //Validazione
            var validations = ValidationUtils.Validate(entity);
            if (validations.Count > 0)
                return validations;

            //Modifica usando il repository
            validations = _CategoriaRepository.Modifica(entity);
            if (validations.Count > 0)
                return validations;

            //Se ho l'evento lo sollevo
            CategorieSaved?.Invoke(this, entity.Nome);

            //Ritorno le validazioni
            return validations;
        }

        /// <summary>
        /// Elimina una categoria sullo storage
        /// </summary>
        /// <param name="entity">Categoria da eliminare</param>
        /// <returns>Ritorna la lista di validazioni</returns>
        public IList<ValidationResult> EliminaCategoria(Categoria entity)
        {
            //Validazione argomenti
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            //TODO Qui si potrebbe mettere una verifica per
            //determinare se la categoria può essere eliminata
            //sulla base del suo utilizzo nei prodotti a catalogo

            //Creo usando il repository
            var validations = _CategoriaRepository.Elimina(entity);
            if (validations.Count > 0)
                return validations;

            //Se ho l'evento lo sollevo
            CategorieSaved?.Invoke(this, entity.Nome);

            //Ritorno le validazioni
            return validations;
        }
        #endregion

        #region Utente

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
            var user = GetUtenteByUserName(userName);

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
        public Utente GetUtenteByUserName(string userName)
        {
            //Estrazione di tutti gli utenti
            var utenti = FetchUtenti();

            //Ritorno l'utente con username indicato
            return utenti.SingleOrDefault(u => u.UserName == userName);
        }

        /// <summary>
        /// Ritorna un utente sulla base dell'id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Ritorna una entity o null</returns>
        public Utente GetUtenteById(int id)
        {
            //Utilizzo il metodo sul repository
            return _UtenteRepository.GetById(id);
        }

        /// <summary>
        /// Ritorna una lista di tutti gli utenti del database
        /// </summary>
        /// <returns>Ritorna lista</returns>
        public IList<Utente> FetchUtenti()
        {
            //Estraggo direttamente dal repository
            return _UtenteRepository.FetchAllUtenti();
        }

        /// <summary>
        /// Creo un utente sullo storage
        /// </summary>
        /// <param name="utente">Utente da creare</param>
        /// <returns>Ritorna la lista di validazioni</returns>
        public IList<ValidationResult> CreaUtente(Utente utente)
        {
            //Validazione argomenti
            if (utente == null) throw new ArgumentNullException(nameof(utente));

            //Validazione
            var validations = ValidationUtils.Validate(utente);
            if (validations.Count > 0)
                return validations;

            //Creo usando il repository
            validations = _UtenteRepository.Crea(utente);
            if (validations.Count > 0)
                return validations;

            //Se ho l'evento lo sollevo
            UtentiSaved?.Invoke(this, utente.UserName);

            //Ritorno le validazioni
            return validations;
        }

        /// <summary>
        /// Modifica un utente sullo storage
        /// </summary>
        /// <param name="utente">Utente da modificare</param>
        /// <returns>Ritorna la lista di validazioni</returns>
        public IList<ValidationResult> ModificaUtente(Utente utente)
        {
            //Validazione argomenti
            if (utente == null) throw new ArgumentNullException(nameof(utente));

            //Validazione
            var validations = ValidationUtils.Validate(utente);
            if (validations.Count > 0)
                return validations;

            //Modifico usando il repository
            validations = _UtenteRepository.Modifica(utente);
            if (validations.Count > 0)
                return validations;

            //Se ho l'evento lo sollevo
            UtentiSaved?.Invoke(this, utente.UserName);

            //Ritorno le validazioni
            return validations;
        }

        /// <summary>
        /// Elimina un utente sullo storage
        /// </summary>
        /// <param name="utente">Utente da eliminare</param>
        /// <returns>Ritorna la lista di validazioni</returns>
        public IList<ValidationResult> EliminaUtente(Utente utente)
        {
            //Validazione argomenti
            if (utente == null) throw new ArgumentNullException(nameof(utente));

            //TODO Qui ci dovrebbe essere una logica di business
            //che definisce se l'utente è già usato in qualche
            //flusso e quindi non può essere cancellato

            //Elimino usando il repository
            var validations = _UtenteRepository.Elimina(utente);
            if (validations.Count > 0)
                return validations;

            //Se ho l'evento lo sollevo
            UtentiSaved?.Invoke(this, utente.UserName);

            //Ritorno le validazioni
            return validations;
        }

        /// <summary>
        /// Esegue la registrazione di un nuovo utente
        /// </summary>
        /// <param name="userName">Username</param>
        /// <param name="password">Password</param>
        /// <param name="confirmPassword">Conferma password</param>
        /// <param name="email">Email</param>
        /// <param name="nome">Nome</param>
        /// <param name="cognome">Cognome</param>
        /// <returns>Ritorna le validazioni</returns>
        public IList<ValidationResult> Register(string userName, string password, 
            string confirmPassword, string email, string nome, string cognome)
        {
            //Predisposizione validazioni
            IList<ValidationResult> validations = new List<ValidationResult>();

            //Verifico se le password e conferma sono uguali
            if (password != confirmPassword)
            {
                //Aggiunta validazione ed uscita
                validations.Add(new ValidationResult("Password and confirm are not equals"));
                return validations;
            }

            //Cerco l'utente con lo stesso username
            var existing = GetUtenteByUserName(userName);
            if (existing != null)
            {
                //Aggiunta validazione ed uscita
                validations.Add(new ValidationResult("Another user exists with the same username"));
                return validations;
            }

            //Creazione utente
            Utente user = new Utente
            {
                UserName = userName,
                Password = password,
                Email = email,
                Nome = nome,
                Cognome = cognome
            };

            //Creazione del record
            validations = CreaUtente(user);

            //Ritorno le validazioni
            return validations;
        }

        #endregion

        #region Versione ApplicationStorage su FileSystem

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
        public IList<Categoria> LegacyFetchCategorie()
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
        public Categoria LegacyGetCategoria(int id)
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
        public IList<ValidationResult> LegacyInsertCategoria(string name, string description)
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
        public IList<ValidationResult> LegacyUpdateCategoria(int id, string name, string description)
        {
            //Preparo la lista vuota che è simbolo di successo dell'operazione
            IList<ValidationResult> validations = new List<ValidationResult>();

            //Carico dal disco
            ApplicationStorage.LoadCategorie();

            //Recupero della categoria esistente
            var categoriaEsistente = LegacyGetCategoria(id);

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
        public IList<ValidationResult> LegacyDeleteCategoria(int id)
        {
            //Carico dal disco
            ApplicationStorage.LoadCategorie();

            //Cerco l'elemento in archivio
            var categoriaEsistente = LegacyGetCategoria(id);

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

        #endregion

        #region Disposable pattern

        /// <summary>
        /// Esegue il rilascio della classe corrente
        /// </summary>
        /// <param name="disposing">Rilascio manuale</param>
        protected virtual void Dispose(bool disposing)
        {
            //Se non è già stato rilasciato
            if (!_IsDisposed)
            {
                //Se sto rilasciando manualmente
                if (disposing)
                {
                    //Rilascio i repository
                    //TODO Quando i repository saranno "disposable"
                }

                //Marco come rilasciato manualmente
                _IsDisposed = true;
            }
        }

        /// <summary>
        /// Distruttore
        /// </summary>
        ~ApplicationServiceLayer()
        {
            //Segnalo che la classe è stata disposta dal CG
            Dispose(false);
        }

        /// <summary>
        /// Esegue il rilascio delle risorse locali
        /// </summary>
        public void Dispose()
        {
            //Rilascio manualmente
            Dispose(true);
            
            //Evito che il GC rilasci la classe corrente
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
