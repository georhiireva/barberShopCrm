using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShopCRM.model {
    class Purchase {
        public IDictionary<Product, double> ProductsWithPrices { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime ReceptionDate { get; set; }
    }
}
