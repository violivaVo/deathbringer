using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DeathBringer.Core.ServiceLayers
{
    class UtenteServiceLayer
    {
        public IList<ValidationResult> InsertUtente(string name, string surname)
        {
            //Preparo la lista vuota che è simbolo di successo dell'operazione
            IList<ValidationResult> validations = new List<ValidationResult>();

            //Se il nome (che è OBBLIGATORIO) è vuoto o nullo, esco
            if (string.IsNullOrWhiteSpace(name))
            {
                //Aggiungo il messaggio con la spiegazione ed esco
                validations.Add(new ValidationResult($"Il nome è obbligatorio"));
                return validations;
            }

            //Creazione dell'oggetto (classe)
            var nuovaCategoria = new Categoria
            {
                Id = GeneratoreId.GeneraNuovoIdentificatore<Categoria>(ApplicationStorage.Categorie),
                Nome = name,
                Descrizione = surname,
                DataCreazioneRecord = DateTime.Now,
                DataUltimaModifica = DateTime.Now,
                UtenteCreazioneRecord = "anonymous",
                UtenteUltimaModificaRecord = "anonymous"
            };
        }
}
