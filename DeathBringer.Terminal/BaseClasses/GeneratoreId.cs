using DeathBringer.Terminal.Data;
using DeathBringer.Terminal.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeathBringer.Terminal.BaseClasses
{
    public static class GeneratoreId
    {
        public static int GeneraNuovoIdentificatore<TEntity>(IList<TEntity> lista) //non è void perché questa funz. a diff. delle altre mi ritorna qualcosa, un int
            where TEntity : IEntity
        {
            //verifico quanti ce ne sono in archivio
            var elementiEsistenti =lista.Count;
            //se non ne ho, il valore base è 1
            if (elementiEsistenti == 0)
            {
                return 1;
            }
            else
            {   //devo cercare l'elemento con Id maggiore
                int idMaggiore = 0;
                for (var i = 0; i < lista.Count; i++)
                {
                    if (lista[i].Id > idMaggiore)
                    {
                        idMaggiore = lista[i].Id;
                    }

                }

                return idMaggiore + 1;
                // al posto del for qui sopra avrei potuto mette -> idMaggiore = ApplicationStorage.Categorie.Max(elementiEsistenti e => e.Id); (AVENDO MESSO SYSTEM LINQ)
                //questa cosa usa LINQ (??)
            }
        }
    }
}
