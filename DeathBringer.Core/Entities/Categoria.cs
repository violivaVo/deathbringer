using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DeathBringer.Terminal.BaseClasses;

namespace DeathBringer.Terminal.Entities
{
    public class Categoria: EntityBase
    {
        [Required(ErrorMessage = "Il campo è richesto")]
        [StringLength(255)]
        public string Nome { get; set; }

        public string Descrizione { get; set; }

        public List<Prodotto> Prodotti { get; set; }
    }
}
