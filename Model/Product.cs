using Newtonsoft.Json;

namespace PriApi.Model
{
    public class Product
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
