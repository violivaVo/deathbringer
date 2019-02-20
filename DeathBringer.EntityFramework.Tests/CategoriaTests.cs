using DeathBringer.Terminal.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Yred.Authentication.Relationals.Data.Contexts;

namespace DeathBringer.EntityFramework.Tests
{
    [TestClass]
    public class CategoriaTests
    {
        [TestMethod]
        public void ShouldCreateNewCategory()
        {
            //Inizializzo il db context
            DeathBringerDbContext context = new DeathBringerDbContext();

            //Verifico numero di categorie nel database
            var conteggioPrima = context.Categorie.Count();

            //Randomizzatore di numero pseudo-casuali
            Random random = new Random();
            var randomName = $"nome-{random.Next(1000)}";
            var randomDesc = $"description-{random.Next(1000)}";

            //Creo categoria random
            var nuovaCategoria = new Categoria
            {
                Nome = randomName, 
                Descrizione = randomDesc, 
                DataCreazioneRecord = DateTime.Now
            };

            //Inserisco categoria
            context.Categorie.Add(nuovaCategoria);

            //Eseguo il COMMIT
            context.SaveChanges();

            //Conto numero di categorie nel database
            var conteggioDopo = context.Categorie.Count();

            //Assert che il numero è aumentato di 1
            Assert.IsTrue(conteggioPrima == conteggioDopo - 1);
        }

        [TestMethod]
        public void ShouldDeleteExistingCategory()
        {
            //Inizializzo il db context
            DeathBringerDbContext context = new DeathBringerDbContext();

            Random random = new Random();
            var randomName = $"nome-{random.Next(1000)}";
            var randomDesc = $"description-{random.Next(1000)}";

            //Creo categoria e la aggiungo
            var nuovaCategoria = new Categoria
            {
                Nome = randomName,
                Descrizione = randomDesc,
                DataCreazioneRecord = DateTime.Now
            };
            context.Categorie.Add(nuovaCategoria);
            context.SaveChanges();

            //Verifico numero di categorie nel database
            var conteggioPrima = context.Categorie.Count();

            //Seleziono una categoria esistente
            var esistente = context.Categorie.FirstOrDefault();

            //Cancellazione categoria selezionata
            context.Categorie.Remove(esistente);

            //Eseguo il COMMIT
            context.SaveChanges();

            //Conto numero di categorie nel database
            var conteggioDopo = context.Categorie.Count();

            //Assert che è una meno di prima
            Assert.IsTrue(conteggioDopo == conteggioPrima - 1);
        }

        [TestMethod]
        public void ShouldFetchAtLeastOneCategory()
        {
            //Inizializzo il db context
            DeathBringerDbContext context = new DeathBringerDbContext();

            Random random = new Random();
            var randomName = $"nome-{random.Next(1000)}";
            var randomDesc = $"description-{random.Next(1000)}";

            //Creo categoria e la aggiungo
            var nuovaCategoria = new Categoria
            {
                Nome = randomName,
                Descrizione = randomDesc,
                DataCreazioneRecord = DateTime.Now
            };
            context.Categorie.Add(nuovaCategoria);
            context.SaveChanges();

            //Verifico numero di categorie nel database
            var lista = context.Categorie.ToList();

            //Assert che è una meno di prima
            Assert.IsNotNull(lista);
            Assert.IsTrue(lista.Count > 0);
        }
    }
}
