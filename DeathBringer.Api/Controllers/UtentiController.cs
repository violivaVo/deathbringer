﻿using Deathbringer.Contracts.Requests;
using DeathBringer.Api.Controllers.Common;
using DeathBringer.Api.Helpers;
using DeathBringer.Api.Models;
using DeathBringer.Terminal.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace DeathBringer.Api.Controllers
{
    /// <summary>
    /// Controller per la gestione degli utenti
    /// </summary>
    [Authorize]
    [Route("api/Utenti")]
    public class UtentiController: ApiControllerBase
    {
        /// <summary>
        /// Ritorna la lista degli utenti dell'applicazione
        /// </summary>
        /// <returns>Ritorna una response</returns>        
        [Obsolete("Utilizza api/Users/FetchAll")]
        [HttpPost]
        [Route("FetchAllUtenti")]
        [ProducesResponseType(200, Type = typeof(IList<UserContract>))]
        public IActionResult FetchAllUtenti()
        {
            //Estrazione da base dati
            var entities = Layer.FetchUtenti();

            //Generazione dei contratti
            IList<UserContract> contracts = new List<UserContract>();
            foreach (var currentEntity in entities)
                contracts.Add(ContractUtils.GenerateContract(currentEntity));
            
            //Emissione dei contratti in formato JSON
            return Ok(contracts);

            //Questa è la chiamata locale a cui corrisponderà
            //la action corrente presente sul controller
            //https://localhost:12345/api/Utenti/FetchAllUtenti
        }

        /// <summary>
        /// Ritorna i dati del singolo utente sulla base dell'id
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Ritorna una response</returns>
        [Obsolete("Utilizza api/Users/GetUser")]
        [HttpPost]
        [Route("GetUtente")]
        [ProducesResponseType(200, Type = typeof(UserContract))]
        public IActionResult GetUtente([FromBody]UserRequest request)
        {
            //Validazione argomenti
            if (request == null)
                return BadRequest();

            //Validazione del model binding
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Tento il recupero dell'utente dallo storage
            if (request.UserId == null)
                throw new InvalidProgramException("L'id dell'utente non è valido");
            Utente entity = Layer.GetUtenteById(request.UserId.Value);

            //Se non lo trovo, not found (404)
            if (entity == null)
                return NotFound();

            //Serializzo l'oggetto e ritorno ok (200)
            return Ok(ContractUtils.GenerateContract(entity));
        }

        /// <summary>
        /// Ritorna l'utente sulla base dello username
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Ritorna una response</returns>
        [Obsolete("Utilizza api/Users/GetUserByUserName")]
        [HttpPost]
        [Route("GetUtenteByUserName")]
        [ProducesResponseType(typeof(UserContract), 200)]
        public IActionResult GetUtenteByUserName([FromBody]UserByUserNameRequest request)
        {
            //Validazione argomenti
            if (request == null)
                return BadRequest();

            //Se non è valida
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Tento il recupero dal service layer
            Utente entity = Layer.GetUtenteByUserName(request.UserName);
            if (entity == null)
                return NotFound();

            //Ritorno il contratto
            return Ok(ContractUtils.GenerateContract(entity));
        }

        /// <summary>
        /// Creo un nuovo utente 
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Ritorna una response</returns>
        [Obsolete("Utilizza api/Users/CreateUser")]
        [HttpPost]
        [Route("CreateUtente")]
        [ProducesResponseType(200, Type = typeof(UserContract))]
        public IActionResult UpdateUtente([FromBody]CreateUserRequest request)
        {
            //Validazione argomenti
            if (request == null)
                return BadRequest();

            //Validazione del model binding
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Creazione nuovo utente 
            var entity = new Utente();
            entity.UserName = request.UserName;
            entity.Nome = request.Name;
            entity.Cognome = request.Surname;
            entity.Email = request.Email;
            entity.Indirizzo = request.Address;
            entity.Civico = request.CivicNumber;
            entity.Cap = request.ZipCode == null ? 0 : request.ZipCode.Value;
            entity.Citta = request.City;
            entity.IsAdministrator = entity.IsAdministrator;

            //Salvataggio dell'entità
            var validations = Layer.CreaUtente(entity);
            if (validations.Count > 0)
                return BadRequest(validations);

            //Serializzo e ritorno ok (200)
            return Ok(ContractUtils.GenerateContract(entity));
        }

        /// <summary>
        /// Modifica un utente esistente
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Ritorna una response</returns>
        [Obsolete("Utilizza api/Users/UpdateUser")]
        [HttpPost]
        [Route("UpdateUtente")]
        [ProducesResponseType(200, Type = typeof(UserContract))]
        public IActionResult UpdateUtente([FromBody]UpdateUserRequest request)
        {
            //Validazione argomenti
            if (request == null)
                return BadRequest();

            //Validazione del model binding
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Tento il recupero dell'utente dallo storage
            if (request.UserId == null)
                throw new InvalidProgramException("L'id dell'utente non è valido");
            Utente entity = Layer.GetUtenteById(request.UserId.Value);
            if (entity == null)
                return NotFound();

            //Aggiornamento dell'entità corrente
            entity.Nome = request.Name;
            entity.Cognome = request.Surname;
            entity.Email = request.Email;
            entity.Indirizzo = request.Address;
            entity.Civico = request.CivicNumber;
            entity.Cap = request.ZipCode == null ? 0 : request.ZipCode.Value;
            entity.Citta = request.City;
            entity.IsAdministrator = entity.IsAdministrator;

            //Salvataggio dell'entità
            var validations = Layer.ModificaUtente(entity);
            if (validations.Count > 0)
                return BadRequest(validations);

            //Serializzo e ritorno ok (200)
            return Ok(ContractUtils.GenerateContract(entity));
        }
    }
}
