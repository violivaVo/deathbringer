using DeathBringer.Api.Helpers;
using DeathBringer.Api.Models;
using DeathBringer.Api.Models.Requests;
using DeathBringer.Core.ServiceLayers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeathBringer.Api.Controllers
{
    /// <summary>
    /// Controller per l'autenticazione
    /// </summary>
    [Route("api/Authentication")]
    public class AuthenticationController: Controller
    {
        /// <summary>
        /// Esegue il sign-in alla piattaforma usando le credenziali
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Ritorna una response</returns>
        [HttpPost]
        [Route("SignIn")]
        [ProducesResponseType(200, Type = typeof(UtenteContract))]
        public IActionResult SignIn([FromBody]SignInRequest request)
        {
            //Se la request è nulla, esci
            if (request == null)
                return BadRequest();

            //Verifica se i dati inviati sono stati compilati
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Interazione con il service layer
            ApplicationServiceLayer layer = new ApplicationServiceLayer();
            var utenteAutenticato = layer.SignIn(request.UserName, request.Password);

            //Se non l'ho trovato, Unauthorized
            if (utenteAutenticato == null)
                return Unauthorized();

            //Generazione contratto e conferm
            var contract = ContractUtils.GenerateContract(utenteAutenticato);
            return Ok(contract);
        }
    }
}
