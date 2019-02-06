using System;
using System.Collections.Generic;
using System.Text;
using DeathBringer.Terminal.Entities;
using DeathBringer.Terminal.Enums;

namespace DeathBringer.Terminal.BaseClasses
{
    public class Carrello : EntityBase
    {
        public double PrezzoTotale { get; set; }
        public Utente Proprietario { get; set; }
        public Stato StatoCorrente { get; set; }
        public DateTime DataCreazione { get; set; }
        public DateTime DataPagamento { get; set; }
        public DateTime DataSpedizione { get; set; }
        public DateTime DataConsegna { get; set; }
        public DateTime DataAnnullamento { get; set; }



            }
}
