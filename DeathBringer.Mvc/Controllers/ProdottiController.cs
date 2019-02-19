using DeathBringer.Core.ServiceLayers;
using DeathBringer.Mvc.Models.Home;
using DeathBringer.Mvc.Models.Prodotti;
using DeathBringer.Terminal.BaseClasses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeathBringer.Mvc.Controllers
{
    public class ProdottiController: Controller
    {
        public IActionResult Index()
        {
            //Istanza del layer di servizio per i prodotti
            ProdottiServiceLayer layer = new ProdottiServiceLayer();

            //Recupero la lista dei prodotti dal database
            IList<Prodotto> prodottiFromDatabase = layer.FetchProdotti();

            //inizializziamo il modello per il ModelBinder di ASP.NET
            IndexModel model = new IndexModel();
            model.ListaProdotti = new List<RigaProdottoModel>();

            //Scorro tutte le entità e creo i modelli per la UI
            foreach (var currentEntity in prodottiFromDatabase)
            {
                var currentModel = new RigaProdottoModel
                {
                    Id = currentEntity.Id,
                    Nome = currentEntity.Nome,
                    Descrizione = currentEntity.Descrizione,
                    Brand = currentEntity.Brand,
                    DataProduzione = currentEntity.DataProduzione
                };

                //Aggiunta del modello creato alla lista
                model.ListaProdotti.Add(currentModel);
            }

            //Renderizzazione del modello
            return View(model);
        }

        [HttpGet]
        public IActionResult Crea()
        {
            CreaModel model = new CreaModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Crea(CreaModel model)
        {
            //Se ho la data di oggi, valorizzo la data
            if (model.SetDataDiOggi == true)
                model.DataCreazione = DateTime.Now;

            if (string.IsNullOrEmpty(model.Nome))
            {
                //Imposto il valore di NON validità ed esco
                model.IsValid = false;
                return View(model);
            }

            if (model.DataCreazione == null)
            {
                //Imposto il valore di NON validità ed esco
                model.IsValid = false;
                return View(model);
            }

            //Istanza del layer di servizio per i prodotti
            ProdottiServiceLayer layer = new ProdottiServiceLayer();

            //Inserisco il prodotto sul layer di servizio
            IList<ValidationResult> validazioni = layer
                .InsertProdotto(model.Nome, model.Descrizione);

            //Altrimenti renderizzo
            model.IsValid = true;
            return View(model);
        }

        public IActionResult Modifica(int id)
        {
            return View();
        }
    }
}
