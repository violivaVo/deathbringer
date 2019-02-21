using System;
using System.ComponentModel.DataAnnotations;
using DeathBringer.Terminal.BaseClasses;

namespace DeathBringer.Terminal.Entities
{
    public class Prezzo : EntityBase
    {

        [Required(ErrorMessage = "Il campo è richesto")]
        [StringLength(255)]
        public virtual double Costo { get; set; }

        public virtual double Sconto { get; set; }
        public virtual DateTime DataInizio { get; set; }
    
        [Range(0, int.MaxValue)]
        public virtual int ProdottoAssociatoId { get; set; }
        [Required]
        public virtual Prodotto ProdottoAssociato { get; set; }
    }
}    