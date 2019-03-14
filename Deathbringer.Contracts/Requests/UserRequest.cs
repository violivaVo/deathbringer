using System.ComponentModel.DataAnnotations;

namespace Deathbringer.Contracts.Requests
{
    /// <summary>
    /// Request per singolo utente
    /// </summary>
    public class UserRequest
    {
        /// <summary>
        /// Id utente
        /// </summary>
        [Required]
        public int? UserId { get; set; }
    }
}
