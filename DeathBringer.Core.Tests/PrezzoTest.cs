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
    public class PrezzoTests
    {
        [TestMethod]
        public void ShoudFecthPricesWithProduct()
        {
            //Inizializzo il db context
            DeathBringerDbContext context = new DeathBringerDbContext();
            //Creo categoria e la aggiungo
            Random random = new Random();
            var randomName = $"nome-{random.Next(1000)}";
            var randomDesc = $"description-{random.Next(1000)}";
            var nuovaCategoria = new Categoria
            {
                Nome = randomName,
                Descrizione = randomDesc,
                DataCreazioneRecord = DateTime.Now
            };
            context.Categorie.Add(nuovaCategoria);
            context.SaveChanges();

            //Creazione della Prodotto e commit
            var newProdotto = new Prodotto
            {
                Nome = $"Prod-{RandomUtils.GenerateString(10)}",
                Descrizione = RandomUtils.GenerateString(15),
                DataCreazioneRecord = DateTime.Now,
                DataUltimaModifica = DateTime.Now,
                CategoriaAppartenenza = nuovaCategoria,
            };
            context.Prodotti.Add(newProdotto);
             
            //Creo il prodotto
            var newPrezzo = new Prezzo
            {
               
               
            Costo = random.NextDouble() * (1000 - 1) + 1,
            Sconto = 12.5,
            DataInizio = DateTime.Now,
            ProdottoAssociato = newProdotto,
            DataCreazioneRecord = DateTime.Now,
            DataUltimaModifica = DateTime.Now,
            
            };
            context.Prezzi.Add(newPrezzo);
            context.SaveChanges();

            //Estrazione dei prezzi del Prodotto
            //EAGER LOADING
               var prezWithProd = context.Prezzi
                .Include(e => e.ProdottoAssociato)
                .Where(p => p.Sconto == 12.5)
                .ToList();

            //LAZY LOADING
            var prezWithProdLazy = context.Prezzi
                .Where(p => p.Sconto == 12.5)
                .ToList();

            //Assert
            Assert.IsNotNull(prezWithProd);
            Assert.IsTrue(prezWithProd.Count > 0);

            //Prendo l'elemento prodotto appena creato
            var prezzoAppenaCreato = prezWithProd.Single(e => e.Costo == newPrezzo.Costo);

            //Altre asserzioni
            Assert.IsNotNull(prezzoAppenaCreato.ProdottoAssociato);
            Assert.IsNotNull(prezzoAppenaCreato.ProdottoAssociato.Nome == newProdotto.Nome);
        }
    }
}
