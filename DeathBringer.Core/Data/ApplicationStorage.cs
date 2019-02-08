using DeathBringer.Terminal.BaseClasses;
using DeathBringer.Terminal.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DeathBringer.Terminal.Data
{
    public class ApplicationStorage   //creato questa classe per metterci dentro una variabile statica di tipo lista categorie
    {
        public static event EventHandler<string> DatabaseSaved;


        public static IList<Categoria> Categorie = new List<Categoria>();  //inizialmente è vuota questa lista
        public static IList<Prodotto> Prodotti = new List<Prodotto>();
        public static IList<Utente> Utenti = new List<Utente>();

        public const string PercorsoFileDatabaseCategorie = "C:\\deathbringer-categorie.json";
        public const string PercorsoFileDatabaseUtenti = "C:\\deathbringer-utenti.json";
        public const string PercorsoFileDatabaseProdotti = "C:\\deathbringer-prodotti.json";

        public static void LoadCategorie()
        {
            //Verifico che il file esiste
            if (!File.Exists(PercorsoFileDatabaseCategorie))
                return;

            //Leggo il contenuto del file
            var contenuto = File.ReadAllText(PercorsoFileDatabaseCategorie);

            //De-serializzo il contenuto sulla classe corrente
            Categorie = JsonConvert.DeserializeObject<IList<Categoria>>(contenuto);
        }

        public static void SaveCategorie()
        {
            //Serializziamo le categorie in JSON
            var json = JsonConvert.SerializeObject(Categorie);

            //Scrittura del file sul disco
            File.WriteAllText(PercorsoFileDatabaseCategorie, json);

            //Se ho un gestore di evento, sollevo l'evento
            if (DatabaseSaved != null)
                DatabaseSaved(null, nameof(Categoria));
        }

        public static void LoadProdotti()
        {
            //Verifico che il file esiste
            if (!File.Exists(PercorsoFileDatabaseProdotti))
                return;

            //Leggo il contenuto del file
            var contenuto = File.ReadAllText(PercorsoFileDatabaseProdotti);

            //De-serializzo il contenuto sulla classe corrente
            Prodotti = JsonConvert.DeserializeObject<IList<Prodotto>>(contenuto);
        }

        public static void SaveProdotti()
        {
            //Serializziamo le categorie in JSON
            var json = JsonConvert.SerializeObject(Prodotti);

            //Scrittura del file sul disco
            File.WriteAllText(PercorsoFileDatabaseProdotti, json);
        }
    }
}
