using middleware.API.Request;
using System;

namespace middleware.API
{
    public class ApiRequests
    {
        public ApiRequests()
        {
            string token = Token.GetToken();

            //Log.Write("inicia ordenes de venta/compra en proceso");
            //new OrdenCompraVentaEnProceso(token);

            //try
            //{
            //    Log.Write("inicia confirmacion orden de compra");
            //    new OrdenDeCompra(token);
            //} catch (Exception ex) { Log.Write($"Error orden de compra: {ex.Message}"); }

            //try
            //{
            //    Log.Write("inicia confirmacion orden de venta");
            //    new OrdenDeVenta(token);
            //}
            //catch (Exception ex) { Log.Write($"Error orden de venta: {ex.Message}"); }

            try
            {
                Log.Write("inicia recibos ciegos");
                new ReciboCiego(token);
            }
            catch (Exception ex) { Log.Write($"Error recibo ciego: {ex.Message}"); }

            try
            {
                Log.Write("inicia surtidos ciego");
                new SurtidoCiego(token);
            }
            catch (Exception ex) { Log.Write($"Error surtido ciego: {ex.Message}"); }
        }
    }
}
