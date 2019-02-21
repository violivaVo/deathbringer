using DeathBringer.Core.ServiceLayers;
using DeathBringer.Mvc.Models.Home;
using DeathBringer.Mvc.Models.Prodotti;
using DeathBringer.Terminal.BaseClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DeathBringer.Mvc.Controllers
{
    public class ProdottiController: Controller
    {
        private const string ProdottiCacheKey = "ProdottiInMemoria";
        private IMemoryCache Cache { get; }

        public ProdottiController(IMemoryCache cache)
        {
            //Imposto l'accesso alla cache
            Cache = cache;
        }

        public IActionResult Index()
        {
            //Predisposizione della lista dei prodotti
            IList<Prodotto> prodottiFromDatabase = null;

            //Se ho una lista in cache, uso quella
            var hoRecuperatoDaCache = Cache.TryGetValue(ProdottiCacheKey, out prodottiFromDatabase);

            //Se non ho prelevato da cache, prelevo da database
            if (!hoRecuperatoDaCache)
            {
                //Istanza del layer di servizio per i prodotti
                ProdottiServiceLayer layer = new ProdottiServiceLayer();

                //Recupero la lista dei prodotti dal database
                prodottiFromDatabase = layer.FetchProdotti();

                //metto in cache i prodotti scaricati dal database
                Cache.Set(ProdottiCacheKey, prodottiFromDatabase);
            }

            //NOTA: Con .NET "Classic" userei "HttpContext.Cache["ProdottiInMemoria"]"

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
