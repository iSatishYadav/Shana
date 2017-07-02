using Newtonsoft.Json;

namespace Shana.Models.Instargam
{
    public class Counts
    {
        [JsonProperty("media")]
        public int Media { get; set; }
        [JsonProperty("follows")]
        public int Follows { get; set; }
        [JsonProperty("followed_by")]
        public int FollowedBy { get; set; }
    }
}