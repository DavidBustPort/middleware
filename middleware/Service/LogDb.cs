using Dapper;
using middleware.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace middleware.Service
{
    public class LogDb : ConnectionBase
    {
        public int Identity { get; set; }

        public void SaveReq(string tipo, string json)
        {
            using (IDbConnection db = new SqlConnection(connection))
            {
                db.Open();
                try
                {
                    Identity = db.QuerySingle<int>(@"
                    insert into KQBITACORAWMS
                    (tipo_solicitud, comando, json_solicitud, fecha_solicitud, estatus)
                    values
                    (@tipo, @command, @json, @date, @status);
                    select cast(scope_identity() as int)",
                    new
                    {
                        tipo,
                        command = "",
                        json,
                        date = DateTime.Now,
                        status = false
                    });

                }
                catch { }
            }
        }

        public void UpdateReq(bool status, string error)
        {
            using (IDbConnection db = new SqlConnection(connection))
            {
                db.Open();
                try
                {
                    db.Execute(@"
                    update KQBITACORAWMS
                    set error = @error,
                    estatus = @status
                    where id = @id",
                    new
                    {
                        error,
                        status,
                        id = Identity
                    });
                }
                catch { }
            }
        }
    }
}
