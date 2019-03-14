using DeathBringer.Core.ServiceLayers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DeathBringer.Api.Controllers.Common
{
    /// <summary>
    /// Classe astratta per i controller
    /// </summary>
    public abstract class ApiControllerBase : Controller
    {
        /// <summary>
        /// Application service layer
        /// </summary>
        protected ApplicationServiceLayer Layer { get; }

        /// <summary>
        /// Costruttore
        /// </summary>
        protected ApiControllerBase()
        {
            //Inizializzazione del layer
            Layer = new ApplicationServiceLayer();
        }

        /// <summary>
        /// Compose a BarRequest (400) with provided list of validations
        /// </summary>
        /// <param name="validations">Validations</param>
        /// <returns>Returns bad request response</returns>
        protected IActionResult BadRequest(IList<ValidationResult> validations)
        {
            //Validazione argomenti
            if (validations == null) throw new ArgumentNullException(nameof(validations));

            //Scorro tutti gli errori, inserisco nel modello ed esco
            foreach (var current in validations)
                ModelState.AddModelError("", current.ErrorMessage);

            //Ritorno la request
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Rilascia le risorse locali
        /// </summary>
        /// <param name="disposing">Sono in fase di dispose esplicito</param>
        protected override void Dispose(bool disposing)
        {
            //Se sto rilasciando
            if (disposing)
            {
                //Rilascio il layer
                Layer.Dispose();
            }

            //Dispose base
            base.Dispose(disposing);
        }
    }
}
