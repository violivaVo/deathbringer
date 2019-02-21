using DeathBringer.Core.ServiceLayers;
using DeathBringer.Mvc.Models.Utenti;
using DeathBringer.Terminal.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeathBringer.Mvc.Controllers
{
    public class UtentiController : Controller
    {
        [HttpGet]
        public IActionResult Crea()
        {
            CreaUtenteModel model = new CreaUtenteModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Crea(CreaUtenteModel model)
        {
            if (string.IsNullOrEmpty(model.Username))
            {
                //Imposto il valore di NON validità ed esco
                model.IsValid = false;
                return View(model);
            }

            if (string.IsNullOrEmpty(model.Password))
            {
                //Imposto il valore di NON validità ed esco
                model.IsValid = false;
                return View(model);
            }
            if (string.IsNullOrEmpty(model.Nome))
            {
                //Imposto il valore di NON validità ed esco
                model.IsValid = false;
                return View(model);
            }
            if (string.IsNullOrEmpty(model.Cognome))
            {
                //Imposto il valore di NON validità ed esco
                model.IsValid = false;
                return View(model);
            }
            if (string.IsNullOrEmpty(model.Email))
            {
                //Imposto il valore di NON validità ed esco
                model.IsValid = false;
                return View(model);
            }

            ApplicationServiceLayer layer = new ApplicationServiceLayer();

            //Creo sul database
            var validations = layer.CreaUtente(
                model.Username, model.Password, 
                model.Nome, model.Cognome, model.Email);
           
            //Istanza del layer di servizio per i prodotti
            //UtenteServiceLayer layer = new UtenteServiceLayer();

            ////Inserisco il prodotto sul layer di servizio
            //IList<ValidationResult> validazioni = layer
            //    .InsertUtente(model.Nome, model.Cognome, model.Username, model.Password);

            //Altrimenti renderizzo
            model.IsValid = true;
            return View(model);
        }

        public IActionResult Index()
        {
            //Istanza del layer di servizio per i prodotti
            UtenteServiceLayer layer = new UtenteServiceLayer();

            //Recupero la lista dei prodotti dal database
            IList<Utente> utentiFromDatabase = layer.FetchUtenti();

            //inizializziamo il modello per il ModelBinder di ASP.NET
            UtentiModel model = new UtentiModel();
            model.ListaUtenti = new List<RigaUtentiModel>();

            //Scorro tutte le entità e creo i modelli per la UI
            foreach (var currentEntity in utentiFromDatabase)
            {
                var currentModel = new RigaUtentiModel
                {
                    Username = currentEntity.Username,
                    Nome = currentEntity.Nome,
                    Cognome = currentEntity.Cognome,
                    Email = currentEntity.Email,
                    Indirizzo = currentEntity.Indirizzo,
                    Civico = currentEntity.Civico,
                    Cap = currentEntity.Cap,
                    Citta = currentEntity.Citta,
                    Password = currentEntity.Password,
                    IsAdministrator = currentEntity.IsAdministrator
                };

                //Aggiunta del modello creato alla lista
                model.ListaUtenti.Add(currentModel);
            }

            //Renderizzazione del modello
            return View(model);
        }
    }
}
