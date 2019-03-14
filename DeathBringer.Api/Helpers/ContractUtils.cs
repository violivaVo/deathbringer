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
        public static UserContract GenerateContract(Utente entity)
        {
            //Validazione argomenti
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            //Generazione contratto e mappatura
            return new UserContract
            {
                UserId = entity.Id,
                UserName = entity.UserName,
                Name = entity.Nome,
                Surname = entity.Cognome,
                Address = entity.Indirizzo,
                City = entity.Citta,
                ZipCode = entity.Cap,
                CivicNumber = entity.Civico,
                Email = entity.Email,
                IsAdministrator = entity.IsAdministrator
            };
        }
    }
}
