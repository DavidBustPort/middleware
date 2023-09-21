using Dapper;
using middleware.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using static middleware.Models.API.Request;

namespace middleware.Service.SurtidoCiego
{
    public class Update : ConnectionBase
    {
        public void ActualizarSurtido(List<Lineas> lineas)
        {
            using (IDbConnection db = new SqlConnection(connection))
            {
                db.Open();
                var row = lineas.First();

                foreach (var x in lineas)
                {
                    db.Execute($@"
                    update {prefixDb}..TransfReplic_SurtidoCiego_ReplicStat_e
                    set Id_Num_ReplicStat = 2
                    where Id_Num_ReplicStat = 1 and Id_Fol_Tarima = @folio and Id_Num_SKU = @sku",
                   new
                   {
                       folio = x.id_fol_tarima,
                       sku = x.IdArticulo
                   });
                }

               
            }
        }
    }
}
