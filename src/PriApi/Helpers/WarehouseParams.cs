using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriApi.Helpers
{
    public class WarehouseParams
    {
        public string Code { get; set; }

        public string Description { get; set; }

        public string Product { get; set; }

        public bool HaveStock { get; set; }

        public string FullSearch { get; set; }

    }
}
