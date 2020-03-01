﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShopCRM.model {
    public class Product : IClonable<Product> {
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
            result.Name = this.Name;
            result.Note = this.Note;
            result.Unit = this.Unit;
            result.Crushable = this.Crushable;
            result.MinCountInUnits = this.MinCountInUnits;
            result.CountInUnits = this.CountInUnits;
            result.UnitsInOnePieceCount = this.UnitsInOnePieceCount;
            return result;
        }
    }
}
