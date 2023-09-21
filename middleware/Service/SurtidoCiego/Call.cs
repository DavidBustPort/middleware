using Dapper;
using middleware.Database;
using middleware.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace middleware.Service.SurtidoCiego
{
    public class Call : ConnectionBase
    {
        public List<SurtidoCiegoDb> ObtenerSurtidos()
        {
            using (IDbConnection db = new SqlConnection(connection))
            {
                db.Open();
                return db.Query<SurtidoCiegoDb>
                    (
                        "sp_KQSurtidosCiegos",
                        new { },
                        commandType: CommandType.StoredProcedure
                    ).ToList();
            }
        }
    }
}
