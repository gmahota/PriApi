using Newtonsoft.Json;

namespace PriApi.Model
{
    public class Customer
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
