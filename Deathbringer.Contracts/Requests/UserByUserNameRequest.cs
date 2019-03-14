﻿using System.ComponentModel.DataAnnotations;

namespace Deathbringer.Contracts.Requests
{
    /// <summary>
    /// Request per utente usando lo username
    /// </summary>
    public class UserByUserNameRequest
    {
        /// <summary>
        /// Username
        /// </summary>
        [Required]
        [StringLength(255)]
        public string UserName { get; set; }
    }
}