using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shana.Models.Instargam
{
    public class Token
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("user")]
        public InstagramUser User { get; set; }
    }
}