using System.ComponentModel.DataAnnotations;

namespace Deathbringer.Contracts.Requests
{
    /// <summary>
    /// Request for register user
    /// </summary>
    public class RegisterUserRequest
    {
        [Required]
        [StringLength(255)]
        public string UserName { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Surname { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        [Required]
        [StringLength(255)]
        public string ConfirmPassword { get; set; }
    }
}
