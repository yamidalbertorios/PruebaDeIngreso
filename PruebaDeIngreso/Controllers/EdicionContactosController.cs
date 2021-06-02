using Entidades;
using Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PruebaDeIngreso.Controllers
{
    public class EdicionContactosController : Controller
    {
        AdministrarContactos adminC = new AdministrarContactos();

        // GET: EdicionContactos
        public ActionResult Index()
        {
            return View();
        } 
        public ActionResult EditarContacto()
        {
            return View();
        }
        public ActionResult _ParcialEditarContacto(int numeroReserva)
        {
            return View(adminC.retornarDatosContacto(numeroReserva));
        }

        [HttpPost]
        public ActionResult modificarContacto(GenericModel gm)
        {
            adminC.modificarContacto(gm);
            //string output = "Modificación éxitosa";
            //return Json(output, JsonRequestBehavior.AllowGet);
            
            return RedirectToAction("VuelosDisponibles", "VuelosDisponibles");
        }
    }
}