using System.Data.SqlClient;
using System.Data;
using middleware.Models;
using Dapper;

namespace middleware.SAP
{
    public class OIGN_reciboCiego : ConnectionBase
    {
        public bool ValidateIfExists(string sku, string reference)
        {
            using (IDbConnection db = new SqlConnection(connectionSAP))
            {
                db.Open();

                return db.ExecuteScalar<bool>(@"
                select count(1) from OIGN o inner join IGN1 i on o.DocEntry = i.DocEntry where i.ItemCode = @sku and NumAtCard = @reference",
                new
                {
                    sku,
                    reference
                });
            }
        }
    }
}
