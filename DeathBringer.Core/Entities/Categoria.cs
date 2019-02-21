using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DeathBringer.Terminal.BaseClasses;

namespace DeathBringer.Terminal.Entities
{
    public class Categoria: EntityBase
    {
        [Required(ErrorMessage = "Il campo è richesto")]
        [StringLength(255)]
        public virtual string Nome { get; set; }

        public virtual string Descrizione { get; set; }

        public virtual List<Prodotto> Prodotti { get; set; }
    }
}
