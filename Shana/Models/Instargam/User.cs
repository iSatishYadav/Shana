using Newtonsoft.Json;
using System.Collections.Generic;

namespace Shana.Models.Instargam
{
    public class InstagramUser
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("full_name")]
        public string FullName { get; set; }
        [JsonProperty("profile_picture")]
        public string ProfilePicture { get; set; }
        [JsonProperty("bio")]
        public string Bio { get; set; }
        [JsonProperty("website")]
        public string Website { get; set; }

        public Counts Counts { get; set; }
    }
}