using DeathBringer.Api.Models;
using DeathBringer.Api.Models.Requests;
using DeathBringer.Clients.Clients.Common;
using DeathBringer.Clients.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DeathBringer.Clients.Clients
{
    public class UtentiClient: HttpClientBase
    {
        private string UserName { get; set; }
        private string Password { get; set; }

        public UtentiClient(string userName, string password)
            : base("https://deathbringer-api.azurewebsites.net/")
        {
            UserName = userName;
            Password = password;
        }

        /// <summary>
        /// Ritorna l'elenco degli utente
        /// </summary>
        /// <returns>Ritorna un task con la response</returns>
        public async Task<HttpResponseMessage<List<UtenteContract>>> FetchAllUtenti()
        {
            //Creazione dell'header (in base 64)
            var encoded = Encoding.UTF8.GetBytes($"{UserName}:{Password}");
            var base64 = Convert.ToBase64String(encoded);
            AuthenticationHeaderValue auth = new AuthenticationHeaderValue
                ("Basic", base64);

            //Invocazione della chiamate remota (con HEADER!)
            return await Invoke<object, List<UtenteContract>>(
                "api/Utenti/FetchAllUtenti",
                HttpMethod.Post, null, auth);
        }
    }
}
