using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace PriApi.Model
{
    public class Warehouse
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        public Collection<ProductModel> Products { get; set; }

        public Warehouse()
        {
            Products = new Collection<ProductModel>();
        }
    }
}
