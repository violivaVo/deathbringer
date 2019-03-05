using DeathBringer.Api.Controllers.Common;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace DeathBringer.Api.Controllers
{
    /// <summary>
    /// Controller for diagnostics of the system
    /// </summary>
    [Route("api/Diagnostics")]
    public class DiagnosticsController : ApiControllerBase
    {
        /// <summary>
        /// Generates a "server running" message on page
        /// </summary>
        /// <returns>Returns message</returns>
        /// <response code="200">Ok</response>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult Get()
        {
            //Recupero le informazioni sulla versione applicativa
            var version = string.Format("v{0}.{1}.{2}",
                Assembly.GetExecutingAssembly().GetName().Version.Major,
                Assembly.GetExecutingAssembly().GetName().Version.Minor,
                Assembly.GetExecutingAssembly().GetName().Version.Build);

            //Compongo la stringa di output
            string output = string.Format("{0} {1} is running...",
                Assembly.GetExecutingAssembly().GetName().Name,
                version);

            //Ritorno il contenuto
            return Ok(output);
        }
    }
}
