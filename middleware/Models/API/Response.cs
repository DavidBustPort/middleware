using Newtonsoft.Json;

namespace middleware.Models.API
{
    public class Response
    {
        [JsonProperty("docEntry")]
        public int DocEntry { get; set; }

        [JsonProperty("idTransaccion")]
        public int IdTransaccion { get; set; }

        [JsonProperty("encabezado")]
        public Encabezado Encabezadoo { get; set; }

        public class Encabezado
        {
            [JsonProperty("exito")]
            public bool Exito { get; set; }
            public string CodError { get; set; }
            public string Mensaje { get; set; }
        }
    }
}
