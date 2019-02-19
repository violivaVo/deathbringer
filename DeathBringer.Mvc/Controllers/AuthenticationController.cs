using DeathBringer.Mvc.Models.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
            //Verificare username e password
            if (model.Username == "mario.rossi" && model.Password == "paperino")
            {
                //Se sono ok, dare conferma
                model.IsLoginOk = true;




                //Ridirigo alla pagina di home
                return RedirectToAction("Index", "Home");
            }
            else
            {
                //Se sono errati, dare errore
                model.IsLoginOk = false;

                //Rimango sulla stessa pagina con il messaggio di errore
                return View(model);
            }
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
