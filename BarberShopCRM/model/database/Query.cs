using BarberShopCRM.exception;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Linq;

namespace BarberShopCRM.model.database {
    class Query {
        private const string DatabaseDirectoryPath = @"..\..\..\database";
        private const string ProductFileName = "Product.xml";
        private const string PurchaseFileName = "Purchase.xml";
        private const string WriteOffFileName = "WriteOff.xml";
        private static Query instance;
        private XElement xml;
        private Logger logger;

        public static Query Instance {
            get {
                return instance == null ? new Query () : instance;
            }
        }

        private string ProductFilePath { get; } = Path.Combine (DatabaseDirectoryPath, ProductFileName);
        private string PurchaseFilePath { get; } = Path.Combine (DatabaseDirectoryPath, PurchaseFileName);
        private string WriteOffFilePath { get; } = Path.Combine (DatabaseDirectoryPath, WriteOffFileName);

        private Query () {
            instance = this;
            logger = new Logger (this.GetType ().Name);
            xml = LoadAllProductsAsXElement ();
        }

        public IEnumerable<Product> LoadAllProducts () {
            return
                from elt in xml.Elements ()
                select new Product () {
                    Name = (string)elt.Element ("Name"),
                    Note = (string)elt.Element ("Note"),
                    Unit = (Unit)Enum.Parse (typeof (Unit), ((string)elt.Element ("Unit"))),
                    Crushable = (bool)elt.Element ("Crushable"),
                    MinCountInUnits = (int)elt.Element ("MinCountInUnits"),
                    UnitsInOnePieceCount = (int)elt.Element ("UnitsInOnePieceCount")
                };
        }

        public bool SaveProduct (Product product) {
            if (IsProductExist (product.Name)) {
                throw new DatabaseDuplicateException ("Продукт с таким именем уже существует!");
            }

            var xmlProduct = MapProduct (product);
            xml.Add (xmlProduct);
            try {
                xml.Save (ProductFilePath);
            }
            catch (XmlException e) {
                logger.log (e.Message);
                return false;
            }
            return true;
        }

        public bool DeleteProduct (string productName) {
            if (!IsProductExist (productName)) {
                throw new DatabaseNotFoundException ($"Продукт {productName} не найден в базе данных");
            }
            XElement removableProduct = FindProductByName (productName);
            removableProduct.Remove ();
            xml.Save (ProductFilePath);
            return true;
        }

        public bool ReplaceProduct (Product oldProduct, Product newProduct) {
            if (!IsProductExist (oldProduct.Name)) {
                throw new DatabaseNotFoundException ($"Продукт {oldProduct.Name} не найден в базе данных");
            }
            var oldProductXml = FindProductByName (oldProduct.Name);
            var newProductXml = MapProduct (newProduct);
            oldProductXml.Remove ();
            SaveProduct (newProduct);
           // xml.Add (newProductXml);
            return true;
        }

        private XElement FindProductByName (string productName) {
            return xml.Elements ().Where (elt => (string)elt.Element ("Name") == productName).First ();
        }

        private XElement MapProduct (Product product) {
            return new XElement (
                "product",
                new XElement (
                    "Name",
                    product.Name),
                new XElement (
                    "Note",
                    product.Note),
                new XElement (
                    "Unit",
                    product.Unit),
                new XElement (
                    "Crushable",
                    product.Crushable),
                new XElement (
                    "MinCountInUnits",
                    product.MinCountInUnits),
                new XElement (
                    "UnitsInOnePieceCount",
                    product.UnitsInOnePieceCount)
                );
        }

        private bool IsProductExist (string productName) {
            var duplicate = xml.Descendants ("Name").Where (elt => (string)elt == productName);
            return duplicate.Count () != 0;
        }

        private XElement LoadAllProductsAsXElement () {
            if (!File.Exists (ProductFilePath)) {
                var emptyXMl = new XElement ("Products");
                emptyXMl.Save (ProductFilePath);
            }
            return XElement.Load (ProductFilePath);
        }
    }
}
