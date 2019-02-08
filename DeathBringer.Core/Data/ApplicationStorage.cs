using DeathBringer.Core.Helpers;
using DeathBringer.Terminal.BaseClasses;
using DeathBringer.Terminal.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DeathBringer.Terminal.Data
{
    public class ApplicationStorage   //creato questa classe per metterci dentro una variabile statica di tipo lista categorie
    {
        //Creazione dell'evento generale
        public static event EventHandler<string> DatabaseSaved;

        private static JsonSerializerSettings settings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };


        public static IList<Categoria> Categorie = new List<Categoria>();  //inizialmente è vuota questa lista
        public static IList<Prodotto> Prodotti = new List<Prodotto>();
        public static IList<Utente> Utenti = new List<Utente>();

        //public const string PercorsoFileDatabaseCategorie = "C:\\Users\\Guest111\\Desktop\\deathbringer-categorie.json";
        //public const string PercorsoFileDatabaseUtenti = "C:\\Users\\Guest111\\Desktop\\deathbringer-utenti.json";
        //public const string PercorsoFileDatabaseProdotti = "C:\\Users\\Guest111\\Desktop\\deathbringer-prodotti.json";

        public static void LoadCategorie()
        {
            //Verifico che il file esiste
            if (!File.Exists(FileUtils.ComposeFileName<Categoria>()))
                return;

            //Leggo il contenuto del file
            var contenuto = File.ReadAllText(FileUtils.ComposeFileName<Categoria>());

            //De-serializzo il contenuto sulla classe corrente
            Categorie = JsonConvert.DeserializeObject<IList<Categoria>>(contenuto, settings);
        }

        public static void SaveCategorie()
        {
            //Serializziamo le categorie in JSON
            var json = JsonConvert.SerializeObject(Categorie, Formatting.Indented, settings);

            //Scrittura del file sul disco
            File.WriteAllText(FileUtils.ComposeFileName<Categoria>(), json);

            //Se ho un gestore di evento, sollevo l'evento
            if (DatabaseSaved != null)
                DatabaseSaved(null, nameof(Categoria));
        }

        public static void LoadProdotti()
        {
            //Verifico che il file esiste
            if (!File.Exists(FileUtils.ComposeFileName<Prodotto>()))
                return;

            //Leggo il contenuto del file
            var contenuto = File.ReadAllText(FileUtils.ComposeFileName<Prodotto>());

            //De-serializzo il contenuto sulla classe corrente
            Prodotti = JsonConvert.DeserializeObject<IList<Prodotto>>(contenuto, settings);
        }

        public static void SaveProdotti()
        {
            //Serializziamo le categorie in JSON
            var json = JsonConvert.SerializeObject(Prodotti, Formatting.Indented, settings);

            //Scrittura del file sul disco
            File.WriteAllText(FileUtils.ComposeFileName<Prodotto>(), json);
        }
        public static void LoadUtenti()
        {
            //Verifico che il file esiste
            if (!File.Exists(FileUtils.ComposeFileName<Utente>()))
                return;

            //Leggo il contenuto del file
            var contenuto = File.ReadAllText(FileUtils.ComposeFileName<Utente>());

            //De-serializzo il contenuto sulla classe corrente
            Utenti = JsonConvert.DeserializeObject<IList<Utente>>(contenuto, settings);
        }

        public static void SaveUtenti()
        {
            //Serializziamo le categorie in JSON
            var json = JsonConvert.SerializeObject(Utenti, Formatting.Indented, settings);

            //Scrittura del file sul disco
            File.WriteAllText(FileUtils.ComposeFileName<Utente>(), json);

            //Se ho un gestore di evento, sollevo l'evento
            if (DatabaseSaved != null)
                DatabaseSaved(null, nameof(Utente));
        }
    }
}
