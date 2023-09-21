using middleware.Models.API;
using middleware.SAP;
using System;
using System.Collections.Generic;
using System.Linq;
using static middleware.Models.API.Request;

namespace middleware.Service.ReciboCiego
{
    public class ReciboCiegoService
    {
        public List<Request_ReciboCiego> ObtenerRecibosCiego()
        {
            var result = new List<Request_ReciboCiego>();
            var rc = new Call().ObtenerRecibos();

            if (rc.Count > 0)
            {
                var now = DateTime.Now.ToShortDateString();
                var group = rc
                    .GroupBy(x => new { x.Motivo, x.Sku, x.FolioTarima })
                    .Select(g => new
                    {
                        Cantidad = g.Sum(x => x.Cantidad),
                        g.Key.Motivo,
                        g.First().FolioTarima,
                        g.Key.Sku,
                        g.First().Folio,
                        g.First().Fecha
                    })
                    .ToList();

                var oign = new OIGN_reciboCiego();
                foreach (var mtvo in group)
                {
                    string _ref = $"{mtvo.FolioTarima}_{mtvo.Sku}";

                    if (!oign.ValidateIfExists(mtvo.Sku, _ref))
                    {
                        var reciboCiego = new Request_ReciboCiego
                        {
                            FechaDocumento = mtvo.Fecha.ToShortDateString(),
                            FechaContabilizacion = now,
                            FechaVencimiento = now,
                            //Referencia = "12345",
                            Referencia = _ref,
                            Serie = "21",
                            Comentarios = $"{mtvo.Motivo} - {mtvo.Folio}",
                            LLineas = new List<Lineas>
                        {
                            new Lineas
                            {
                                IdArticulo = mtvo.Sku,
                                id_fol_tarima = mtvo.FolioTarima,
                                Precio = null,
                                Cantidad = mtvo.Cantidad,
                                FechaEnvio = now
                            }
                        },
                            ListaPrecio = -2
                        };

                        result.Add(reciboCiego);
                    }
                    else
                    {

                    }
                }
            }

            return result;
        }

        public void ActualizarRecibo(List<Lineas> lineas)
        {
            new Update().ActualizarRecibo(lineas);
        }
    }
}
