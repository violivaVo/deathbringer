using System;
using DeathBringer.EntityFramework.Data.Repositories;
using DeathBringer.Terminal.ApplicationManagers;
using DeathBringer.Terminal.BaseClasses;
using DeathBringer.Terminal.Entities;
using DeathBringer.Terminal.Interfaces;


namespace DeathBringer.Terminal
{
    class Program: EntityBase
    {
        public static void Main(string[] args)
        {            
            var start = new DateTime(2017, 03, 08, 11, 13, 14);
            var end = new DateTime(2017, 03, 31, 06, 03, 52);

            var duration = end.Subtract(start);


            MainMenu.VisualizzaMenuPrincipale();

            //Console.Write("Inserisci un nome prodotto: "); //quello che inserisci, dopo l'invio viene assegnato alla variabile qui sotto definita
            //string nomeProdotto = Console.ReadLine();
            //Console.Write("Inserisci una descrizione prodotto: "); //quello che inserisci, dopo l'invio viene assegnato alla variabile qui sotto definita
            //string descrizioneProdotto = Console.ReadLine();

            //Prodotto nuovoProdotto = new Prodotto();
            //nuovoProdotto.Nome = nomeProdotto;
            //nuovoProdotto.Descrizione = descrizioneProdotto;
            //Console.WriteLine("Creato prodotto " + nuovoProdotto.Nome + "!");
            //Console.ReadLine();

            //Console.WriteLine("Modifica nome prodotto: ");
            //string nomeProdDaModificare = Console.ReadLine();
            //Console.WriteLine("Il nuovo nome del prodotto è ");
            //string nomeProdModificato = Console.ReadLine(); 
            
        }

        public static void InserisciCategoria()
        {
            Categoria elettronica = new Categoria();
            elettronica.Nome = "Elettronica";
            elettronica.Descrizione = "Batterie e cavi vari";
            ApplicaDatiSistema(elettronica);
        }
       
        

        public static void EsempioInserimentoDaTerminale()
        {

        }

       public static void ApplicaDatiSistema(IEntity entityGenerica)
        {  //questa è la funzione che ho creato per evitare di mettere per intero 'sto comando di tracciamento ogni volta

            entityGenerica.DataCreazioneRecord = DateTime.Now;
            entityGenerica.DataUltimaModifica = DateTime.Now;
            entityGenerica.UtenteCreazioneRecord = "mauro";
            entityGenerica.UtenteUltimaModificaRecord = "alessio";
        }
    }
}
