using middleware.Models.API;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using static middleware.Models.API.Response;

namespace middleware.API
{
    public class Base
    {
        public string Token { get; set; }
        public const string RFC = "CPD900516RX2";
        public string URL = "http://keyquimica.vertrou.mx:81/corponet/api/v1/";

        public Response POST(HttpClient client, StringContent data)
        {
            var result = new Response();
            try
            {
                var response = client.PostAsync(URL, data).Result;
                result = JsonConvert.DeserializeObject<Response>(response.Content.ReadAsStringAsync().Result);
                if (result.Encabezadoo == null)
                {
                    result.Encabezadoo = new Encabezado
                    {
                        Exito = false,
                        Mensaje = "error al obtener el resultado API"
                    };
                }
            }
            catch (Exception ex)
            {
                result.Encabezadoo = new Encabezado
                {
                    Mensaje = ex.Message,
                    Exito = false
                };
            }

            return result;
        }
    }
}
