using AccesoADatos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class AdministrarContactos
    {
        PruebaIngresoEntities db = new PruebaIngresoEntities();

        public List<ReservaNro> guardarContactoPasajerosReserva(string dataContacto, string dataPasajeros, int IdVuelo)
        {
            try
            {
                Reserva r = new Reserva();
                r.IdVuelo = IdVuelo;
                db.Reserva.Add(r);
                db.SaveChanges();

                var dataItemContacto = dataContacto.Split('|');

                Contacto c = new Contacto();
                c.nombreContacto = dataItemContacto[0];
                c.cedulaContacto = dataItemContacto[1];
                c.telefonoContacto = dataItemContacto[2];
                c.emailContacto = dataItemContacto[3];
                c.IdReserva = r.Id;
                db.Contacto.Add(c);
                db.SaveChanges();

                if (dataPasajeros != string.Empty)
                {
                    string[] dataItemPasajeros = dataPasajeros.Split('|');

                    foreach (string pa in dataItemPasajeros)
                    {
                        Pasajero p = new Pasajero();
                        p.nombrePasajero = pa;
                        p.IdReserva = r.Id;
                        db.Pasajero.Add(p);
                        db.SaveChanges();
                    }
                }

                var listR = (from re in db.Reserva
                             where re.Id == r.Id
                             select new ReservaNro
                             {
                                 Id = re.Id
                             }).ToList();
                return listR;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    string error = ex.Message + ex.InnerException.ToString();
                    return null;
                }
                else
                {
                    string error = ex.Message + ex.InnerException.ToString();
                    return null;
                }
            }
        }

        public GenericModel retornarDatosContacto(int numeroReserva)
        {
            string fechaIda = string.Empty;
            var item = db.Contacto.Where(x => x.IdReserva == numeroReserva).FirstOrDefault();
            var itemR = db.Reserva.Where(x => x.Id == numeroReserva).FirstOrDefault();
            if (itemR != null)
            {
                var itemV = db.Flight.Where(x => x.Id == itemR.IdVuelo).FirstOrDefault();
                fechaIda = itemV.DepartureDate.ToShortDateString();
            }
            var modelo = new GenericModel();

            if (item != null)
            {
                modelo.nombreContacto = item.nombreContacto;
                modelo.cedulaContacto = item.cedulaContacto;
                modelo.telefonoContacto = item.telefonoContacto;
                modelo.emailContacto = item.emailContacto;
                modelo.IdReserva = item.IdReserva;
                modelo.fechaIda = fechaIda;
            }
            return modelo;
        }
        public void modificarContacto(GenericModel gm)
        {
            try
            {
                Contacto c = db.Contacto.Where(x => x.IdReserva == gm.IdReserva).FirstOrDefault();
                c.nombreContacto = gm.nombreContacto;
                c.cedulaContacto = gm.cedulaContacto;
                c.telefonoContacto = gm.telefonoContacto;
                c.emailContacto = gm.emailContacto;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    string error = ex.Message + ex.InnerException.ToString();
                }
                else
                {
                    string error = ex.Message + ex.InnerException.ToString();
                }
            }
        }
    }
}
