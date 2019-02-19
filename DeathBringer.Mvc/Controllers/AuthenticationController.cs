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
            var claims = new List<Claim>
            {
                new Claim( ClaimTypes.Name, model.Username),
                new Claim("FullName", "Mario"),
                new Claim(ClaimTypes.Role, "Administrator"),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return RedirectToAction("Index", "Home");
        }
    }
}
