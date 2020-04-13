using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShopCRM.model {
    public class ProductWrapper {
        public Product Product { get; set; }
        public int Count { get; set; }
        public double Price { get; set; } 
    }
}
