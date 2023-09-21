using System;

namespace middleware.Database
{
    public class OrdenDeCompraDb
    {
        public string Folio { get; set; }
        public string Sku { get; set; }
        public decimal Cantidad { get; set; }
        public DateTime Fecha { get; set; }
        public string DocEntry { get; set; }
        public int BaseLine { get; set; }
        public string FolioCyan { get; set; }
        public string Factura { get; set; }
        public string Cliente { get; set; }
        public bool ProcesoWMS { get; set; }
        public int? FactorBase { get; set; }
    }
}
