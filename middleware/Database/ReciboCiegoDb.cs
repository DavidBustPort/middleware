using System;

namespace middleware.Database
{
    public class ReciboCiegoDb
    {
        public string Folio { get; set; }
        public string Sku { get; set; }
        public string Motivo { get; set; }
        public int FolioTarima { get; set; }
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; }
    }
}
