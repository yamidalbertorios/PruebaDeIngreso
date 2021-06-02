using AccesoADatos;
using Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Logica
{
    public class AdministrarVuelos
    {
        PruebaIngresoEntities db = new PruebaIngresoEntities();

        public List<string> getListaDeCiudades()
        {
            try
            {
                var cities = db.Ciudades.AsNoTracking().Distinct().ToList();
                List<string> ListCities = new List<string>();
                foreach (var item in cities)
                {
                    ListCities.Add(item.Codigo.Trim() + "|" + item.Nombre.Trim());
                }
                return ListCities;
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

        public List<RespuestaAPI> getRespuestaAPI(string codigoOrigen, string codigoDestino, DateTime Desde, string metodoAPI)
        {
            try
            {
                //Para efectos de prueba sin consumo de API
                if (metodoAPI.Equals("no"))
                {
                    string quote = "\"";
                    var json = "[{" + quote + "DepartureDate" + quote + ":" + quote + "2019-08-01T07:35:00" + quote + ",";
                    json = json + quote + "DepartureStation" + quote + ":" + quote + "MDE" + quote + ",";
                    json = json + quote + "ArrivalStation" + quote + ":" + quote + "BOG" + quote + ",";
                    json = json + quote + "FlightNumber" + quote + ":" + quote + "8020" + quote + ",";
                    json = json + quote + "Price" + quote + ":23094.0,";
                    json = json + quote + "Currency" + quote + ":" + quote + "COP" + quote + "}]";

                    var response = JsonConvert.DeserializeObject<List<RespuestaAPI>>(json);
                    return response;
                }
                else
                {
                    const string URL = "http://testapi.vivaair.com/otatest/api/values";
                    string quote = "\"";
                    string DATA = "{" + quote + "Origin" + quote + ":" + quote + codigoOrigen + quote + ",";
                    DATA = DATA + quote + "Destination" + quote + ":" + quote + codigoDestino + quote + ",";
                    DATA = DATA + quote + "From" + quote + ":" + quote + Desde.ToString("yyyy-MM-dd") + quote + "}]";

                    string strResponseValue = string.Empty;
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                    request.Method = "POST";
                    request.ContentType = "application/json";
                    byte[] bytes = Encoding.UTF8.GetBytes(DATA);

                    using (Stream requestStream = request.GetRequestStream())
                    {
                        request.GetRequestStream().Write(bytes, 0, bytes.Length);

                        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                        {
                            if (response.StatusCode != HttpStatusCode.OK)
                            {
                                throw new ApplicationException("Codigo Error : " + response.StatusCode);
                            }

                            using (Stream responseStream = response.GetResponseStream())
                            {
                                if (responseStream != null)
                                {
                                    using (StreamReader reader = new StreamReader(responseStream))
                                    {
                                        strResponseValue = reader.ReadToEnd();
                                    }
                                }
                            }
                        }
                    }

                    //Ajustes adcionales al formato JSON:
                    strResponseValue = strResponseValue.Replace("\\", "");
                    strResponseValue = strResponseValue.Replace("\"[", "[");
                    strResponseValue = strResponseValue.Replace("]\"", "]");

                    var formatoJson = strResponseValue;
                    var respuesta = JsonConvert.DeserializeObject<List<RespuestaAPI>>(formatoJson);
                    return respuesta;
                }
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
        public int guardarResultado(string DepartureStation, string ArrivalStation, DateTime DepartureDate, int FlightNumber, decimal Price, string Currency)
        {
            try
            {
                Flight item = new Flight();
                item.DepartureStation = DepartureStation;
                item.ArrivalStation = ArrivalStation;
                item.DepartureDate = DepartureDate;
                item.Transport = FlightNumber;
                item.Price = Price;
                item.Currency = Currency;
                db.Flight.Add(item);
                db.SaveChanges();

                return item.Id;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    string error = ex.Message + ex.InnerException.ToString();
                    return 0;
                }
                else
                {
                    string error = ex.Message + ex.InnerException.ToString();
                    return 0;
                }
            }
        } 
    }
}
