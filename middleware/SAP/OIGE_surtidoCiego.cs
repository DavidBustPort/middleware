using middleware.Models;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace middleware.SAP
{
    public class OIGE_surtidoCiego : ConnectionBase
    {
        public bool ValidateIfExists(string sku, string reference)
        {
            using (IDbConnection db = new SqlConnection(connectionSAP))
            {
                db.Open();

                return db.ExecuteScalar<bool>(@"
                select count(1) from OIGE o inner join IGE1 i on o.DocEntry = i.DocEntry where i.ItemCode = @sku and NumAtCard = @reference",
                new
                {
                    sku,
                    reference
                });
            }
        }
    }
}
