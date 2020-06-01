using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarberShopCRM.model.database;

namespace BarberShopCRM.model {
    class Remain {
        private Product _product;
        private int _openedCount;
        public string ProductName => _product.Name;
        public int ClosedCount { get; }
        public string OpenedCount { get; }

        public Remain(Product product) {
            _product = product;
            var purchases = Query.Instance.LoadAllPurchases();
            var writeOffs = Query.Instance.LoadAllWriteOffs();
            
        }

        private int GetProductCountFromDb(IEnumerable<IProductContainer> list) {
            int retVal = 0;
            foreach (var elt in list) {
                foreach (var productWrapper in elt.ProductsWithPrices) {
                    if (productWrapper.Product.Id == _product.Id) {
                        retVal += productWrapper.ClosedCount;
                    }
                }
            }

            return retVal;
        }

    }
}
