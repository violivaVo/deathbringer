using System;

namespace Deathbringer.Contracts.Results
{
    public class SignInResult
    {
        /// <summary>
        /// User id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Nome
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Cognome
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Flag amministratore
        /// </summary>
        public bool IsAdministrator { get; set; }

        /// <summary>
        /// Data di ultimo accesso
        /// </summary>
        public DateTime LastAccessDate { get; set; }
    }
}
