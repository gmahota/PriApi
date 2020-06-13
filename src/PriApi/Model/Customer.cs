using Newtonsoft.Json;

namespace PriApi.Model
{
    public class Customer
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("discount")]
        public double Discount { get; set; }

        [JsonProperty("typePrice")]
        public string TypePrice { get; set; }
    }
}
