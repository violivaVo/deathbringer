using System;
using System.Collections.Generic;
using System.Text;

namespace DeathBringer.Terminal.Interfaces
{
    interface IEntity // per convenzione si mette una I maiuscola iniziale; inoltre non entità perché accentati no
    {
        int Id { get; set; }
        DateTime DataCreazioneRecord { get; set; }
        DateTime DataUltimaModifica { get; set; }
        string UtenteCreazioneRecord { get; set; }
       string UtenteUltimaModificaRecord { get; set; }
    }
}
