using DeathBringer.Core.Data;
using DeathBringer.Terminal.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DeathBringer.Core.Mocks
{
    public class MockCategoriaRepository : ICategoriaRepository
    {
        //Lista statica per mock
        private static Lazy<IList<Categoria>> Categorie = new Lazy<IList<Categoria>>(Initialize);

        private static IList<Categoria> Initialize()
        {
            //Creazione lista utenti fittizi
            return new List<Categoria>
            {
                new Categoria
                {
                    Id = 1,
                    Nome = "Frutta", 
                    Descrizione = "descrizione frutta"                    
                },
                new Categoria
                {
                    Id = 2,
                    Nome = "Verdura",
                    Descrizione = "descrizione verdura"
                },
                new Categoria
                {
                    Id = 3,
                    Nome = "Libri",
                    Descrizione = "descrizione libri"
                },
                new Categoria
                {
                    Id = 4,
                    Nome = "Videogames",
                    Descrizione = "descrizione videogames"
                }
            };
        }

        public IList<ValidationResult> Crea(Categoria entity)
        {
            //Semplice aggiunta
            Categorie.Value.Add(entity);

            //Ritorno senza errori
            return new List<ValidationResult>();
        }

        public IList<ValidationResult> Elimina(Categoria entity)
        {
            //Rimozione
            Categorie.Value.Remove(entity);

            //Ritorno senza errori
            return new List<ValidationResult>();
        }

        public IList<Categoria> Fetch()
        {
            //Ritorna la lista statica
            return Categorie.Value;
        }

        public Categoria GetById(int id)
        {
            //Ritorno l'entity con id (se esiste)
            return Categorie.Value.SingleOrDefault(e => e.Id == id);
        }

        public IList<ValidationResult> Modifica(Categoria entity)
        {
            //Predisponsizione a nessun errore
            var validations = new List<ValidationResult>();

            //Rimpiazza
            var existing = Categorie.Value.SingleOrDefault(e => e.Id == entity.Id);
            if (existing == null)
                return validations;
            Categorie.Value.Remove(existing);
            Categorie.Value.Add(entity);
            return validations;
        }
    }
}
