using System;

namespace middleware.Database
{
    public class OrdenDeVentaDb
    {
        public string Folio { get; set; }
        public string Sku { get; set; }
        public decimal Cantidad { get; set; }
        public DateTime Fecha { get; set; }
        public string DocEntry { get; set; }
        public string Cliente { get; set; }
        public bool ProcesoWMS { get; set; }
    }
}
