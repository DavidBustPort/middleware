using middleware.Models.API;
using middleware.Service;
using middleware.Service.SurtidoCiego;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace middleware.API.Request
{
    public class SurtidoCiego : Base
    {
        public SurtidoCiego(string token)
        {
            Token = token;
            Request();
        }

        private void Request()
        {
            var sc = new SurtidoCiegoService();
            var modelReq = sc.ObtenerSurtidosCiego();
            if (modelReq.Count == 0) return;

            //URL += $"{RFC}/inventario/stock/salidamercancia";
            //var client = new HttpClient
            //{
            //    BaseAddress = new Uri(URL)
            //};
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);

            //foreach (var req in modelReq)
            //{
            //    var json = JsonConvert.SerializeObject(req);
            //    var data = new StringContent(json, Encoding.UTF8, "application/json");

            //    var log = new LogDb();
            //    log.SaveReq("surtido ciego", json);

            //    //var response = POST(client, data);
            //    Response response = null;
            //    log.UpdateReq(response.Encabezadoo.Exito, response.Encabezadoo.Mensaje);

            //    if (response.Encabezadoo.Exito) sc.ActualizarSurtido(req.LLineas);
            //}
        }
    }
}
