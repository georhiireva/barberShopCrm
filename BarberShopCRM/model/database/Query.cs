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
    class Query : AbstractXmlHolder {
        private static Query instance;
        private Logger logger;
        private string ProductFilePath { get; } = Path.Combine (Constants.DatabaseDirectoryPath, Constants.ProductFileName);
        private string PurchaseFilePath { get; } = Path.Combine (Constants.DatabaseDirectoryPath, Constants.PurchaseFileName);
        private string WriteOffFilePath { get; } = Path.Combine (Constants.DatabaseDirectoryPath, Constants.WriteOffFileName);

        public static Query Instance {
            get {
                return instance == null ? new Query () : instance;
            }
        }

        private Query () {
            instance = this;
            logger = new Logger (this.GetType ().Name);
        }

        //Метод загружает все записи из файла с продукатми и возвращает коллекцию Product
        public IEnumerable<Product> LoadAllProducts () {
            var rows = LoadFile (ProductFilePath, "Products").Elements ();
            return
                from elt in rows
                select new Product ().MapFromXml (elt) as Product;
        }

        //Метод загружает все записи из файла с закупками и возвращает коллекцию Purchase
        public IEnumerable<Purchase> LoadAllPurchases () {
            var rows = LoadFile (PurchaseFilePath, "Purchases").Elements ();
            return
                from elt in rows
                select new Purchase ().MapFromXml (elt) as Purchase;
        }

        public IEnumerable<WriteOff> LoadAllWriteOffs () {
            var rows = LoadFile (WriteOffFilePath, "WriteOffs").Elements ();
            return
                from elt in rows
                select new WriteOff ().MapFromXml (elt) as WriteOff;
        }

        private XElement LoadFile (string filePath, string root) {
            if (!File.Exists (filePath))
                new XElement (root).Save (filePath);

            return XElement.Load (filePath);
        }
    }
}
