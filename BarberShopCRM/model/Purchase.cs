using BarberShopCRM.model.database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BarberShopCRM.model {
    public class Purchase : IXmlStorable {
        public string Id { get; set; }
        public IList<ProductWrapper> ProductsWithPrices { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime ReceptionDate { get; set; }

        public double Price => ProductsWithPrices.Select (elt => elt.Price * elt.Count).Sum ();
        

        public string Products {
            get {
                var result = new StringBuilder ();
                foreach (var product in ProductsWithPrices) {
                    result.Append (product.Count);
                    result.Append ("x ");
                    result.Append (product.Product.Name);
                    if(product != ProductsWithPrices.Last())
                        result.Append (", ");
                }
                return result.ToString ();
            }
        }

        public string FilePath { get; } = Path.Combine (Constants.DatabaseDirectoryPath, Constants.PurchaseFileName);

        public IXmlStorable MapFromXml (XElement obj) {
            this.ProductsWithPrices = GetProductsWithPricesFromPurchase (obj.Element ("Products"));
            this.PaymentDate = DateTime.Parse ((string)obj.Element ("PaymentDate"));
            this.ReceptionDate = DateTime.Parse ((string)obj.Element ("ReceptionDate"));
            this.Id = (string)obj.Element ("Id");
            return this;
        }

        public XElement MapToXml () {
            var result = new XElement ("Purchase");
            var productsXml = new XElement ("Products");
            foreach (var product in this.ProductsWithPrices) {
                var productXml = product.Product.MapToXml ();
                productXml.SetAttributeValue ("Price", product.Price);
                productXml.SetAttributeValue ("Count", product.Count);
                productsXml.Add (productXml);
            }
            result.Add (new XElement ("Id", this.Id));
            result.Add (productsXml);
            result.Add (new XElement ("PaymentDate", this.PaymentDate));
            result.Add (new XElement ("ReceptionDate", this.ReceptionDate));
            return result;
        }

        private IList<ProductWrapper> GetProductsWithPricesFromPurchase (XElement products) {
            var result = new List<ProductWrapper> ();
            foreach (var xmlProductWithPrice in products.Elements ()) {
                var product = new Product ().MapFromXml (xmlProductWithPrice) as Product;
                var count = (int)xmlProductWithPrice.Attribute ("Count");
                var price = (double)xmlProductWithPrice.Attribute ("Price");
                result.Add (new ProductWrapper () { Product = product, Count = count, Price = price });
            }
            return result;
        }
    }
}
