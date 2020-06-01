using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BarberShopCRM.model {
    public class WriteOff : IXmlStorable, IProductContainer {
        public IList<ProductWrapper> ProductsWithPrices { get; set; }
        public DateTime WriteOffDate { get; set; }
        public string MasterName { get; set; }

        public string Products {
            get {
                var result = new StringBuilder();
                foreach (var product in ProductsWithPrices) {
                    result.Append(product.ClosedCount);
                    result.Append("x ");
                    result.Append(product.Product.Name);
                    if (product != ProductsWithPrices.Last())
                        result.Append(", ");
                }
                return result.ToString();
            }
        }

        public string FilePath => Path.Combine(Constants.DatabaseDirectoryPath, Constants.WriteOffFileName);

        public string Id { get; set; }

        public IXmlStorable MapFromXml(XElement obj) {
            this.ProductsWithPrices = GetProductsWithPricesFromWriteOff(obj.Element("Products"));
            this.MasterName = (string)obj.Element("MasterName");
            this.WriteOffDate = DateTime.Parse((string)obj.Element("WriteOffDate"));
            this.Id = (string) obj.Element("Id");
            return this;
        }

        public XElement MapToXml() {
            var retVal = new XElement("WriteOff");
            var productsXml = new XElement("Products");
            foreach (var product in this.ProductsWithPrices) {
                var productXml = product.Product.MapToXml();
                productXml.SetAttributeValue("Price", product.Price);
                productXml.SetAttributeValue("ClosedCount", product.ClosedCount);
                productsXml.Add(productXml);
            }

            retVal.Add(new XElement("MasterName", this.MasterName));
            retVal.Add(new XElement("WriteOffDate", this.WriteOffDate));
            retVal.Add(productsXml);
            retVal.Add(new XElement("Id", this.Id));
            return retVal;
        }

        private IList<ProductWrapper> GetProductsWithPricesFromWriteOff(XElement products) {
            var result = new List<ProductWrapper>();
            foreach (var xmlProductWithPrice in products.Elements()) {
                var product = new Product().MapFromXml(xmlProductWithPrice) as Product;
                var count = (int)xmlProductWithPrice.Attribute("ClosedCount");
                var price = (double)xmlProductWithPrice.Attribute("Price");
                result.Add(new ProductWrapper() { Product = product, ClosedCount = count, Price = price });
            }

            return result;
        }
    }
}