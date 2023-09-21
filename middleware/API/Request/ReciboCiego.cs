using middleware.Service;
using middleware.Service.ReciboCiego;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace middleware.API.Request
{
    public class ReciboCiego : Base
    {
        public ReciboCiego(string token)
        {
            Token = token;
            Request();
        }

        private void Request()
        {
            var rc = new ReciboCiegoService();
            var data = rc.ObtenerRecibosCiego();
            if (data.Count == 0) return;

            //URL += $"{RFC}/inventario/stock/entradamercancia";
            //var client = new HttpClient
            //{
            //    BaseAddress = new Uri(URL)
            //};
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);

            //foreach (var req in data)
            //{
            //    var json = JsonConvert.SerializeObject(req);
            //    var sc = new StringContent(json, Encoding.UTF8, "application/json");

            //    var log = new LogDb();
            //    log.SaveReq("recibo ciego", json);

            //    var response = POST(client, sc);
            //    log.UpdateReq(response.Encabezadoo.Exito, response.Encabezadoo.Mensaje);

            //    if (response.Encabezadoo.Exito) rc.ActualizarRecibo(req.LLineas);
            //}
        }
    }
}
