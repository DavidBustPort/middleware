using middleware.Service;
using middleware.Service.OrdenDeCompra;
using middleware.Service.OrdenDeVenta;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace middleware.API.Request
{
    public class OrdenCompraVentaEnProceso : Base
    {
        public OrdenCompraVentaEnProceso(string token)
        {
            Token = token;

            try
            {
                OrdenDeCompra();
            }
            catch (Exception ex) { Log.Write($"Error orden de compra en proceso: {ex.Message}"); }

            try
            {
                OrdenDeVenta();
            }
            catch (Exception ex) { Log.Write($"Error orden de venta en proceso: {ex.Message}"); }
        }


        private void OrdenDeCompra()
        {
            var oc = new OrdenDeCompraService();
            var data = oc.ObtenerOrdenesEnProceso();

            if (data.Count > 0)
            {
                if (data.All(x => x.Status)) return;

                foreach (var req in data.Where(x => !x.Status))
                {
                    URL += $"{RFC}/compras/procesowms?status=true&docEntry={req.DocEntry}";
                    var client = new HttpClient
                    {
                        BaseAddress = new Uri(URL)
                    };
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);

                    var log = new LogDb();
                    log.SaveReq("notif. orden de compra en proceso", URL);

                    var response = POST(client, null);
                    log.UpdateReq(response.Encabezadoo.Exito, response.Encabezadoo.Mensaje);

                    if (response.Encabezadoo.Exito) oc.ActualizarEnProceso(req.Folio, req.DocEntry);
                }
            }
        }

        private void OrdenDeVenta()
        {
            var oc = new OrdenDeVentaService();
            var model = oc.ObtenerOrdenesEnProceso();

            if (model.Count > 0)
            {
                if (model.All(x => x.Status)) return;

                foreach (var req in model.Where(x => !x.Status))
                {
                    URL += $"{RFC}/ventas/procesowms?status=true&docEntry={req.DocEntry}";
                    var client = new HttpClient
                    {
                        BaseAddress = new Uri(URL)
                    };
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);

                    var log = new LogDb();
                    log.SaveReq("notif. orden de venta en proceso", URL);

                    var response = POST(client, null);
                    log.UpdateReq(response.Encabezadoo.Exito, response.Encabezadoo.Mensaje);

                    if (response.Encabezadoo.Exito) oc.ActualizarEnProceso(req.Folio, req.DocEntry);
                }
            }
        }
    }
}
