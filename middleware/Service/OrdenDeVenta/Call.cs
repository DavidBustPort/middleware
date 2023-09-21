using Dapper;
using middleware.Database;
using middleware.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace middleware.Service.OrdenDeVenta
{
    public class Call : ConnectionBase
    {
        public List<OrdenDeVentaDb> ObtenerConfirmaciones()
        {
            using (IDbConnection db = new SqlConnection(connection))
            {
                db.Open();

                return db.Query<OrdenDeVentaDb>
                    (
                        "sp_KQSurtidosOrdenesDeVenta",
                        new { },
                        commandType: CommandType.StoredProcedure
                    ).ToList();
            }
        }

        public List<OrdenesEnProcesoDb> ObtenerOrdenesEnProceso()
        {
            using (IDbConnection db = new SqlConnection(connection))
            {
                db.Open();

                return db.Query<OrdenesEnProcesoDb>($@"
                select distinct
                kq.docEntry,
                kq.procesoWMS,
                p.Id_Fol_Ped as Id_Folio_Ped
                from {prefixDb}..Ruta_PedTdaCod p 
                inner join KQDocNumEntry kq on p.Id_Fol_Ped = kq.id_fol_ped").ToList();
            }
        }
    }
}
