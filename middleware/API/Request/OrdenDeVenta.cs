using middleware.Service;
using middleware.Service.OrdenDeVenta;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace middleware.API.Request
{
    public class OrdenDeVenta : Base
    {
        public OrdenDeVenta(string token)
        {
            Token = token;
            Request();
        }

        private void Request()
        {
            var ov = new OrdenDeVentaService();
            var data = ov.ObtenerConfirmaciones();
            if (data.Count == 0) return;

            URL += $"{RFC}/ventas/entrega";
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
                log.SaveReq("confirmacion orden de venta", json);

                var response = POST(client, sc);
                log.UpdateReq(response.Encabezadoo.Exito, response.Encabezadoo.Mensaje);

                if (response.Encabezadoo.Exito) ov.ActualizarSurtido(req.LLineas);
            }
        }
    }
}
