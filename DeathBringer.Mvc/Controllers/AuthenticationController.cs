using DeathBringer.Core.ServiceLayers;
using DeathBringer.Mvc.Models.Authentication;
using DeathBringer.Terminal.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DeathBringer.Mvc.Controllers
{
    public class AuthenticationController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            LoginModel model = new LoginModel();
            model.IsLoginOk = null;
            return View(model);
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            UtenteServiceLayer layer = new UtenteServiceLayer();

            //Recupero la lista dei prodotti dal database
            IList<Utente> utentiFromDatabase = layer.FetchUtenti();

            var res = utentiFromDatabase.SingleOrDefault(r => r.Username == model.Username &&
            r.Password == model.Password);
            if(res!=null)
            {
                model.IsLoginOk = true;

                //Ridirigo alla pagina di home
                return RedirectToAction("Index", "Home");
            }
            else
            {
                model.IsLoginOk = false;
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> LoginCookie(LoginModel model)
        {
            ApplicationServiceLayer layer = new ApplicationServiceLayer();

            //Recupero la lista dei prodotti dal database
            IList<Utente> utentiFromDatabase = layer.FetchUtenti();

            //Prendo l'utente corrispondente
            var utenteSelezionato = utentiFromDatabase.SingleOrDefault(u => u.Username == model.Username);

            //Se non ho trovato l'utente, esco e non faccio nulla
            if (utenteSelezionato == null)
            {
                //Imposto il fallimento e renderizzo la view
                model.IsLoginOk = false;
                return View("Login", model);
            }                

            //Se è stato trovato, verifico la password e ritorno fallimento se errata
            if (utenteSelezionato.Password != model.Password)
            {
                //Imposto il fallimento e renderizzo la view
                model.IsLoginOk = false;
                return View("Login", model);
            }

            //Se sono qui, creo l'elenco dei claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.Username),
                new Claim(ClaimTypes.GivenName, $"{utenteSelezionato.Nome} {utenteSelezionato.Cognome}"),
                new Claim(ClaimTypes.Email, utenteSelezionato.Email),
                new Claim("IsAdministrator", utenteSelezionato.IsAdministrator.ToString()),
            };

            //Creo l'Identity usando i claims
            var claimsIdentity = new ClaimsIdentity(claims, 
                CookieAuthenticationDefaults.AuthenticationScheme);

            //Creazione delle autorizzazioni
            var authProperties = new AuthenticationProperties
            {
                //Nessuna impostazione specifica
            };

            //Chiamo la pipeline di ASP.NET per salvare il cookie
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            //Ridirigo alla pagina principale
            return RedirectToAction("Index", "Home");
        }
    }
}
