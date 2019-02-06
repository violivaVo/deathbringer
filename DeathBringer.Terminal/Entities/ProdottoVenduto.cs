using System;
using System.Collections.Generic;
using System.Text;
using DeathBringer.Terminal.BaseClasses;

namespace DeathBringer.Terminal.Entities
{
    class ProdottoVenduto: EntityBase
    {
        public int Quantita { get; set; }
        public Prezzo PrezzoProdotto { get; set; }
        public Carrello CarrelloAssociato { get; set; }
    }
}
