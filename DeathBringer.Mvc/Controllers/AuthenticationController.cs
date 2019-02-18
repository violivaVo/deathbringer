using DeathBringer.Mvc.Models.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace DeathBringer.Mvc.Controllers
{
    public class AuthenticationController: Controller
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
            }
            else
            {
                //Se sono errati, dare errore
                model.IsLoginOk = false;
            }

            return View(model);
        }
    }
}
