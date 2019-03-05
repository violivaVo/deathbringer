using DeathBringer.Core.ServiceLayers;
using Microsoft.AspNetCore.Mvc;

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
