using middleware.Models.API;
using middleware.SAP;
using System;
using System.Collections.Generic;
using System.Linq;
using static middleware.Models.API.Request;

namespace middleware.Service.SurtidoCiego
{
    public class SurtidoCiegoService
    {
        public List<Request_SurtidoCiego> ObtenerSurtidosCiego()
        {
            var result = new List<Request_SurtidoCiego>();
            var sc = new Call().ObtenerSurtidos();
            
            if (sc.Count > 0)
            {
                var now = DateTime.Now.ToShortDateString();
                var group = sc
                    .GroupBy(x => new { x.Motivo, x.Sku, x.FolioTarima, x.FolioTarimaId })
                    .Select(g => new
                    {
                        Cantidad = g.Sum(x => x.Cantidad),
                        g.Key.Motivo,
                        g.First().FolioTarima,
                        g.First().FolioTarimaId,
                        g.First().Id_Cnsc_MtvoSurtido,
                        g.Key.Sku,
                        g.First().Fecha,
                    })
                    .ToList();

                var oige = new OIGE_surtidoCiego();
                foreach (var mtvo in group)
                {
                    string _ref = $"{mtvo.Id_Cnsc_MtvoSurtido} - {mtvo.Motivo}";
                    string refTwo = $"{mtvo.FolioTarimaId}_{mtvo.Sku}";

                    if (oige.ValidateIfExists(mtvo.Sku, refTwo))
                    {
                        var reciboCiego = new Request_SurtidoCiego
                        {
                            FechaDocumento = mtvo.Fecha.ToShortDateString(),
                            FechaContabilizacion = now,
                            //Referencia = _ref.Length > 20 ? _ref.Substring(0, 20) : _ref,
                            Referencia = refTwo,
                            Serie = "22",
                            FechaVencimiento = now,
                            Comentarios = $"{mtvo.Id_Cnsc_MtvoSurtido} - {mtvo.Motivo}",
                            LLineas = new List<Lineas>
                        {
                            new Lineas
                            {
                                IdArticulo = mtvo.Sku,
                                id_fol_tarima = mtvo.FolioTarimaId,
                                Cantidad = mtvo.Cantidad,
                                FechaEnvio = now
                            }
                        }
                        };

                        result.Add(reciboCiego);
                    }
                }
            }

            return result;
        }

        public void ActualizarSurtido(List<Lineas> lineas)
        {
            new Update().ActualizarSurtido(lineas);
        }
    }
}
