using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeathBringer.Mvc.Models.Home
{
    public class HomeIndexModel
    {
        /// <summary>
        /// Nome dell'utente autenticato con ASP.NET Identity
        /// </summary>
        public string AuthenticatedUserName { get; set; }
    }
}
