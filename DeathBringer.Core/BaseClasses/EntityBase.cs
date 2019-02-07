using System;
using System.Collections.Generic;
using System.Text;
using DeathBringer.Terminal.Interfaces;

namespace DeathBringer.Terminal.BaseClasses
{
    public abstract class EntityBase: IEntity
    {
        public int Id { get; set; }
        public DateTime DataCreazioneRecord { get; set; }
        public DateTime DataUltimaModifica { get; set; }
        public string UtenteCreazioneRecord { get; set; }
        public string UtenteUltimaModificaRecord { get; set; }
    }
}
