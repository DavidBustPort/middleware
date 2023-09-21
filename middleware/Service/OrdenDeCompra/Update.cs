using Dapper;
using middleware.Models;
using middleware.Models.API;
using System;
using System.Data;
using System.Data.SqlClient;

namespace middleware.Service.OrdenDeCompra
{
    public class Update : ConnectionBase
    {
        public void ActualizarRecibo(Request_OrdenDeCompra req)
        {
            using (IDbConnection db = new SqlConnection(connection))
            {
                db.Open();
                foreach (var li in req.LLineas)
                {
                    db.Execute($@"
                        update {prefixDb}..PedReciboReplicStat
                        set Id_Num_ReplicStat = 2
                        where Id_Num_ReplicStat = 1 and Id_Folio_Ped = @folio and Id_Num_SKU = @sku",
                    new
                    {
                        folio = li.Id_Fol_Ped,
                        sku = li.IdArticulo
                    });
                }
            }

            if (req.IsCyan) ActualizarCyan(req.CyanReference, req.FolioCyan);
        }

        private void ActualizarCyan(string folio, string oc_no)
        {
            using (IDbConnection db = new SqlConnection(connection))
            {
                db.Open();

                db.Execute(@"
                    update KQREMISIONESCYAN
                    set Entrada_status = 1,
                    entradaKSL_dt = @date
                    where folio_entrega = @folio
                    and oc_no = @oc_no",
                new
                {
                    date = DateTime.Now,
                    folio,
                    oc_no
                });
            }
        }

        public void ActualizarEnProceso(string folio, string docEntry)
        {
            using (IDbConnection db = new SqlConnection(connection))
            {
                db.Open();

                db.Execute(@"
                    update KQDocNumEntry
                    set procesoWMS = 1
                    where id_fol_ped = @folio and docEntry = @docEntry",
                    new
                    {
                        folio,
                        docEntry
                    });
            }
        }
    }
}
