using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShopCRM.model {
    public class ProductWrapper {
        public Product Product { get; set; }
        public int ClosedCount { get; set; }
        public int OpenedCount { get; set; }
        public double Price { get; set; } 
    }
}
