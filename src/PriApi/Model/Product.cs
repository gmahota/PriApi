using Newtonsoft.Json;

namespace PriApi.Model
{
    public class Product
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("pvp1")]
        public double Pvp1 { get; set; }

        [JsonProperty("pvp2")]
        public double Pvp2 { get; set; }

        [JsonProperty("pvp3")]
        public double Pvp3 { get; set; }

        [JsonProperty("pvp4")]
        public double Pvp4 { get; set; }

        [JsonProperty("pvp5")]
        public double Pvp5 { get; set; }

        [JsonProperty("pvp6")]
        public double Pvp6 { get; set; }
    }
}
