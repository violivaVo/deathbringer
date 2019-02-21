using DeathBringer.Api.Models;
using DeathBringer.Terminal.Entities;
using System;

namespace DeathBringer.Api.Helpers
{
    /// <summary>
    /// Contiene utilità varie per gestire i contract
    /// </summary>
    public static class ContractUtils
    {
        public static UtenteContract GenerateContract(Utente entity)
        {
            //Validazione argomenti
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            //Generazione contratto e mappatura
            return new UtenteContract
            {
                Username = entity.Username,
                Nome = entity.Nome,
                Cognome = entity.Cognome,
                Indirizzo = entity.Indirizzo,
                Citta = entity.Citta,
                Cap = entity.Cap,
                Civico = entity.Civico,
                Email = entity.Email,
                IsAdministrator = entity.IsAdministrator
            };
        }
    }
}
