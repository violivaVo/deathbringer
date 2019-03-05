using DeathBringer.Core.ServiceLayers;
using DeathBringer.Mvc.Models.Home;
using DeathBringer.Terminal.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeathBringer.Mvc.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            HomeIndexModel model = new HomeIndexModel();

            //Recupero il nome dell'utente autenticato
            model.AuthenticatedUserName = User.Identity.Name;

            //Passo il modello alla view
            return View(model);
        }

        [Authorize]
        public IActionResult SuperSecretPage()
        {
            ApplicationServiceLayer layer = new ApplicationServiceLayer();
            Utente user = layer.GetUtenteByUserName(User.Identity.Name);
            if (!user.IsAdministrator)
                return RedirectToAction("Authentication", "Login");


            

            return View();
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
