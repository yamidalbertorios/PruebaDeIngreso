using System;
using System.Collections.Generic;

namespace Entidades
{
    public class RespuestaAPI
    {
        public string DepartureStation { get; set; }
        public string ArrivalStation { get; set; }
        public DateTime DepartureDate { get; set; }
        public int FlightNumber { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
    }
    public class GenericModel
    {
        public List<RespuestaAPI> listRespuestaAPI { get; set; }
        public string nombreContacto { get; set; }
        public string cedulaContacto { get; set; }
        public string telefonoContacto { get; set; }
        public string emailContacto { get; set; }
        public string nombrePasajero { get; set; }
        public int IdReserva { get; set; }
        public string fechaIda { get; set; }
    }
    public class ReservaNro
    {
        public int Id { get; set; }
    }
}
