using System;
using System.Collections.Generic;
using System.Text;
using DeathBringer.Terminal.BaseClasses;

namespace DeathBringer.Terminal.Entities
{
    public class Prezzo : EntityBase
    {
        public double Costo { get; set; }
        public double Sconto { get; set; }
        public DateTime DataInizio { get; set; }
        public Prodotto ProdottoAssociato { get; set; }
    }
}    