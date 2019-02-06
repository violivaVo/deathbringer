using DeathBringer.Terminal.BaseClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeathBringer.Terminal.ApplicationManagers
{
    public class SampleCreazioneDati
    {
        internal static void CreaDatiEsempio()
        {
            
                Prodotto libro = new Prodotto();
                libro.Nome = "Signore degli Anelli";
                libro.Descrizione = "boh";
                libro.Brand = "Duracell";
                libro.DataProduzione = new DateTime(2019, 2, 5);
                ApplicaDatiSistema(libro);

                Prodotto videogame = new Prodotto();
                videogame.Nome = "Ezio's Collection";
                videogame.Descrizione = "sangue";
                videogame.Brand = "Ubisoft";
                videogame.DataProduzione = new DateTime(2019, 2, 5);
                ApplicaDatiSistema(videogame);
           
        }

        private static void ApplicaDatiSistema(Prodotto libro)
        {
            throw new NotImplementedException();
        }
    }
}
