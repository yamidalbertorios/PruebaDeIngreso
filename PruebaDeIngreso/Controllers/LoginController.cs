using System;
using System.Web.Mvc;

namespace PruebaDeIngreso.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string user, string pass)
        {
            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                ViewBag.messageDanger = "Datos incorrectos de inicio de sesion";
                return View();
            }
           
            try
            {
                return RedirectToAction("VuelosDisponibles", "VuelosDisponibles");
            }
            catch (Exception ex)
            {
                ViewBag.messageError = ex.ToString();
                return View("~/Views/Shared/Error.cshtml");
            }
        }
    }
}