using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using DeathBringer.Core.ServiceLayers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;


namespace iCubed.Ragnarok.Api.Middlewares
{
    /// <summary>
    /// Handler for Basi Authentication
    /// </summary>
    public class BasicAuthenticationHandler : AuthenticationHandler<BasicAuthenticationOptions>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options">Options</param>
        /// <param name="logger">Logger</param>
        /// <param name="encoder">Encoder</param>
        /// <param name="clock">Clock</param>
        public BasicAuthenticationHandler(IOptionsMonitor<BasicAuthenticationOptions> options, 
            ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock) { }

        /// <summary>
        /// Handle process for current authentication
        /// </summary>
        /// <returns></returns>
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            //Se non ho headers o non ho "Authentication", esco
            if (string.IsNullOrEmpty(Request.Headers?["Authorization"]))
            {
                //Fallisco l'autenticazione
                return Task.FromResult(AuthenticateResult.Fail("Header 'Authorization' was not provided"));
            }

            //Recupero il valore e split
            string authValue = Request.Headers["Authorization"];
            var segments = authValue.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            //Se non ho due elementi, esco
            if (segments.Length != 2)
            {
                //Fallisco l'autenticazione
                return Task.FromResult(AuthenticateResult.Fail("Header 'Authorization' should contains two items: schema and value"));
            }

            //Se il lo schema non è Basic, esco
            if (segments[0] != "Basic" || string.IsNullOrEmpty(segments[1]))
            {
                //Fallisco l'autenticazione
                return Task.FromResult(AuthenticateResult.Fail($"Provided schema is not '{Scheme.Name}'"));
            }

            string credentials;
            try
            {
                //Il valore dell'intestazione va decodificato dalla sua forma Base64
                //Per i dettagli, vedere: http://www.w3.org/Protocols/HTTP/1.0/spec.html#BasicAA
                credentials = Encoding.UTF8.GetString(Convert.FromBase64String(segments[1]));
            }
            catch
            {
                //Probabilmente la stringa base64 non era valida
                credentials = string.Empty;
            }

            //Username e password sono separati dal carattere delimitatore ":"
            //Terminiamo l'esecuzione se non è presente o se è in posizione non valida
            var indexOfSeparator = credentials.IndexOf(":", StringComparison.Ordinal);
            if (indexOfSeparator < 1 || indexOfSeparator > credentials.Length - 2)
            {
                //Fallisco l'autenticazione
                return Task.FromResult(AuthenticateResult.Fail("Base64 encoded values should be separated by char ':'"));
            }

            //Estraiamo finalmente le credenziali
            var username = credentials.Substring(0, indexOfSeparator);
            var password = credentials.Substring(indexOfSeparator + 1);

            //Se username o password sono vuoti, esco
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                //Fallisco l'autenticazione
                return Task.FromResult(AuthenticateResult.Fail("Username and/or password should not be empty or null"));
            }

            //Istanzio l'ApplicationServiceLayer
            ApplicationServiceLayer layer = new ApplicationServiceLayer();
            var signedInUser = layer.SignIn(username, password);

            //Se non ho l'utente, esco
            if (signedInUser == null)
            {
                //Fallisco
                return Task.FromResult(AuthenticateResult.Fail("Provided credentials are invalid"));
            }

            //Altrimenti creo un principal generico
            var identity = new GenericIdentity(signedInUser.Username);
            var principal = new GenericPrincipal(identity, new string[] { });

            //Creo il ticket di autenticazione
            var authTicket = new AuthenticationTicket(
                new ClaimsPrincipal(principal),
                new AuthenticationProperties(),
                Scheme.Name);

            //Confermo l'autenticazione
            return Task.FromResult(AuthenticateResult.Success(authTicket));

            #region VERSIONE CON CLAIMS
            ////Creo un'identity generica per le informazioni base e la inserisco in una claims                   
            //ClaimsIdentity claimsIdentity = new ClaimsIdentity(BasicAuthenticationOptions.Scheme);
            //claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, signedInUser.UserName));
            //claimsIdentity.AddClaim(new Claim("FullName", signedInUser.Name));

            ////Aggiungo il claim con la password
            //claimsIdentity.AddClaim(new Claim("alcazar://password", password));

            ////Serializzo i profili e li accodo in un altro claim
            //string json = JsonConvert.SerializeObject(signedInUser);
            //claimsIdentity.AddClaim(new Claim("alcazar://profiles", json));

            ////Creo un principal che contenga i dati
            //ClaimsPrincipal principal = new ClaimsPrincipal(claimsIdentity);

            ////Imposto il principal
            //Thread.CurrentPrincipal = principal;
            //Context.User = principal;

            ////Creo il ticket di autenticazione
            //var authTicket = new AuthenticationTicket(principal, new AuthenticationProperties(), Scheme.Name);

            ////Confermo l'autenticazione
            //return Task.FromResult(AuthenticateResult.Success(authTicket));
            #endregion
        }
    }
}