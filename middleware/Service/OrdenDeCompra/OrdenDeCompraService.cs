using middleware.Models.API;
using System;
using System.Collections.Generic;
using System.Linq;
using static middleware.Models.API.Request;

namespace middleware.Service.OrdenDeCompra
{
    public class OrdenDeCompraService
    {
        public List<Request_OrdenDeCompra> ObtenerConfirmaciones()
        {
            var result = new List<Request_OrdenDeCompra>();
            var data = new Call().ObtenerConfirmaciones();

            if (data.Count > 0)
            {
                var all = data.Where(x => x.FactorBase.HasValue && x.FactorBase.Value > 0);

                var now = DateTime.Now.ToShortDateString();
                var groupByCyan = all
                    .Where(x => !string.IsNullOrEmpty(x.FolioCyan))
                    .GroupBy(n => n.FolioCyan)
                    .ToList();
                var groupByAnothers = all
                    .Where(x => string.IsNullOrEmpty(x.FolioCyan))
                    .GroupBy(n => new { n.Folio, n.Factura })
                    .Select(g => new
                    {
                        g.First().Cliente,
                        g.Key.Factura,
                        g.Key.Folio,
                        g.First().Sku,
                        g.First().Cantidad,
                        g.First().FactorBase,
                        g.First().DocEntry,
                        g.First().BaseLine
                    }).ToList();

                foreach (var folio in groupByCyan)
                {
                    var f = folio.First();
                    var req = new Request_OrdenDeCompra
                    {
                        IdCliente = f.Cliente,
                        FechaDocumento = now,
                        FechaContabilizacion = now,
                        IsCyan = true,
                        FolioCyan = f.FolioCyan,
                        Referencia = f.Factura,
                        CyanReference = f.Folio,
                        FechaVencimiento = now,
                        Comentarios = "Recibo orden de compra",
                        LLineas = new List<Lineas>()
                    };

                    foreach (var lineas in folio)
                    {
                        req.LLineas.Add(new Lineas
                        {
                            IdArticulo = lineas.Sku.Trim(),
                            Id_Fol_Ped = lineas.Folio,
                            Cantidad = (int)lineas.Cantidad / lineas.FactorBase.Value,
                            FechaEnvio = now,
                            BaseEntry = lineas.DocEntry,
                            BaseLine = lineas.BaseLine
                        });
                    }

                    result.Add(req);
                }

                foreach (var f in groupByAnothers)
                {
                    var req = new Request_OrdenDeCompra
                    {
                        IdCliente = f.Cliente,
                        FechaDocumento = now,
                        FechaContabilizacion = now,
                        IsCyan = false,
                        FolioCyan = "",
                        Referencia = f.Factura,
                        CyanReference = f.Folio,
                        FechaVencimiento = now,
                        Comentarios = "Recibo orden de compra",
                        LLineas = new List<Lineas>
                        {
                            new Lineas
                            {
                                IdArticulo = f.Sku.Trim(),
                                Id_Fol_Ped = f.Folio,
                                Cantidad = (int)f.Cantidad / f.FactorBase.Value,
                                FechaEnvio = now,
                                BaseEntry = f.DocEntry,
                                BaseLine = f.BaseLine
                            }
                        }
                    };

                    result.Add(req);
                }

                //var groupByFolio = all.GroupBy(x => x.Folio).ToList();

                //foreach (var folio in groupByFolio)
                //{
                //    var f = folio.First();
                //    var req = new Request_OrdenDeCompra
                //    {
                //        IdCliente = f.Cliente,
                //        FechaDocumento = now,
                //        FechaContabilizacion = now,
                //        IsCyan = !string.IsNullOrEmpty(f.FolioCyan),
                //        FolioCyan = f.FolioCyan,
                //        Referencia = f.Factura,
                //        CyanReference = f.Folio,
                //        FechaVencimiento = now,
                //        Comentarios = "Recibo orden de compra",
                //        LLineas = new List<Lineas>()
                //    };

                //    foreach (var lineas in folio)
                //    {
                //        req.LLineas.Add(new Lineas
                //        {
                //            IdArticulo = lineas.Sku.Trim(),
                //            Id_Fol_Ped = lineas.Folio,
                //            Cantidad = (int)lineas.Cantidad / lineas.FactorBase.Value,
                //            FechaEnvio = now,
                //            BaseEntry = lineas.DocEntry,
                //            BaseLine = lineas.BaseLine
                //        });
                //    }

                //    result.Add(req);
                //}
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

        public void ActualizarRecibo(Request_OrdenDeCompra req)
        {
            new Update().ActualizarRecibo(req);
        }

        public void ActualizarEnProceso(string folio, string docEntry)
        {
            new Update().ActualizarEnProceso(folio, docEntry);
        }
    }
}
