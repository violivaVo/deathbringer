using DeathBringer.Terminal.BaseClasses;
using DeathBringer.Terminal.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DeathBringer.Core.Tests
{
    [TestClass]
    public class GeneratoreIdTests
    {
        [TestMethod]
        public void DovrebbeRitornarmi1SeChiamatoUnaVolta()
        {
            //Lista vuota
            var sampleCategorie = new List<Categoria>();

            //Se chiamo dovrebbe darmi 1
            var generato = GeneratoreId.GeneraNuovoIdentificatore(sampleCategorie);

            //Asserzione
            Assert.AreEqual(1, generato);
        }

        [TestMethod]
        public void DovrebbeRitornarmi2SeChiamatoDueVolte()
        {
            //Lista vuota
            var sampleCategorie = new List<Categoria>();

            //Se chiamo dovrebbe darmi 1
            var primoValoreGenerato = GeneratoreId.GeneraNuovoIdentificatore(sampleCategorie);

            //Aggiungo una categoria con il valore generato
            Categoria prima = new Categoria { Id = primoValoreGenerato };
            sampleCategorie.Add(prima);

            //Se chiamo ancora con elemento in lista dovrebbe dare 2 (primoValoreGenerato + 1)
            var secondValoreGenerato = GeneratoreId.GeneraNuovoIdentificatore(sampleCategorie);

            //Asserzione
            Assert.AreEqual(primoValoreGenerato + 1, secondValoreGenerato);
        }

        [TestMethod]
        public void DovrebbeSaltareIlNumero17()
        {
            //Lista vuota
            var sampleCategorie = new List<Categoria>();

            //Creo 20 categorie
            for (var i = 0; i < 20; i++)
            {
                sampleCategorie.Add(new Categoria
                {
                    Id = GeneratoreId.GeneraNuovoIdentificatore(sampleCategorie)
                });
            };

            //Cerco un elemento con Id = 17
            var elementoCon17 = sampleCategorie.SingleOrDefault(e => e.Id == 17);

            //Assert: se non ho elementi, il test è passato
            Assert.IsTrue(elementoCon17 != null);
        }
    }
}
