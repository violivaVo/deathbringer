using System;
using System.ComponentModel.DataAnnotations;
using DeathBringer.Terminal.Entities;

namespace DeathBringer.Terminal.BaseClasses
{
    public class Prodotto: EntityBase
    {
        [Required]
        [StringLength(255)]
        public string Nome { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public virtual int CategoriaAppartenenzaId { get; set; }

        [Required]
        public Categoria CategoriaAppartenenza { get; set; }        

        [Range(typeof(DateTime), "2000-01-01", "2100-12-31")]
        public DateTime DataProduzione { get; set; }

        public string Descrizione { get; set; }

        public byte[] Foto { get; set; }

        public string Brand { get; set; }
    }
}
