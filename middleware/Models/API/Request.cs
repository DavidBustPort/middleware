using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace middleware.Models.API
{
    public class Request
    {
        [JsonProperty("idCliente")]
        public string IdCliente { get; set; }

        [JsonProperty("fechaDocumento")]
        public string FechaDocumento { get; set; }

        [JsonProperty("fechaContabilizacion")]
        public string FechaContabilizacion { get; set; }

        [JsonProperty("referencia")]
        public string Referencia { get; set; }

        [JsonProperty("fechaVencimiento")]
        public string FechaVencimiento { get; set; }

        [JsonProperty("serie")]
        public string Serie { get; set; } = "13";

        [JsonProperty("idVendedor")]
        public int IdVendedor { get; set; } = -1;

        [JsonProperty("comentarios")]
        public string Comentarios { get; set; }

        [JsonProperty("moneda")]
        public string Moneda { get; set; } = "MXP";

        [JsonProperty("esUnidadBase")]
        public bool EsUnidadBase { get; set; } = false;

        [JsonProperty("lineas")]
        public List<Lineas> LLineas { get; set; }

        [JsonProperty("listaPrecio")]
        public int ListaPrecio { get; set; }


        [JsonIgnore]
        public bool IsCyan { get; set; } = false;
        [JsonIgnore]
        public string CyanReference { get; set; }

        public class Lineas
        {
            [JsonProperty("idArticulo")]
            public string IdArticulo { get; set; }

            [JsonProperty("cantidad")]
            public int Cantidad { get; set; }

            [JsonProperty("precio")]
            public double? Precio { get; set; }

            [JsonProperty("Descuento")]
            public double descuento { get; set; }

            public string FechaEnvio { get; set; }

            [JsonProperty("almacen")]
            public string Almacen { get; set; } = "100";
            public string CentroCostos { get; set; } = "";
            public string CentroCostos2 { get; set; } = "";
            public string CentroCostos3 { get; set; } = "";

            [JsonProperty("baseEntry")]
            public string BaseEntry { get; set; }

            [JsonProperty("baseLine")]
            public int BaseLine { get; set; }

            [JsonIgnore]
            public int id_fol_tarima { get; set; }
            [JsonIgnore]
            public string Id_Fol_Ped { get; set; }
        }
    }
}
