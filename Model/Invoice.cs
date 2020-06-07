using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PriApi.Model
{
    public class Invoice
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("customer")]
        public string Customer { get; set; }

        [JsonProperty("document")]
        public string Document { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("Itens")]
        public List<Invoice_Itens> Itens { get; set; }

        [JsonProperty("Customer")]
        public Customer _Customer { get; set; }

        public Invoice()
        {
            Itens = new List<Invoice_Itens>();
        }
    }

    public class Invoice_Itens
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("product")]
        public string Product { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("qty")]
        public double Qty { get; set; }

        [JsonProperty("UnityPrice")]
        public double PriceUnit { get; set; }

        [JsonProperty("total")]
        public double Total { get; set; }

        [JsonProperty("vat")]
        public double Vat { get; set; }

        [JsonProperty("discount")]
        public double Discount { get; set; }

        [JsonProperty("Product")]
        public Product _Product { get; set; }
    }
}
