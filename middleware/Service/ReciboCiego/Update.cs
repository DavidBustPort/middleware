using Dapper;
using middleware.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using static middleware.Models.API.Request;

namespace middleware.Service.ReciboCiego
{
    public class Update : ConnectionBase
    {
        public void ActualizarRecibo(List<Lineas> lineas)
        {
            using (IDbConnection db = new SqlConnection(connection))
            {
                db.Open();
                var row = lineas.First();

                db.Execute($@"
                update {prefixDb}..ReciboReplic_ReciboCiego_ReplicStat_e
                set Id_Num_ReplicStat = 2
                where Id_Num_ReplicStat = 1 and Id_Fol_Tarima = @folio and Id_Num_SKU = @sku",
                new
                {
                    folio = row.id_fol_tarima,
                    sku = row.IdArticulo
                });
            }
        }
    }
}
