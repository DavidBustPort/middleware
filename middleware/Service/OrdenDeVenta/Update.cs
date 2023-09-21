using Dapper;
using middleware.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using static middleware.Models.API.Request;

namespace middleware.Service.OrdenDeVenta
{
    public class Update : ConnectionBase
    {
        public void ActualizarSurtido(List<Lineas> lineas)
        {
            using (IDbConnection db = new SqlConnection(connection))
            {
                db.Open();
                foreach (var li in lineas)
                {
                    db.Execute($@"
                    update {prefixDb}..PedRutaReplicStat
                    set Id_Num_ReplicStat = 3
                    where Id_Num_ReplicStat = 2 and Id_Folio_Ped = @folio and Id_Num_SKU = @sku",
                    new
                    {
                        folio = li.Id_Fol_Ped,
                        sku = li.IdArticulo
                    });
                }
            }
        }

        public void ActualizarEnProceso(string folio, string docEntry)
        {
            using (IDbConnection db = new SqlConnection(connection))
            {
                db.Open();

                db.Execute(@"
                    update KQDocNumEntry
                    set procesoWMS = 1
                    where id_fol_ped = @folio and docEntry = @docEntry",
                    new
                    {
                        folio,
                        docEntry
                    });
            }
        }
    }
}
