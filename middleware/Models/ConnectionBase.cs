using System.Configuration;

namespace middleware.Models
{
    public class ConnectionBase
    {
        public readonly string connection = "";
        public readonly string connectionSAP = "";
        public readonly string prefixDb = ConfigurationManager.AppSettings["prefixDb"].ToString();

        public ConnectionBase()
        {
            connection = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
            connectionSAP = ConfigurationManager.ConnectionStrings["connection_sap"].ConnectionString;
        }
    }
}
