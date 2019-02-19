using DeathBringer.Core.ServiceLayers;
using DeathBringer.Mvc.Models.Authentication;
using DeathBringer.Terminal.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

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
            UtenteServiceLayer layer = new UtenteServiceLayer();

            //Recupero la lista dei prodotti dal database
            IList<Utente> utentiFromDatabase = layer.FetchUtenti();

            var res = utentiFromDatabase.SingleOrDefault(r => r.Username == model.Username &&
            r.Password == model.Password);
            if(res!=null)
            {
                model.IsLoginOk = true;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                model.IsLoginOk = false;
            } 
            return View(model);
        }
    }
}
