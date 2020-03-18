using BarberShopCRM.model.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BarberShopCRM.model {
    public class Purchase : IXmlStorable {
        public string Id { get; set; }
        public IDictionary<Product, double> ProductsWithPrices { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime ReceptionDate { get; set; }

        public string FilePath => throw new NotImplementedException ();

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
                var productXml = new XElement ("Product", product.Key);
                productXml.SetAttributeValue ("Price", product.Value);
                productsXml.Add (productXml);
            }
            result.Add ("Id", this.Id);
            result.Add (productsXml);
            result.Add (new XElement ("PaymentDate", this.PaymentDate));
            result.Add (new XElement ("ReceptionDate", this.ReceptionDate));
            return result;
        }

        private IDictionary<Product, double> GetProductsWithPricesFromPurchase (XElement products) {
            var result = new Dictionary<Product, double> ();
            foreach (var xmlProductWithPrice in products.Elements ()) {
                var product = new Product ().MapFromXml (xmlProductWithPrice) as Product;
                var price = (double)xmlProductWithPrice.Attribute ("Price");
                result.Add (product, price);
            }
            return result;
        }
    }
}
