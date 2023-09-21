using Newtonsoft.Json;

namespace middleware.Models.API
{
    public class Request_OrdenesEnProceso
    {
        [JsonProperty("status")]
        public bool Status { get; set; }
        [JsonProperty("docEntry")]
        public string DocEntry { get; set; }

        [JsonIgnore]
        public string Folio { get; set; }
    }
}
