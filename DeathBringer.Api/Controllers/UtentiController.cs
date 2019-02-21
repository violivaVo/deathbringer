using DeathBringer.Api.Helpers;
using DeathBringer.Api.Models;
using DeathBringer.Core.ServiceLayers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeathBringer.Api.Controllers
{
    [Route("api/Utenti")]
    public class UtentiController: Controller
    {
        [Authorize]
        [HttpPost]
        [Route("FetchAllUtenti")]
        public IActionResult FetchAllUtenti()
        {
            //Inizializzazione layer applicativo
            ApplicationServiceLayer layer = new ApplicationServiceLayer();

            //Estrazione da base dati
            var entities = layer.FetchUtenti();

            //Generazione dei contratti
            IList<UtenteContract> contracts = new List<UtenteContract>();
            foreach (var currentEntity in entities)
                contracts.Add(ContractUtils.GenerateContract(currentEntity));
            
            //Emissione dei contratti in formato JSON
            return Ok(contracts);

            //https://localhost:12345/api/Utenti/FetchAllUtenti
        }
    }
}
