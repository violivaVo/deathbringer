using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeathBringer.Mvc.Models.Home
{
    public class RigaProdottoModel
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public DateTime DataProduzione { get; set; }

        public string Descrizione { get; set; }

        public string Brand { get; set; }
    }
}
