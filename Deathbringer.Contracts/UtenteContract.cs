using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeathBringer.Api.Models
{
    public class UserContract
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string CivicNumber { get; set; }
        public int ZipCode { get; set; }
        public string City { get; set; }
        public bool IsAdministrator { get; set; }
        public int UserId { get; set; }
    }
}
