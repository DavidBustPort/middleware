using middleware.Service;
using middleware.Service.OrdenDeCompra;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace middleware.API.Request
{
    public class OrdenDeCompra : Base
    {
        public OrdenDeCompra(string token)
        {
            Token = token;
            Request();
        }

        private void Request()
        {
            var oc = new OrdenDeCompraService();
            var data = oc.ObtenerConfirmaciones();
            if (data.Count == 0) return;

            URL += $"{RFC}/compras/entradamercancias";
            var client = new HttpClient
            {
                BaseAddress = new Uri(URL)
            };
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);

            foreach (var req in data)
            {
                var json = JsonConvert.SerializeObject(req);
                var sc = new StringContent(json, Encoding.UTF8, "application/json");

                var log = new LogDb();
                log.SaveReq("confirmacion orden de compra", json);

                var response = POST(client, sc);
                log.UpdateReq(response.Encabezadoo.Exito, response.Encabezadoo.Mensaje);

                if (response.Encabezadoo.Exito) oc.ActualizarRecibo(req);
            }
        }
    }
}
