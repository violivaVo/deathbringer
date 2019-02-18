using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeathBringer.Mvc.Models.Prodotti
{
    public class CreaModel
    {
        public string Nome { get; set; }
        public string Descrizione { get; set; }
        public string Brand { get; set; }
        public DateTime? DataCreazione { get; set; }
        public bool SetDataDiOggi { get; set; }
        public bool? IsValid { get; set; }
    }
}
