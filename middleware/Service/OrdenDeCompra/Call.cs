using Dapper;
using middleware.Database;
using middleware.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace middleware.Service.OrdenDeCompra
{
    public class Call : ConnectionBase
    {
        public List<OrdenDeCompraDb> ObtenerConfirmaciones()
        {
            using (IDbConnection db = new SqlConnection(connection))
            {
                db.Open();

                return db.Query<OrdenDeCompraDb>
                    (
                        "sp_KQRecibosOrdenesDeCompra_procesados",
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
                p.Id_Folio_Ped
                from {prefixDb}..rcDocPedido p 
                inner join KQDocNumEntry kq on p.Id_Folio_Ped = kq.id_fol_ped").ToList();
            }
        }
    }
}
