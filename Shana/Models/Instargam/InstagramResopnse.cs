using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shana.Models.Instargam
{
    public class InstagramResopnse<T>
    {
        [JsonProperty("meta")]
        public MetaData Meta { get; set; }

        [JsonProperty("data")]
        public T Data { get; set; }

        [JsonProperty("pagination")]
        public Pagination Pagination { get; set; }
    }
}