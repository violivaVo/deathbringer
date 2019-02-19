using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DeathBringer.Core.Data.Helpers
{
    /// <summary>
    /// Utilità per eseguire le validazioni
    /// </summary>
    public static class ValidationUtils
    {
        /// <summary>
        /// Esegue la validazione dell'oggetto passato sulla base delle annotazioni
        /// </summary>
        /// <typeparam name="TEntity">Tipo dell'entità</typeparam>
        /// <param name="entity">Istanza dell'entità</param>
        /// <returns>Ritorna la lista delle validazioni</returns>
        public static IList<ValidationResult> Validate<TEntity>(TEntity entity)
            where TEntity : class, new()
        {
            //Eseguo la creazione di una lista vuota per contenere gli errori di validazione
            IList<ValidationResult> validationResults = new List<ValidationResult>();

            //Creo un'istanza del contesto di validazione (passando la sessione nel contesto)
            IDictionary<object, object> items = new Dictionary<object, object>();
            ValidationContext validationContext = new ValidationContext(entity, null, items);

            //Eseguo la valiazione dell'entità e mando in uscita la lista di errori di validazione
            Validator.TryValidateObject(entity, validationContext, validationResults, true);

            //Mando in uscita la lista
            return validationResults;
        }
    }
}
