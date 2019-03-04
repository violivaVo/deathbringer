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
    public class AuthenticationClient: HttpClientBase
    {
        public AuthenticationClient()
            : base("https://deathbringer-api.azurewebsites.net/") { }

        /// <summary>
        /// Esegue il sign-in sulla piattaforma API
        /// </summary>
        /// <param name="request">Richiesta</param>
        /// <returns>Ritorna un task con la response</returns>
        public async Task<HttpResponseMessage<UtenteContract>> SignIn(SignInRequest request)
        {
            //Validazione argomenti
            if (request == null) throw new ArgumentNullException(nameof(request));

            return await Invoke<SignInRequest, UtenteContract>(
                "api/Authentication/SignIn",
                HttpMethod.Post, request);
        }
    }
}
