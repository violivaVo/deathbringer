using System;
using System.Collections.Generic;
using System.Text;
using DeathBringer.Terminal.BaseClasses;
using DeathBringer.Terminal.Entities;

namespace DeathBringer.Terminal.BaseClasses
{
    public class Prodotto: EntityBase
    {
        public string Nome { get; set; }
        public Categoria CategoriaAppartenenza { get; set; }
        
        public DateTime DataProduzione { get; set; }
        public string Descrizione { get; set; }
        public byte[] Foto { get; set; }
        public string Brand { get; set; }
    }
}
