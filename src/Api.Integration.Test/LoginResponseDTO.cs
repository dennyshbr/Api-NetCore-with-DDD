using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Integration.Test
{
    public class LoginResponseDTO
    {
        [JsonProperty("authenticated")]
        public bool authenticated { get; set; }

        [JsonProperty("created")]
        public string created { get; set; }

        [JsonProperty("expiration")]
        public string expiration { get; set; }

        [JsonProperty("acessToken")]
        public string acessToken { get; set; }

        [JsonProperty("userName")]
        public string userName { get; set; }
        
        [JsonProperty("message")]
        public string message { get; set; }
    }
}
