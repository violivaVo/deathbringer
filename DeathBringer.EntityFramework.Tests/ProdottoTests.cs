using DeathBringer.EntityFramework.Tests.Helpers;
using DeathBringer.Terminal.BaseClasses;
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
    }
}
