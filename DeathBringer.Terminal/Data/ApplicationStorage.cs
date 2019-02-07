using DeathBringer.Terminal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeathBringer.Terminal.Data
{
    public class ApplicationStorage   //creato questa classe per metterci dentro una variabile statica di tipo lista categorie
    {
        public static IList<Categoria> Categorie = new List<Categoria>();  //inizialmente è vuota questa lista
        public static IList<Utente> Utenti = new List<Utente>();
    }
}
