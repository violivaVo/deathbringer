using Deathbringer.Contracts.Results;
using DeathBringer.Api.Controllers.Common;
using DeathBringer.Api.Helpers;
using DeathBringer.Api.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DeathBringer.Api.Controllers
{
    /// <summary>
    /// Controller per l'autenticazione
    /// </summary>
    [Route("api/Authentication")]
    public class AuthenticationController: ApiControllerBase
    {
        /// <summary>
        /// Esegue il sign-in alla piattaforma usando le credenziali
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Ritorna una response</returns>
        [HttpPost]
        [Route("SignIn")]
        [ProducesResponseType(200, Type = typeof(Deathbringer.Contracts.Results.SignInResult))]
        public IActionResult SignIn([FromBody]SignInRequest request)
        {
            //Se la request è nulla, esci
            if (request == null)
                return BadRequest();

            //Verifica se i dati inviati sono stati compilati
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Interazione con il service layer
            var utenteAutenticato = Layer.SignIn(request.UserName, request.Password);

            //Se non l'ho trovato, Unauthorized
            if (utenteAutenticato == null)
                return Unauthorized();

            //Creazione della risposta
            var result = new Deathbringer.Contracts.Results.SignInResult
            {
                UserId = utenteAutenticato.Id, 
                UserName = utenteAutenticato.UserName, 
                Email = utenteAutenticato.Email, 
                Name = utenteAutenticato.Nome, 
                Surname = utenteAutenticato.Cognome, 
                IsAdministrator = utenteAutenticato.IsAdministrator, 
                LastAccessDate = DateTime.Now
            };

            //Conferma
            return Ok(result);
        }
    }
}
