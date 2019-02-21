using DeathBringer.EntityFramework.Tests.Helpers;
using DeathBringer.Terminal.BaseClasses;
using DeathBringer.Terminal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yred.Authentication.Relationals.Data.Contexts;

namespace DeathBringer.EntityFramework.Tests
{
    [TestClass]
    public class ProdottoTests
    {
        [TestMethod]
        public void ShouldCreateNewProduct()
        {
            //Inizializzo il db context
            DeathBringerDbContext context = new DeathBringerDbContext();

            //Estrazione di una categoria: se non esiste, inconclusivo
            var categoria = context.Categorie.FirstOrDefault();
            if (categoria == null)
                Assert.Inconclusive();

            //Verifico numero di prodotti
            var conteggioPrima = context.Prodotti.Count();

            //Creo prodotto random
            var nuovoProdotto = new Prodotto
            {
                Nome = RandomUtils.GenerateString(6),
                Descrizione = RandomUtils.GenerateString(15),
                DataCreazioneRecord = DateTime.Now, 
                CategoriaAppartenenza = categoria, 
                Brand = "Philips", 
                //CategoriaAppartenenzaId = categoria.Id,
            };

            //Inserisco prodotto e commit
            context.Prodotti.Add(nuovoProdotto);
            context.SaveChanges();

            //Conto numero dopo inserit
            var conteggioDopo = context.Prodotti.Count();

            //Assert che il numero è aumentato di 1
            Assert.IsTrue(conteggioPrima + 1 == conteggioDopo);
        }

        [TestMethod]
        public void ShoudFetchProductsWithCategories()
        {
            //Inizializzo il db context
            DeathBringerDbContext context = new DeathBringerDbContext();

            //Creazione della categoria e commit
            var newCategoria = new Categoria
            {
                Nome = $"Cat-{RandomUtils.GenerateString(10)}", 
                Descrizione = RandomUtils.GenerateString(15), 
                DataCreazioneRecord = DateTime.Now,
                DataUltimaModifica = DateTime.Now
            };
            context.Categorie.Add(newCategoria);

            //Creo il prodotto
            var newProdotto = new Prodotto
            {
                Nome = $"Prod-{RandomUtils.GenerateString(4)}", 
                Descrizione = "non importa", 
                CategoriaAppartenenza = newCategoria,
                DataCreazioneRecord = DateTime.Now,
                DataUltimaModifica = DateTime.Now, 
                Brand = "Prova"
            };
            context.Prodotti.Add(newProdotto);
            context.SaveChanges();

            //Estrazione dei prodotti con le categorie
            //EAGER LOADING
            var prodWithCateg = context.Prodotti
                .Include(e => e.CategoriaAppartenenza)
                .Where(p => p.Brand == "Prova")
                .ToList();

            //LAZY LOADING
            var prodWithCategLazy = context.Prodotti
                .Where(p => p.Brand == "Prova")
                .ToList();

            //Assert
            Assert.IsNotNull(prodWithCateg);
            Assert.IsTrue(prodWithCateg.Count > 0);

            //Prendo l'elemento prodotto appena creato
            var prodAppenaCreato = prodWithCateg.Single(e => e.Nome == newProdotto.Nome);

            //Altre asserzioni
            Assert.IsNotNull(prodAppenaCreato.CategoriaAppartenenza);
            Assert.IsNotNull(prodAppenaCreato.CategoriaAppartenenza.Nome == newCategoria.Nome);
        }

        [TestMethod]
        public void ShouldInvokeStoreProcedure()
        {
            //Inizializzo il db context
            DeathBringerDbContext context = new DeathBringerDbContext();

            //Date range
            var from = new DateTime(2019, 1, 1);
            var to = new DateTime(2019, 12, 31);
            var fromString = from.ToString("yyyy-MM-dd");
            var toString= to.ToString("yyyy-MM-dd");

            //Estrazione da store procedure
            var prodottiDaStoreProcedure = context.Prodotti
                .FromSql(
                $"SelezionaProdottiConCategorieCreateInData('{fromString}', '{toString}')")
                .ToList();

            //Assert
            Assert.IsNotNull(prodottiDaStoreProcedure);
            Assert.IsNotNull(prodottiDaStoreProcedure.Count > 0);
        }
    }
}
