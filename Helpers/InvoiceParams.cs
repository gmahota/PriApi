using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriApi.Model.Helper
{
    public class InvoiceParams
    {
        public DateTime DateBegin { get; set; }

        public DateTime DateEnd { get; set; }

        public string Entity { get; set; }

        public string Document { get; set; }

        public string FullSearch { get; set; }
    }
}
