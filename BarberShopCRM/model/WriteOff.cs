using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShopCRM.model {
    class WriteOff {
        public IDictionary<Product, int> ProductsWithCountInUnits { get; set; }
        public DateTime WriteOffDate { get; set; }
        public string MasterName { get; set; }
    }
}
