using Dapper;
using middleware.Database;
using middleware.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace middleware.Service.ReciboCiego
{
    public class Call : ConnectionBase
    {
        public List<ReciboCiegoDb> ObtenerRecibos()
        {
            using (IDbConnection db = new SqlConnection(connection))
            {
                db.Open();
                return db.Query<ReciboCiegoDb>
                    (
                        "sp_KQRecibosCiegos_example",
                        new { },
                        commandType: CommandType.StoredProcedure
                    ).ToList();
            }
        }
    }
}
