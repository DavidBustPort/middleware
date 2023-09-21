using System;

namespace middleware.Database
{
    public class SurtidoCiegoDb
    {
        public string Folio { get; set; }
        public string Sku { get; set; }
        public string Motivo { get; set; }
        public string FolioTarima { get; set; }
        public int FolioTarimaId { get; set; }
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; }
        public string Id_Cnsc_MtvoSurtido { get; set; }
    }
}
