using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeathBringer.Mvc.Models.Home
{
    public class IndexModel
    {
        /// <summary>
        /// Lista dei prodotti nella view
        /// </summary>
        public List<RigaProdottoModel> ListaProdotti { get; set; }
    }
}
