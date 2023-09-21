using Newtonsoft.Json;

namespace middleware.Models.API
{
    public class Authentication
    {
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
