using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DeathBringer.Terminal.Entities;

namespace DeathBringer.Terminal.BaseClasses
{
    public class Prodotto: EntityBase
    {
        [Required]
        [StringLength(255)]
        public virtual string Nome { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public virtual int CategoriaAppartenenzaId { get; set; }

        [Required]
        public virtual Categoria CategoriaAppartenenza { get; set; }        

        [Range(typeof(DateTime), "2000-01-01", "2100-12-31")]
        public virtual DateTime DataProduzione { get; set; }

        public virtual string Descrizione { get; set; }

        public virtual byte[] Foto { get; set; }

        public virtual string Brand { get; set; }
        public virtual List<Prezzo> Prezzi { get; set; }
    }
}
