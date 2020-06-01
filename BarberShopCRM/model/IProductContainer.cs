using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShopCRM.model {
    interface IProductContainer {
        IList<ProductWrapper> ProductsWithPrices { get; set; }
    }
}
