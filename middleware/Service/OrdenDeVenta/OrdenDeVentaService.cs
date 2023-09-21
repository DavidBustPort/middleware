using middleware.Models.API;
using System;
using System.Collections.Generic;
using System.Linq;
using static middleware.Models.API.Request;

namespace middleware.Service.OrdenDeVenta
{
    public class OrdenDeVentaService
    {
        public List<Request_OrdenDeVenta> ObtenerConfirmaciones()
        {
            var result = new List<Request_OrdenDeVenta>();
            var data = new Call().ObtenerConfirmaciones();

            if (data.Count > 0)
            {
                var now = DateTime.Now.ToShortDateString();
                var groupByFolio = data.GroupBy(x => x.Folio).ToList();

                foreach (var folio in groupByFolio)
                {
                    var f = folio.First();
                    var req = new Request_OrdenDeVenta
                    {
                        IdCliente = f.Cliente,
                        FechaDocumento = now,
                        FechaContabilizacion = now,
                        Referencia = f.DocEntry,
                        Serie = "7",
                        FechaVencimiento = now,
                        Comentarios = "Recibo orden de venta",
                        LLineas = new List<Lineas>()
                    };

                    foreach (var lineas in folio)
                    {
                        req.LLineas.Add(new Lineas
                        {
                            IdArticulo = lineas.Sku.Trim(),
                            Id_Fol_Ped = lineas.Folio,
                            Cantidad = (int)lineas.Cantidad,
                            FechaEnvio = now,
                            BaseEntry = lineas.DocEntry
                        });
                    }

                    result.Add(req);
                }
            }

            return result;
        }

        public List<Request_OrdenesEnProceso> ObtenerOrdenesEnProceso()
        {
            var result = new List<Request_OrdenesEnProceso>();
            var oc = new Call().ObtenerOrdenesEnProceso();

            if (oc.Count > 0)
            {
                foreach (var f in oc)
                {
                    result.Add(new Request_OrdenesEnProceso
                    {
                        DocEntry = f.DocEntry.ToString(),
                        Status = f.ProcesoWMS,
                        Folio = f.Id_Folio_Ped.ToString()
                    });
                }
            }

            return result;
        }

        public void ActualizarSurtido(List<Lineas> lineas)
        {
            new Update().ActualizarSurtido(lineas);
        }

        public void ActualizarEnProceso(string folio, string docEntry)
        {
            new Update().ActualizarEnProceso(folio, docEntry);
        }
    }
}
