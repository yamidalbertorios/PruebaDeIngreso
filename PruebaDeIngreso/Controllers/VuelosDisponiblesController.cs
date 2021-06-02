using Entidades;
using Logica;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PruebaDeIngreso.Controllers
{
    public class VuelosDisponiblesController : Controller
    {        
        AdministrarVuelos adminV = new AdministrarVuelos();
        AdministrarContactos adminC = new AdministrarContactos();

        // GET: VuelosDisponibles
        public ActionResult VuelosDisponibles()
        {
            ViewBag.ListCities = adminV.getListaDeCiudades(); 
            return View();
        }
        [HttpPost]
        public ActionResult _DisponibilidadesDeVuelos(string codigoOrigen, string codigoDestino, DateTime Desde)
        {
            var modelo = new GenericModel(); 
            List<RespuestaAPI> respuesta = adminV.getRespuestaAPI(codigoOrigen, codigoDestino, Desde, "si");
            modelo.listRespuestaAPI = respuesta;
            return View(modelo);
        }
        public JsonResult guardarInformacionContactoPasajero(string origen, string destino, DateTime fechaIda, int numeroVuelo, decimal precio, string moneda, string dataContacto, string dataPasajeros)
        {
            int IdVuelo = adminV.guardarResultado(origen, destino, fechaIda, numeroVuelo, precio, moneda);
            List<ReservaNro> re = adminC.guardarContactoPasajerosReserva(dataContacto, dataPasajeros, IdVuelo);
            return Json(new { Results = JsonConvert.SerializeObject(re) }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GuardarResultado(string DepartureStation, string ArrivalStation, DateTime DepartureDate, int FlightNumber, decimal Price, string Currency)
        {
            adminV.guardarResultado(DepartureStation, ArrivalStation, DepartureDate, FlightNumber, Price, Currency);
            return RedirectToAction("VuelosDisponibles", "VuelosDisponibles");
        } 
    }
}