using Deathbringer.Contracts.Requests;
using DeathBringer.Api.Controllers.Common;
using DeathBringer.Api.Helpers;
using DeathBringer.Api.Models;
using DeathBringer.Terminal.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DeathBringer.Api.Controllers
{
    /// <summary>
    /// Controller per la gestione utente
    /// </summary>
    [Route("api/Users")]
    public class UsersController: ApiControllerBase
    {
        /// <summary>
        /// Esegue l'auto-registrazione di un utente
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Ritorna una action result</returns>
        [HttpPost]
        [Route("Register")]
        [ProducesResponseType(200, Type = typeof(UserContract))]
        public IActionResult Register([FromBody] RegisterUserRequest request)
        {
            //Se la request è nulla, esci
            if (request == null)
                return BadRequest();

            //Verifica se i dati inviati sono stati compilati
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Utilizzo il service layer per la registrazione
            IList<ValidationResult> validations = Layer.Register(request.UserName, 
                request.Password, request.ConfirmPassword, 
                request.Email, request.Name, request.Surname);

            //Se ho validazioni fallite, esco
            if (validations.Count > 0)
                return BadRequest(validations);

            //Recupero l'utente per username
            var createdUser = Layer.GetUtenteByUserName(request.UserName);
            if (createdUser == null)
                throw new InvalidProgramException($"User {request.UserName} cannot be found");

            //Generazione contratto e conferm
            var contract = ContractUtils.GenerateContract(createdUser);
            return Ok(contract);
        }

        /// <summary>
        /// Ritorna la lista degli utenti dell'applicazione
        /// </summary>
        /// <returns>Ritorna una response</returns>        
        //[Authorize]
        [HttpPost]
        [Route("FetchAll")]
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
        }

        /// <summary>
        /// Recupera i dati dell'utente sulla base dell'Id
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Ritorna una response</returns>
        [HttpPost]
        [Route("GetUser")]
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
        [HttpPost]
        [Route("GetUserByUserName")]
        [ProducesResponseType(typeof(UserContract), 200)]
        public IActionResult GetUserByUserName([FromBody]UserByUserNameRequest request)
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
        /// Crea un nuovo utente 
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Ritorna una response</returns>
        [HttpPost]
        [Route("CreateUser")]
        [ProducesResponseType(200, Type = typeof(UserContract))]
        public IActionResult CreateUser([FromBody]CreateUserRequest request)
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
            entity.Password= request.Password;
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
        [HttpPost]
        [Route("UpdateUser")]
        [ProducesResponseType(200, Type = typeof(UserContract))]
        public IActionResult UpdateUser([FromBody]UpdateUserRequest request)
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
