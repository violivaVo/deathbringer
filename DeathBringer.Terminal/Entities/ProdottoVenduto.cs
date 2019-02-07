using DeathBringer.Terminal.BaseClasses;

namespace DeathBringer.Terminal.Entities
{
    public class ProdottoVenduto: EntityBase
    {
        public int Quantita { get; set; }
        public Prezzo PrezzoProdotto { get; set; }
        public Carrello CarrelloAssociato { get; set; }        
    }
}
