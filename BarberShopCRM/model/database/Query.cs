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
        }

        public IEnumerable<Product> LoadAllProducts () {
            XElement xml = LoadAllProductsAsXElement ();
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

        private bool IsProductUnique (string productName, XElement products) {
            var duplicate = products.Descendants ("Name").Where (elt => (string)elt == productName);
            return duplicate.Count () == 0;
        }

        private XElement LoadAllProductsAsXElement () {
            if (!File.Exists (ProductFilePath)) {
                var emptyXMl = new XElement ("Products");
                emptyXMl.Save (ProductFilePath);
            }
            return XElement.Load (ProductFilePath);
        }

        public bool SaveProduct (Product product) {
            XElement xml = LoadAllProductsAsXElement ();
            if (!IsProductUnique (product.Name, xml)) {
                throw new DatabaseDuplicateException ("Продукт с таким именем уже существует!");
            }
                xml.Add (
                new XElement (
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
                    ));
            try {
                xml.Save (ProductFilePath);
            }
            catch (XmlException e) {
                logger.log (e.Message);
                return false;
            }
            return true;
        }
    }
}
