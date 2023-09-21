using Newtonsoft.Json;

namespace middleware.Models.API
{
    public class AuthenticationRes
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
