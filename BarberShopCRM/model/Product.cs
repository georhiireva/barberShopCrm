using BarberShopCRM.model.database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BarberShopCRM.model {
    public class Product : IClonable<Product>, IXmlStorable {
        public static int Count { get; set; }
        public string FilePath { get; } = Path.Combine (Constants.DatabaseDirectoryPath, Constants.ProductFileName);
        public string Id { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public Unit Unit { get; set; }
        public bool Crushable { get; set; }
        public int MinCountInUnits { get; set; }
        public int CountInUnits { get; set; }
        public int UnitsInOnePieceCount { get; set; }
        public string DisplayCount {
            get {
                if (!Crushable)
                    return CountInUnits.ToString ();
                else {
                    var wholePart = CountInUnits / UnitsInOnePieceCount;
                    var oddment = CountInUnits % UnitsInOnePieceCount;
                    return $"{wholePart} {Unit.Piece}(s) {oddment} {this.Unit}";
                }
            }
        }

        public Product Clone () {
            var result = new Product ();
            result.Id = this.Id;
            result.Name = this.Name;
            result.Note = this.Note;
            result.Unit = this.Unit;
            result.Crushable = this.Crushable;
            result.MinCountInUnits = this.MinCountInUnits;
            result.CountInUnits = this.CountInUnits;
            result.UnitsInOnePieceCount = this.UnitsInOnePieceCount;
            return result;
        }

        public IXmlStorable MapFromXml (XElement obj) {
            this.Id = (string)obj.Element ("Id");
            this.Name = (string)obj.Element ("Name");
            this.Note = (string)obj.Element ("Note");
            this.Unit = (Unit)Enum.Parse (typeof (Unit), ((string)obj.Element ("Unit")));
            this.Crushable = (bool)obj.Element ("Crushable");
            this.MinCountInUnits = (int)obj.Element ("MinCountInUnits");
            this.UnitsInOnePieceCount = (int)obj.Element ("UnitsInOnePieceCount");
            return this;
        }

        public XElement MapToXml () =>
            new XElement (
                "Product",
                new XElement(
                    "Id",
                    this.Id
                    ),
                new XElement (
                    "Name",
                    this.Name),
                new XElement (
                    "Note",
                    this.Note),
                new XElement (
                    "Unit",
                    this.Unit),
                new XElement (
                    "Crushable",
                    this.Crushable),
                new XElement (
                    "MinCountInUnits",
                    this.MinCountInUnits),
                new XElement (
                    "UnitsInOnePieceCount",
                    this.UnitsInOnePieceCount)
                );
        
    }
}
